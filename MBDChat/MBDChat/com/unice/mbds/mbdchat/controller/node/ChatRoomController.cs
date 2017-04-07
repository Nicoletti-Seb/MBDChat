using MBDChat.com.unice.mbds.mbdchat.controller.action;
using MBDChat.com.unice.mbds.mbdchat.controller.node;
using MBDChat.com.unice.mbds.mbdchat.controller.receipter;
using MBDChat.com.unice.mbds.mbdchat.controller.sender;
using MBDChat.com.unice.mbds.mbdchat.model.message;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace MBDChat.com.unice.mbds.mbdchat.model.clientServer
{
    public class ChatRoomController
    {
        // att
        private List<ChatRoom> chatrooms = new List<ChatRoom>();
        private List<Action> actions;
        private const int MAX_PAIR = 4;

        // properties
        public string nickname { get; set; }
        public int port { get; set; }
        public string ipAddress { get; set; }
        public Sender sender { get; set; }
        public Receipter receipter { get; set; }
        public List<Pair> nodes { get; set; }

        // events
        public delegate void EventUpdatePairs();
        public event EventUpdatePairs eventUpdatePairs;

        // singleton
        public static readonly ChatRoomController instance = new ChatRoomController();
        public static ChatRoomController Instance { get { return instance; } }

        private ChatRoomController() {
            port = 2323;
            nodes = new List<Pair>();
        }

        public void startUp()
        {
            //Init connexion
            Socket socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
            initListAction();

            //Receipter
            receipter = new ReceipterImp(actions);
            receipter.startListen(port);

            //Sender
            sender = new SenderImpl(socket, port, actions);
            sender.sendHelloBroadcast();
        }

        public string getIpLocal()
        {
            if (ipAddress != null)
            {
                return ipAddress;
            }

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = ip.ToString();
                    return ipAddress;
                }
            }
            throw new System.Exception("Local IP Address Not Found!");
        }

        public void addPair(Pair pair)
        {
            if (pair.Addr != getIpLocal() && !nodes.Contains(pair) && nodes.Count <= MAX_PAIR)
            {
                System.Console.WriteLine("ADD PAIR " + pair.Addr);
                nodes.Add(pair);

                //notify
                if (eventUpdatePairs != null)
                {
                    eventUpdatePairs.Invoke();
                }
            }
        }

        public void removePair(string addr)
        {
            System.Console.WriteLine("REMOVE PAIR " + addr);
            foreach (Pair p in nodes)
            {
                if (p.Addr.Equals(addr))
                {
                    nodes.Remove(p);

                    //notify
                    if (eventUpdatePairs != null)
                    {
                        eventUpdatePairs.Invoke();
                    }

                    return;
                }
            }
        }

        private void initListAction()
        {
            actions = new List<Action>();
            actions.Add(new ActionGoodBye());
            actions.Add(new ActionHelloA());
            actions.Add(new ActionHelloR());
            actions.Add(new ActionMessage());
            actions.Add(new ActionPingPong());
        }
    }
}

using MBDChat.com.unice.mbds.mbdchat.controller.action;
using MBDChat.com.unice.mbds.mbdchat.controller.node;
using MBDChat.com.unice.mbds.mbdchat.controller.receipter;
using MBDChat.com.unice.mbds.mbdchat.controller.sender;
using MBDChat.com.unice.mbds.mbdchat.model.message;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace MBDChat.com.unice.mbds.mbdchat.model.clientServer
{
    public class ChatRoomController
    {
       
        private List<ChatRoom> chatrooms = new List<ChatRoom>();

        public string nickname { get { return "Seb&Leo"; } }
        public int port { get; set; }
        public Sender sender { get; set; }
        public Receipter receipter { get; set; }
        public List<Pair> nodes { get; }

        public static readonly ChatRoomController instance = new ChatRoomController();
        public static ChatRoomController Instance { get { return instance; } }
        
        private List<Action> actions;

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
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    System.Console.WriteLine(ip.ToString());
                    return ip.ToString();
                }
            }
            throw new System.Exception("Local IP Address Not Found!");
        }

        public void addPair(Pair pair)
        {
            nodes.Add(pair);
        }

        public void removePair(string addr)
        {
            foreach (Pair p in nodes)
            {
                if (p.Addr == addr)
                {
                    nodes.Remove(p);
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

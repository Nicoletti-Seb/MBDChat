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
        private Socket socket;
        private List<Pair> nodes = new List<Pair>();
        private List<ChatRoom> chatrooms = new List<ChatRoom>();

        public string nickname { get { return "Seb&Leo"; } }
        public int port { get; set; }
        public Sender sender { get; set; }
        public Receipter receipter { get; set; }

        public static readonly ChatRoomController instance = new ChatRoomController();
        public static ChatRoomController Instance { get { return instance; } }
        private ChatRoomController() { port = 2323; }
        private List<Action> actions;

        public void startUp()
        {
            //Init connexion
            socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
            initListAction();

            //Receipter
            receipter = new ReceipterImp(actions);
            receipter.startListen(port);

            //Sender
            sender = new SenderImpl(socket, nodes, port, actions);
            sender.sendHelloBroadcast();
        }

        public string getIpLocal()
        {
            return (socket.LocalEndPoint as IPEndPoint).ToString();
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
            actions.Add(new ActionHello());
            actions.Add(new ActionMessage());
            actions.Add(new ActionPingPong());
        }
    }
}

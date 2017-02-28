using MBDChat.com.unice.mbds.mbdchat.controller.node;
using MBDChat.com.unice.mbds.mbdchat.controller.receipter;
using MBDChat.com.unice.mbds.mbdchat.controller.sender;
using MBDChat.com.unice.mbds.mbdchat.model.message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        public void startUp()
        {
            //Init connexion
            socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);

            //Receipter
            receipter = new ReceipterImp();
            receipter.startListen(port);

            //Sender
            sender = new SenderImpl(socket, nodes, port);
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
    }
}

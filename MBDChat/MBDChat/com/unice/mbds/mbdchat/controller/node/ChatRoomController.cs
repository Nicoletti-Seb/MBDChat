using MBDChat.com.unice.mbds.mbdchat.controller.node;
using MBDChat.com.unice.mbds.mbdchat.controller.receipter;
using MBDChat.com.unice.mbds.mbdchat.controller.sender;
using MBDChat.com.unice.mbds.mbdchat.model.message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model.clientServer
{
    public class ChatRoomController
    {
        public int port { get; set; }
        private Socket socket;
        private List<Pair> nodes = new List<Pair>();
        private List<ChatRoom> chatrooms = new List<ChatRoom>();
        private Sender sender;
        private Receipter receipter;

        public static readonly ChatRoomController instance = new ChatRoomController();

        private ChatRoomController() { this.port = 2323; }
        public static ChatRoomController Instance { get { return instance; } }

        public void startUp()
        {
            /*
            Thread thread = new Thread(listner());
            thread.start();
            */

            //Init connexion
            socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);

            //Receipter
            receipter = new ReceipterImp();
            receipter.startListen(port);

            //Sender
            sender = new SenderImpl(socket, nodes);
            sender.sendHelloBroadcast();
        }

        private string getIpLocal()
        {
            return (socket.LocalEndPoint as IPEndPoint).ToString();
        }

        public void addPair(Pair pair)
        {
            nodes.Add(pair);
        }
    }
}

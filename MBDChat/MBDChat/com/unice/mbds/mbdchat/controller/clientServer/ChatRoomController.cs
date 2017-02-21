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
    class ChatRoomController : Receipter, Sender
    {
        public int port { get; set; }
        private List<Pair> pairs = new List<Pair>();
        //private List<ChatRoom> chatrooms;
        private Socket socket;

        public static readonly ChatRoomController instance = new ChatRoomController();

        private ChatRoomController()
        {
            this.port = 2323;
        }

        public static ChatRoomController Instance {
            get 
            {
                return instance; 
            }
        }

        public void startUp()
        {
            /*
            Thread thread = new Thread(listner());
            thread.start();
            */

            socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
        }

        public void sendHelloBroadcast()
        {
            throw new NotImplementedException();
        }

        public void sendMessage(string message, EndPoint remoteEP)
        {
            byte[] msg = Encoding.ASCII.GetBytes(message);

            socket.SendTo(msg, remoteEP);
        }

        public void receiptMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}

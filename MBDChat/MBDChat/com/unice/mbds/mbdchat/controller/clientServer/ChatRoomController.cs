using MBDChat.com.unice.mbds.mbdchat.model.message;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
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

        public static string toJson(Message message)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Message));

            MemoryStream stream1 = new MemoryStream();
            ser.WriteObject(stream1, message);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            Console.WriteLine(sr.ReadToEnd());

            return sr.ReadToEnd();
        }

        public static Message toMessage(string json)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Message));

            MemoryStream stream1 = new MemoryStream();
            Message msg = (Message)ser.ReadObject(stream1);

            return msg;
        }
    }
}

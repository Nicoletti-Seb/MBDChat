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
        public string nickname { get { return "Seb&Leo"; } }
        public int port { get; set; }
        private Socket socket;
        private List<Pair> nodes = new List<Pair>();
        private List<ChatRoom> chatrooms = new List<ChatRoom>();
        public Sender sender { get; set; }
        public Receipter receipter { get; set; }

        public static readonly ChatRoomController instance = new ChatRoomController();
        public static SHA256 mySHA256 = new SHA256Managed();

        private ChatRoomController() { this.port = 2323; }
        public static ChatRoomController Instance { get { return instance; } }

        public void startUp()
        {
            //Init connexion
            socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);

            //Receipter
            receipter = new ReceipterImp();
            receipter.startListen(port);

            //Sender
            sender = new SenderImpl(socket, nodes);
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

        public static long UnixTimestampFromDateTimeNow()
        {
            long unixTimestamp = DateTime.Now.Ticks - new DateTime(1970, 1, 1).Ticks;
            unixTimestamp /= TimeSpan.TicksPerSecond;
            return unixTimestamp;
        }

        public static long UnixTimestampFromDateTime(DateTime date)
        {
            long unixTimestamp = date.Ticks - new DateTime(1970, 1, 1).Ticks;
            unixTimestamp /= TimeSpan.TicksPerSecond;
            return unixTimestamp;
        }

        public static string toHash(string type, string nickname, string message, string timestamp, string dest)
        {
            byte[] hash = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(type + nickname + message + timestamp + dest));

            StringBuilder res = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                res.Append(String.Format("{0:X2}", hash[i]));
            }
            Console.WriteLine("hash:" + res.ToString());
            return res.ToString();
        }
    }
}

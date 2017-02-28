using MBDChat.com.unice.mbds.mbdchat.model.clientServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using MBDChat.com.unice.mbds.mbdchat.model;
using System.Net.Sockets;
using MBDChat.com.unice.mbds.mbdchat.model.message;
using System.Runtime.Serialization.Json;
using System.IO;

namespace MBDChat.com.unice.mbds.mbdchat.controller.sender
{
    public class SenderImpl : Sender
    {
        private List<Pair> nodes;
        private Socket socket;

        private DataContractJsonSerializer jsonParser;
        private MemoryStream stream;
        private StreamReader streamReader;

        public SenderImpl(Socket socket, List<Pair> nodes)
        {
            this.socket = socket;
            this.nodes = nodes;
            this.jsonParser = new DataContractJsonSerializer(typeof(Message));
            stream = new MemoryStream();
            streamReader = new StreamReader(stream);
        }

        public void sendHelloBroadcast()
        {
            foreach(Pair pair in nodes)
            {
                Message message = new Message("HELLO", new HelloData(pair.Addr, pair.Port, nodes));

                byte[] msg = Encoding.ASCII.GetBytes(parseToJson(message));
                socket.SendTo(msg, pair.ep);
            }
            
        }

        public void sendGoodByeBroadcast()
        {
            foreach (Pair pair in nodes)
            {
                Message message = new Message("GOODBYE", new GoodByeData(pair.Addr));

                byte[] msg = Encoding.ASCII.GetBytes(message.ToString());
                socket.SendTo(msg, pair.ep);
            }
        }

        public void sendPingBroadcast()
        {
            foreach (Pair pair in nodes)
            {
                Message message = new Message("PING/PONG", new PingPongData(pair.Addr, ChatRoomController.UnixTimestampFromDateTimeNow().ToString()));

                byte[] msg = Encoding.ASCII.GetBytes(parseToJson(message));
                socket.SendTo(msg, pair.ep);
            }
        }

        public void sendMessage(Message message)
        {
            foreach(Pair pair in nodes)
            {
                sendMessage(message, pair);
            }
        }

        public void sendMessage(Message message, Pair pair)
        {
            byte[] msg = Encoding.ASCII.GetBytes(parseToJson(message));
            socket.SendTo(msg, pair.ep);
        }

        private string parseToJson(Message message)
        {
            jsonParser.WriteObject(stream, message);
            stream.Position = 0;
           
            string result = streamReader.ReadToEnd();
            Console.WriteLine(result);

            stream.Flush();
            return result;
        }
    }
}

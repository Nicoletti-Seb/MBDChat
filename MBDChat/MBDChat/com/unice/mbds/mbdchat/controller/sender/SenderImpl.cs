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
using MBDChat.com.unice.mbds.mbdchat.controller.utils;

namespace MBDChat.com.unice.mbds.mbdchat.controller.sender
{
    public class SenderImpl : Sender
    {
        private List<Pair> nodes;
        private Socket socket;
        private int port;

        public SenderImpl(Socket socket, List<Pair> nodes, int port)
        {
            this.socket = socket;
            this.nodes = nodes;
            this.port = port;
        }

        public void sendHelloBroadcast()
        {
            foreach(Pair pair in nodes)
            {
                Message message = new Message("HELLO", new HelloData(pair.Addr, pair.Port, nodes));

                byte[] msg = Encoding.ASCII.GetBytes(Parser.parseToJson(message));
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
                Message message = new Message("PING/PONG", new PingPongData(pair.Addr, this.port, Parser.TimestampNow().ToString()));

                byte[] msg = Encoding.ASCII.GetBytes(Parser.parseToJson(message));
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
            byte[] msg = Encoding.ASCII.GetBytes(Parser.parseToJson(message));
            socket.SendTo(msg, pair.ep);
        }
    }
}

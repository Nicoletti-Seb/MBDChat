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

namespace MBDChat.com.unice.mbds.mbdchat.controller.sender
{
    public class SenderImpl : Sender
    {
        private static readonly int PORT = 2323;

        private List<Pair> nodes;
        private Socket socket;

        public SenderImpl(Socket socket, List<Pair> nodes)
        {
            this.socket = socket;
            this.nodes = nodes;
        }

        public void sendHelloBroadcast()
        {
            foreach(Pair pair in nodes)
            {
                Message message = new Message("HELLO", new HelloData(pair.addr, pair.port, nodes));
                byte[] msg = Encoding.ASCII.GetBytes(message.ToString());
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
            byte[] msg = Encoding.ASCII.GetBytes(message.ToString());
            socket.SendTo(msg, pair.ep);
        }
    }
}

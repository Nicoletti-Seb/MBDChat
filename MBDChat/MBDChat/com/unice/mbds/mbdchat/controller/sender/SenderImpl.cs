using MBDChat.com.unice.mbds.mbdchat.model.clientServer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using MBDChat.com.unice.mbds.mbdchat.model;
using System.Net.Sockets;
using MBDChat.com.unice.mbds.mbdchat.model.message;
using MBDChat.com.unice.mbds.mbdchat.controller.utils;
using MBDChat.com.unice.mbds.mbdchat.controller.action;

namespace MBDChat.com.unice.mbds.mbdchat.controller.sender
{
    public class SenderImpl : Sender
    {
        private List<Pair> nodes;
        private Socket socket;
        private int port;
        private List<Action> actions;

        public SenderImpl(Socket socket, List<Pair> nodes, int port, List<Action> actions)
        {
            this.socket = socket;
            this.nodes = nodes;
            this.port = port;
            this.actions = actions;
        }

        public void sendHelloBroadcast()
        {
            foreach(Pair pair in nodes)
            {
                Message message = new HelloMessage(pair.Addr, pair.Port, nodes);
                sendMessage(message, pair);
            }
            
        }

        public void sendGoodByeBroadcast()
        {
            foreach (Pair pair in nodes)
            {
                Message message =  new GoodByeMessage(pair.Addr);
                sendMessage(message, pair);
            }
        }

        public void sendPingBroadcast()
        {
            foreach (Pair pair in nodes)
            {
                Message message = new PingPongMessage(pair.Addr, this.port, Parser.TimestampNow().ToString());
                sendMessage(message, pair);
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

            foreach (Action action in actions)
            {
                if (action.Type == message.Type)
                {
                    action.onSender(message);
                }
            }
        }
    }
}

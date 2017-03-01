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
        public ChatRoomController controller = ChatRoomController.Instance;

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
            HelloMessage hm = new HelloMessage(controller.getIpLocal(), this.port, this.nodes);
            sendMessage(hm);            
        }

        public void sendGoodByeBroadcast()
        {
            GoodByeMessage gbm =  new GoodByeMessage(controller.getIpLocal());
            System.Console.WriteLine(gbm);
            sendMessage(gbm);
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

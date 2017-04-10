using MBDChat.com.unice.mbds.mbdchat.model.clientServer;
using System.Collections.Generic;
using System.Text;
using MBDChat.com.unice.mbds.mbdchat.model;
using System.Net.Sockets;
using MBDChat.com.unice.mbds.mbdchat.model.message;
using MBDChat.com.unice.mbds.mbdchat.controller.utils;
using MBDChat.com.unice.mbds.mbdchat.controller.action;
using System.Threading;

namespace MBDChat.com.unice.mbds.mbdchat.controller.sender
{
    public class SenderImpl : Sender
    {
        public ChatRoomController controller = ChatRoomController.Instance;

        private Socket socket;
        private int port;
        private List<Action> actions;

        public SenderImpl(Socket socket, int port, List<Action> actions)
        {
            this.socket = socket;
            this.port = port;
            this.actions = actions;
        }

        public void sendMessage(Message message)
        {
            foreach (Pair pair in controller.nodes)
            {
                new Thread(() =>
                {
                    sendMessage(message, pair);
                }).Start();
                
            }
        }

        public void sendMessage(Message message, Pair pair)
        {
            byte[] msg = Encoding.ASCII.GetBytes(Parser.parseToJson(message));
            socket.SendTo(msg, pair.ep);

            foreach (Action action in actions)
            {
                if (action.containsType(message.Type))
                {
                    action.onSender(message, pair);
                }
            }
        }
    }
}

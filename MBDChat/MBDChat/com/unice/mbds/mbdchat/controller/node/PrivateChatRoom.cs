using MBDChat.com.unice.mbds.mbdchat.controller.utils;
using MBDChat.com.unice.mbds.mbdchat.model.clientServer;
using MBDChat.com.unice.mbds.mbdchat.model.message;
using System;

namespace MBDChat.com.unice.mbds.mbdchat.controller.node
{
    public class PrivateChatRoom
    {
        private ChatRoomController controller = ChatRoomController.Instance;

        //Model
        public string Participant { get; }

        //View
        private ChatRoomView view;
        
        public PrivateChatRoom(string participant)
        {
            this.Participant = participant;
            view = new ChatRoomView(this);
        }

        public void sendMessage(String message)
        {
            string nickname = controller.nickname;
            string timestamp = Parser.TimestampNow().ToString();
            string rootedby = controller.nickname;
            string hash = Parser.toHash("MESSAGE", nickname, message, timestamp, Participant);

            MessageMess msg = new MessageMess(nickname, message, timestamp, "@" + Participant, hash, rootedby);
            controller.sender.sendMessage(msg);
        }

        public void receiveMessage(String message)
        {
            view.reveivedMessage(message);
        }

        public void display()
        {
            view.Dispatcher.Invoke(() =>
            {
                view.Show();
            });
        }

        public void close()
        {
            view.Dispatcher.Invoke(() =>
            {
                view.Hide();
            });
        }

        public override string ToString()
        {
            return Participant;
        }
    }
}

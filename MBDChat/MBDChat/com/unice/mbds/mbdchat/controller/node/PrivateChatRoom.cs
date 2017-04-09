using MBDChat.com.unice.mbds.mbdchat.controller.utils;
using MBDChat.com.unice.mbds.mbdchat.model.clientServer;
using MBDChat.com.unice.mbds.mbdchat.model.message;
using System;
using System.Threading;
using System.Windows;

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
            
            /*if(view == null)
            {
                newViewHandler();
                return;
            }

            view.Dispatcher.Invoke(() =>
            {
                view.Show();
            });*/
        }

        public void close()
        {
            view.Dispatcher.Invoke(() =>
            {
                view.Hide();
            });
            
            /*if(view == null)
            {
                return;
            }

            view.Dispatcher.Invoke(() =>
            {
                view.Hide();
            });*/
        }

        public override string ToString()
        {
            return Participant;
        }

        /*private void newViewHandler()
        {
            //Create the window in another thread
            Thread newWindowThread = new Thread(() =>
            {
                view = new ChatRoomView(this);
                view.Show();
                System.Windows.Threading.Dispatcher.Run();
            });
            newWindowThread.SetApartmentState(ApartmentState.STA);
            newWindowThread.IsBackground = true;
            newWindowThread.Start();

            //Wait thread 
            newWindowThread.Join();
        }*/
    }
}

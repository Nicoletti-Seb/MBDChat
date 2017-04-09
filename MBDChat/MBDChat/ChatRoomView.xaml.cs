using MBDChat.com.unice.mbds.mbdchat.controller.node;
using MBDChat.com.unice.mbds.mbdchat.model.clientServer;
using System.Windows;

namespace MBDChat
{
    /// <summary>
    /// Logique d'interaction pour ChatRoom.xaml
    /// </summary>
    public partial class ChatRoomView : Window
    {
        private ChatRoomController controller = ChatRoomController.Instance;

        private PrivateChatRoom chatRoom;

        public ChatRoomView(PrivateChatRoom chatRoom)
        {
            InitializeComponent();
            this.chatRoom = chatRoom;
            this.Title = "ChatRoom-" + controller.nickname + ": " + chatRoom.Participant;
        }

        void sendMessage(object sender, RoutedEventArgs e)
        {
            if(Message.Text == "")
            {
                return;
            }

            //Refresh textarea message
            string message = Message.Text;
            Message.Text = "";

            //Send
            chatRoom.sendMessage(message);

            //Add msg in messagelist
            string toDisplay = controller.nickname + " : " + message;
            MessagesList.Items.Add(toDisplay);
        }

        public void reveivedMessage(string msg)
        {
            string toDisplay = chatRoom.Participant + " : " + msg;
            if (!MessagesList.Dispatcher.CheckAccess())
            {
                MessagesList.Dispatcher.Invoke(() =>
                {
                    MessagesList.Items.Add(toDisplay);
                });
            }
            else
            {
                MessagesList.Items.Add(toDisplay);
            }
        }

        private void onClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}

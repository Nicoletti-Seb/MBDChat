using MBDChat.com.unice.mbds.mbdchat.controller.node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MBDChat
{
    /// <summary>
    /// Logique d'interaction pour ChatRoom.xaml
    /// </summary>
    public partial class ChatRoomView : Window
    {
        private ChatRoom chatRoom;
        public ChatRoomView(ChatRoom chatRoom)
        {
            InitializeComponent();
        }

        void sendMessage(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Send message");
        }
    }
}

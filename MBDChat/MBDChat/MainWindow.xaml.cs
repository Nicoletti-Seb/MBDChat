using MBDChat.com.unice.mbds.mbdchat.model;
using MBDChat.com.unice.mbds.mbdchat.model.clientServer;
using MBDChat.com.unice.mbds.mbdchat.model.message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MBDChat
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ChatRoomController controller = ChatRoomController.Instance;

        public MainWindow()
        {
            InitializeComponent();

            controller.addPair(new Pair("192.168.0.160", 2323));
            controller.startUp();

            /*List<Pair> pairs = new List<Pair>();
            pairs.Add(new Pair("192.168.0.5", "2323"));
            pairs.Add(new Pair("192.168.0.45", "2323"));
            Message msg = new Message("HELLO", new HelloData("127.0.0.1", "2323", pairs));

            string json = ChatRoomController.toJson(msg);
            Console.WriteLine("json : " + json);*/
        }

        void sendMessage(object sender, RoutedEventArgs e)
        {
            // check empty message
            if (TextToSend.Text == "") { return; }

            string type = "MESSAGE";
            string nickname = controller.nickname;
            string message = TextToSend.Text;
            string timestamp = ChatRoomController.UnixTimestampFromDateTime(DateTime.Now).ToString();
            string dest = "";
            string hash = ChatRoomController.toHash(type, nickname, message, timestamp, dest);
            string rootedby = controller.nickname;

            MessageData msgData = new MessageData(nickname, message, timestamp, dest, hash, rootedby);
            Message msg = new Message(type, msgData);

            controller.sender.sendMessage(msg);

            // add msg in messagelist
            MessagesList.Items.Add(message);

            // refresh textarea message
            TextToSend.Text = "";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            controller.sender.sendGoodBye();
            Console.WriteLine("GoodBye");
        }
    }
}

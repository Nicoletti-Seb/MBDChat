using MBDChat.com.unice.mbds.mbdchat.controller.utils;
using MBDChat.com.unice.mbds.mbdchat.model;
using MBDChat.com.unice.mbds.mbdchat.model.clientServer;
using MBDChat.com.unice.mbds.mbdchat.model.message;
using System;
using System.Windows;
using System.Windows.Controls;

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

            //controller.addPair(new Pair("192.168.0.145", 2323));
            controller.addPair(new Pair("127.0.0.1", 2323));
            controller.addPair(new Pair("10.154.127.247", 2323));
            controller.addPair(new Pair("10.154.127.245", 2323));

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
            string timestamp = Parser.TimestampNow().ToString();
            string dest = "";
            string hash = Parser.toHash(type, nickname, message, timestamp, dest);
            string rootedby = controller.nickname;

            MessageMess msg = new MessageMess(nickname, message, timestamp, dest, hash, rootedby);

            controller.sender.sendMessage(msg);

            // add msg in messagelist
            MessagesList.Items.Add(message);

            // refresh textarea message
            TextToSend.Text = "";
        }

        private void onReceiveMessage(string message)
        {
            if (!MessagesList.Dispatcher.CheckAccess())//vérifie que le thread courant est bien le thread graphique (celui qui a créé la listbox)
            {
                MessagesList.Dispatcher.Invoke(() => { MessagesList.Items.Add(message); });
            }
            else
            {
                MessagesList.Items.Add(message);
            }
                
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // error
            //controller.sender.sendGoodByeBroadcast();
            Console.WriteLine("GoodBye");
        }

        private void onLoaded(object sender, RoutedEventArgs e)
        {
           controller.startUp();
           controller.receipter.events += onReceiveMessage;
        }
    }
}

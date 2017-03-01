using MBDChat.com.unice.mbds.mbdchat.controller.utils;
using MBDChat.com.unice.mbds.mbdchat.model;
using MBDChat.com.unice.mbds.mbdchat.model.clientServer;
using MBDChat.com.unice.mbds.mbdchat.model.message;
using System;
using System.Collections.Generic;
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

            // add event pour mettre a jour la liste des pairs
            controller.eventUpdatePairs += updateListPair;
            //controller.addPair(new Pair("10.154.106.235", 2323, ));
            controller.addPair(new Pair("10.154.127.247", 2323, "David"));
            controller.addPair(new Pair("10.154.127.245", 2323));
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

        public void updateListPair()
        {
            System.Console.WriteLine("UPDATE NODES");

            // update list user
            usersList.Items.Clear();
            foreach (Pair p in controller.nodes)
            {
                usersList.Items.Add(p.Nickname);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // error
            controller.sender.sendGoodByeBroadcast();
            Console.WriteLine("WINDOW_CLOSING");
        }

        private void onLoaded(object sender, RoutedEventArgs e)
        {
           System.Console.WriteLine("ONLOAD");
           controller.startUp();
           controller.receipter.events += onReceiveMessage;
        }
    }
}

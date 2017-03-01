using MBDChat.com.unice.mbds.mbdchat.controller.node;
using MBDChat.com.unice.mbds.mbdchat.controller.utils;
using MBDChat.com.unice.mbds.mbdchat.model;
using MBDChat.com.unice.mbds.mbdchat.model.clientServer;
using MBDChat.com.unice.mbds.mbdchat.model.message;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            controller.addPair(new Pair("10.154.106.235", 2323, "Seb"));
            controller.addPair(new Pair("10.154.127.244", 2323, "Aurore"));
            controller.addPair(new Pair("10.154.124.248", 2323, "Léo"));
            controller.addPair(new Pair("10.154.127.235", 2323, "Thais"));
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
            // CHANGER PAR LIST USER ET DISPLAY NICKNAME
            if (!usersList.Dispatcher.CheckAccess())
            {
                usersList.Dispatcher.Invoke(() =>
                { 
                    System.Console.WriteLine("UPDATE NODES");

                    // update list user
                    usersList.Items.Clear();
                    foreach (Pair p in controller.nodes)
                    {
                        usersList.Items.Add(p.Addr);
                    }
                    usersList.Items.Refresh();
                });
            }
            else
            {
                System.Console.WriteLine("UPDATE NODES");

                // update list user
                usersList.Items.Clear();
                foreach (Pair p in controller.nodes)
                {
                    usersList.Items.Add(p.Addr);
                }
                usersList.Items.Refresh();
            }
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            controller.sender.sendGoodByeBroadcast();
            Console.WriteLine("WINDOW_CLOSING");
            Environment.Exit(0);
        }

        private void onLoaded(object sender, RoutedEventArgs e)
        {
           System.Console.WriteLine("ONLOAD");
           controller.startUp();
           controller.receipter.events += onReceiveMessage;
        }

        private void HelloClick(object sender, RoutedEventArgs e)
        {
            controller.sender.sendHelloBroadcast();
        }

        private void GoodByeClick(object sender, RoutedEventArgs e)
        {
            controller.sender.sendGoodByeBroadcast();
        }

        private void ClearClick(object sender, RoutedEventArgs e)
        {
            controller.nodes = new List<Pair>();
            System.Console.WriteLine("CLEAR : " + controller.nodes.Count);
            updateListPair();
        }

        private void PingClick(object sender, RoutedEventArgs e)
        {
            PingPongMessage ppm = new PingPongMessage(controller.getIpLocal(), controller.port, Parser.TimestampNow().ToString());
            //controller.sender.sendMessage(ppm);
        }

        private void onDblClickUser(object sender, MouseButtonEventArgs e)
        {
            new ChatRoomView(new ChatRoom()).Show();
        }
    }
}

using MBDChat.com.unice.mbds.mbdchat.controller.node;
using MBDChat.com.unice.mbds.mbdchat.controller.utils;
using MBDChat.com.unice.mbds.mbdchat.model.clientServer;
using MBDChat.com.unice.mbds.mbdchat.model.message;
using System;
using System.Windows;
using System.Windows.Input;

namespace MBDChat
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ChatRoomController controller;

        public MainWindow(ChatRoomController chatRoomController)
        {
            InitializeComponent();
            this.controller = chatRoomController;

            //Update participants list
            usersList.ItemsSource = controller.participants;
            chatList.ItemsSource = controller.chatRooms;
            pairsList.ItemsSource = controller.nodes;

            //Init events
            controller.eventUpdateParticipants += updateParticipants;
            controller.eventUpdateChatRooms += updateChatRooms;
            controller.eventUpdatePairs += updatePairs;
            controller.eventReceiptedMessage += onReceiveMessage;
            

            //Title
            Title = controller.ipAddress + " - " + controller.nickname;
        }

        void onSendMessage(object sender, RoutedEventArgs e)
        {
            sendMessage();
        }

        private void sendMessage()
        {
            //Check empty message
            if (TextToSend.Text == "") { return; }

            //Refresh textarea message
            string message = TextToSend.Text;
            TextToSend.Text = "";

            //Create message
            string nickname = controller.nickname;
            string timestamp = Parser.TimestampNow().ToString();
            string dest = "";
            string hash = Parser.toHash("MESSAGE", nickname, message, timestamp, dest);
            string rootedby = controller.nickname;
            MessageMess msg = new MessageMess(nickname, message, timestamp, dest, hash, rootedby);

            //Send message
            controller.sender.sendMessage(msg);

            //Add msg in messagelist
            string toDisplay = controller.nickname + " : " + message;
            MessagesList.Items.Add(toDisplay);
        }

        private void onReceiveMessage(string message, string src)
        {
            string toDisplay = src + " : " + message;
            if (!MessagesList.Dispatcher.CheckAccess())
            {
                MessagesList.Dispatcher.Invoke(() => { MessagesList.Items.Add(toDisplay); });
            }
            else
            {
                MessagesList.Items.Add(toDisplay);
            }
        }

        public void updateParticipants()
        {
            if (!usersList.Dispatcher.CheckAccess())
            {
                usersList.Dispatcher.Invoke(() =>
                { 
                    usersList.Items.Refresh();
                });
            }
            else
            {
                usersList.Items.Refresh();
            }
        }

        public void updateChatRooms()
        {
            if (!chatList.Dispatcher.CheckAccess())
            {
                chatList.Dispatcher.Invoke(() =>
                {
                    chatList.Items.Refresh();
                });
            }
            else
            {
                chatList.Items.Refresh();
            }
        }

        public void updatePairs()
        {
            if (!pairsList.Dispatcher.CheckAccess())
            {
                pairsList.Dispatcher.Invoke(() =>
                {
                    pairsList.Items.Refresh();
                });
            }
            else
            {
                pairsList.Items.Refresh();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GoodByeMessage goodBye = new GoodByeMessage(controller.getIpLocal(), controller.nickname);
            controller.sender.sendMessage(goodBye);
            Environment.Exit(0);
        }

        private void onLoaded(object sender, RoutedEventArgs e)
        {
           controller.startUp();
        }

        private void HelloClick(object sender, RoutedEventArgs e)
        {
            HelloMessage helloMessage = new HelloMessage(controller.getIpLocal(), controller.port, controller.nodes, true);
            controller.sender.sendMessage(helloMessage);
        }

        private void GoodByeClick(object sender, RoutedEventArgs e)
        {
            GoodByeMessage goodBye = new GoodByeMessage(controller.getIpLocal(), controller.nickname);
            controller.sender.sendMessage(goodBye);
        }

        private void ClearClick(object sender, RoutedEventArgs e)
        {
            controller.participants.Clear();
            controller.nodes.Clear();

            foreach(PrivateChatRoom chatRoom in controller.chatRooms)
            {
                chatRoom.close();
            }
            controller.chatRooms.Clear();

            Console.WriteLine("CLEAR ALL");

            updateParticipants();
            updatePairs();
            updateChatRooms();
        }

        private void PingClick(object sender, RoutedEventArgs e)
        {
            PingPongMessage ppm = new PingPongMessage(controller.getIpLocal(), controller.port, Parser.TimestampNow().ToString(), true);
            controller.sender.sendMessage(ppm);
        }

        private void onDblClickUser(object sender, MouseButtonEventArgs e)
        {
            string name = (string)usersList.SelectedItem;
            controller.openPrivateRoom(name);
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                sendMessage();
            }
        }

        private void onDblClickRooms(object sender, MouseButtonEventArgs e)
        {
            string name = (string)usersList.SelectedItem;
            controller.openPrivateRoom(name);
        }
    }
}

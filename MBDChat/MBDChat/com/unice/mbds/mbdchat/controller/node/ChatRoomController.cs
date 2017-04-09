using MBDChat.com.unice.mbds.mbdchat.controller.action;
using MBDChat.com.unice.mbds.mbdchat.controller.node;
using MBDChat.com.unice.mbds.mbdchat.controller.receipter;
using MBDChat.com.unice.mbds.mbdchat.controller.sender;
using MBDChat.com.unice.mbds.mbdchat.controller.utils;
using MBDChat.com.unice.mbds.mbdchat.model.message;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MBDChat.com.unice.mbds.mbdchat.model.clientServer
{
    public delegate void EventUpdateParticipants();
    public delegate void EventUpdatePairs();
    public delegate void EventUpdateChatRooms();
    public delegate void EventReceiptedMessage(string message, string src);

    public class ChatRoomController
    {
        private const int MAX_PAIR = 4;
        private const int DELAY_TIMER_PING_PONG = 10000;
        private const int DELAY_TIMER_HELLO = 10000;

        // att
        private List<Action> actions = new List<Action>();
        private System.Timers.Timer timerPingPong;
        private System.Timers.Timer timerHello;

        // properties
        public string nickname { get; set; }
        public int port { get; set; }
        public string ipAddress { get; set; }
        public Sender sender { get; set; }
        public Receipter receipter { get; set; }
        public List<Pair> nodes { get; set; }
        public List<string> participants { get; }
        public List<PrivateChatRoom> chatRooms { get; }

        // events
        public event EventUpdateParticipants eventUpdateParticipants;
        public event EventUpdatePairs eventUpdatePairs;
        public event EventUpdateChatRooms eventUpdateChatRooms;
        public event EventReceiptedMessage eventReceiptedMessage;

        // singleton
        public static readonly ChatRoomController instance = new ChatRoomController();
        public static ChatRoomController Instance { get { return instance; } }

        private ChatRoomController() {
            port = 2323;
            nodes = new List<Pair>();
            participants = new List<string>();
            chatRooms = new List<PrivateChatRoom>();
        }

        public void startUp()
        {
            //Init connexion
            Socket socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
            initListAction();

            //Receipter
            receipter = new ReceipterImp(actions);
            receipter.startListen(port);

            //Sender
            sender = new SenderImpl(socket, port, actions);
            sender.sendHelloBroadcast();

            //Timers
            initTimerPingPong();
            initTimerHello();
        }

        private void initTimerPingPong()
        {
            timerPingPong = new System.Timers.Timer(DELAY_TIMER_PING_PONG);
            timerPingPong.AutoReset = true;
            timerPingPong.Elapsed += (s, e) =>
            {
                PingPongMessage ping = new PingPongMessage(getIpLocal(), port, Parser.TimestampNow().ToString(), true);
                sender.sendMessage(ping);
            };

            timerPingPong.Start();
        }

        private void initTimerHello()
        {
            timerHello = new System.Timers.Timer(DELAY_TIMER_PING_PONG);
            timerHello.AutoReset = true;
            timerHello.Elapsed += (s, e) =>
            {
                if(nodes.Count >= MAX_PAIR) { return; }
                HelloMessage hello = new HelloMessage(getIpLocal(), port, nodes, true);
                sender.sendMessage(hello);
            };

            timerHello.Start();
        }
        public string getIpLocal()
        {
            if (ipAddress != null)
            {
                return ipAddress;
            }

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = ip.ToString();
                    return ipAddress;
                }
            }

            throw new System.Exception("Local IP Address Not Found!");
        }

        public void addPair(Pair pair)
        {
            if (pair.Addr != getIpLocal() && !nodes.Contains(pair) && nodes.Count <= MAX_PAIR)
            {
                System.Console.WriteLine(nickname + " ADD PAIR " + pair.Addr);
                nodes.Add(pair);
                eventUpdatePairs?.Invoke();
            }
        }

        public void removePair(string addr)
        {
            System.Console.WriteLine(nickname + " REMOVE PAIR " + addr);
            foreach (Pair p in nodes)
            {
                if (p.Addr.Equals(addr))
                {
                    nodes.Remove(p);
                    eventUpdatePairs?.Invoke();
                    return;
                }
            }
        }

        public void addParticipant(string name)
        {
            if (participants.Contains(name))
            {
                return;
            }

            participants.Add(name);
            participants.Sort();

            //notify
            eventUpdateParticipants?.Invoke();

        }

        public void removeParticipant(string name)
        {
            participants.Remove(name);
            participants.Sort();

            //notify
            eventUpdateParticipants?.Invoke();
        }

        public void openPrivateRoom(string dest)
        {
            foreach(PrivateChatRoom cr in chatRooms)
            {
                if(cr.Participant == dest)
                {
                    cr.display();
                    return;
                }
            }

            //create new chatroom
            PrivateChatRoom chatRoom = new PrivateChatRoom(dest);
            chatRooms.Add(chatRoom);
            eventUpdateChatRooms?.Invoke();

            chatRoom.display();
        }

        public void onReveivedMessage(string msg, string src, string dest)
        {
            //Update main conversation 
            if(dest == null || dest.Length <= 0)
            {
                eventReceiptedMessage?.Invoke(msg, src);
                return;
            }

            //Update private room
            foreach(PrivateChatRoom cr in chatRooms)
            {
                if(cr.Participant == src)
                {
                    cr.receiveMessage(msg);
                    cr.display();
                    return;
                }
            }

            //Create new private room
            Thread th = new Thread(() =>
            {
                PrivateChatRoom chatRoom = new PrivateChatRoom(src);
                chatRooms.Add(chatRoom);
                eventUpdateChatRooms?.Invoke();

                chatRoom.display();
                chatRoom.receiveMessage(src);

                System.Windows.Threading.Dispatcher.Run();//Call in the main thread
            });
            th.SetApartmentState(ApartmentState.STA);
            th.IsBackground = true;
            th.Start();
        }

        private void initListAction()
        {
            actions.Add(new ActionGoodBye());
            actions.Add(new ActionHello());
            actions.Add(new ActionMessage());
            actions.Add(new ActionPingPong());
        }
    }
}

using MBDChat.com.unice.mbds.mbdchat.controller.action;
using MBDChat.com.unice.mbds.mbdchat.controller.utils;
using MBDChat.com.unice.mbds.mbdchat.model;
using MBDChat.com.unice.mbds.mbdchat.model.clientServer;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MBDChat.com.unice.mbds.mbdchat.controller.receipter
{
    public class ReceipterImp : Receipter
    {
        private UdpClient listener;
        private IPEndPoint ip;
        private Thread thread;
        private bool continued = false;
        private List<Action> actions;
        public event EventMessage events;
        
        public ReceipterImp(List<Action> actions) {
            this.actions = actions;
        }

        public void receiptMessage()
        {
            while (continued)
            {
                byte[] bytes = listener.Receive(ref ip);
                string json = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                Message message = Parser.parseToMessage(json);

                if(message == null)
                {
                    System.Console.WriteLine("Message null... " + json);
                    continue;
                }

                //Update actions
                foreach(Action action in actions)
                {
                    if(action.containsType(message.Type))
                    {
                        action.onReceiver(message);
                    }
                }

                //notify
                if(events != null)
                {
                    events.Invoke(json);
                }
            }
        }

        public void startListen(int port)
        {
            listener = new UdpClient(port);
            ip = new IPEndPoint(IPAddress.Any, port);

            thread = new Thread(receiptMessage);
            continued = true;
            thread.Start();
        }

        public void stopListen()
        {
            if (thread != null)
            {
                continued = false;
                thread = null;
            }

            if (listener != null)
            {
                listener.Close();
                listener = null;
            }           
        }
    }
}

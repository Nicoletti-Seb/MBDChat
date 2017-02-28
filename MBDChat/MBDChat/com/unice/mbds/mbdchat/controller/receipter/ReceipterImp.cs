using MBDChat.com.unice.mbds.mbdchat.controller.utils;
using MBDChat.com.unice.mbds.mbdchat.model;
using MBDChat.com.unice.mbds.mbdchat.model.clientServer;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
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
        public event EventMessage events;
        
        public ReceipterImp()
        {

        }

        public void receiptMessage()
        {
            while (continued)
            {
                byte[] bytes = listener.Receive(ref ip);
                String json = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                Message message = Parser.parseToMessage(json);

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

using MBDChat.com.unice.mbds.mbdchat.model.clientServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.controller.receipter
{
    public class ReceipterImp : Receipter
    {
        private UdpClient listener;
        private IPEndPoint ip;
        private Thread thread;
        private bool continued = false;

        public void receiptMessage()
        {
            while (continued)
            {
                byte[] bytes = listener.Receive(ref ip);
                String msg = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                Console.WriteLine("Message receive " + msg);
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

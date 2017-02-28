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


        //Parser json
        //TODO Create another class to Parse JSON
        private DataContractJsonSerializer jsonParser;
        private MemoryStream stream;


        public ReceipterImp()
        {
            this.jsonParser = new DataContractJsonSerializer(typeof(Message));
            stream = new MemoryStream();
        }

        public void receiptMessage()
        {
            while (continued)
            {
                byte[] bytes = listener.Receive(ref ip);
                String json = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                Message message = parseToMessage(json);

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

        private Message parseToMessage(string json)
        {
            Message deserializedMessage = new Message();

            try
            {
                stream.Position = 0;
                stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
                deserializedMessage = jsonParser.ReadObject(stream) as Message;
            }catch(Exception e)
            {
                Console.WriteLine("Error parse message :", json);
            }
            
            
            return deserializedMessage;
        }
    }
}

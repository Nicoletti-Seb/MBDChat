using MBDChat.com.unice.mbds.mbdchat.model.message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model.clientServer
{
    class ChatRoom : Receipter, Sender
    {
        private int port;
        private List<Pair> pairs;

        public ChatRoom(int port)
        {
            this.port = port;
        }

        void Sender.sendHelloBroadcast()
        {
            throw new NotImplementedException();
        }

        void Sender.sendMessage()
        {
            throw new NotImplementedException();
        }

        void Receipter.receiptMessage()
        {
            throw new NotImplementedException();
        }
    }
}

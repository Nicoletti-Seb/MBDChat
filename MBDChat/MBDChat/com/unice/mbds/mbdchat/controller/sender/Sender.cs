using MBDChat.com.unice.mbds.mbdchat.model.message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model.clientServer
{
    public interface Sender
    {
        void sendMessage(Message message);
        void sendMessage(Message message, Pair remoteEP);
    }
}

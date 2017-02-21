using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model.clientServer
{
    public interface Sender
    {
        void sendHelloBroadcast();
        void sendMessage();
    }
}

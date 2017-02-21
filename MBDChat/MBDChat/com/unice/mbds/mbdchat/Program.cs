using MBDChat.com.unice.mbds.mbdchat.model.clientServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat
{
    class Program
    {
        static void main(String[] args)
        {
            int port = 2323;

            ChatRoom chat = new ChatRoom(port);

            // faire un broadcast de hello : chat.sendHelloBroadcast
        }
    }
}

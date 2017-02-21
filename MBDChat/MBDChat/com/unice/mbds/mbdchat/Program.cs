using MBDChat.com.unice.mbds.mbdchat.model.clientServer;
using MBDChat.com.unice.mbds.mbdchat.model.message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat
{
    public class Program
    {
        static void main(String[] args)
        {
            /*ChatRoomController chatroomController = ChatRoomController.Instance;
            chatroomController.port = 2323;
            chatroomController.startUp();*/

            List<Pair> pairs = new List<Pair>();
            pairs.Add(new Pair("192.168.4.5", "2323"));
            pairs.Add(new Pair("192.168.4.50", "2323"));
            HelloData hd = new HelloData("127.0.0.1", "192.168.56.1", pairs);

            Console.WriteLine("ici : " + hd.ToString());

            Console.ReadLine();
            // faire un broadcast de hello : chat.sendHelloBroadcast
        }
    }
}

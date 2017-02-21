using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model.message
{
    public class Pair
    {
        public string addr { get; }
        public int port { get; }

        public Pair(string addr, int port)
        {
            this.addr = addr;
            this.port = port;
        }

        public IPEndPoint ep
        {
            get
            {
                IPAddress target = IPAddress.Parse(addr);
                IPEndPoint ep = new IPEndPoint(target, port);
                return ep;
            }
        }
    }
}

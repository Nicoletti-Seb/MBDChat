using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model.message
{
    class Pair
    {
        private string addr;
        private string port;

        public Pair(string addr, string port)
        {
            this.addr = addr;
            this.port = port;
        }

        public IPEndPoint ep
        {
            get
            {
                IPAddress target = IPAddress.Parse(addr);
                IPEndPoint ep = new IPEndPoint(target, Int32.Parse(port));
                return ep;
            }
        }
    }
}

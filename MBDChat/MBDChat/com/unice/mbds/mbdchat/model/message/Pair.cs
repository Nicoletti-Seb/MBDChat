using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model.message
{
    [DataContract]
    public class Pair
    {
        [DataMember]
        public string Addr { get; set; } 

        [DataMember]
        public string Port { get; set; } 

        public Pair(string addr, string port)
        {
            Addr = addr;
            Port = port;
        }

        public IPEndPoint ep
        {
            get
            {
                IPAddress target = IPAddress.Parse(Addr);
                IPEndPoint ep = new IPEndPoint(target, Int32.Parse(Port));
                return ep;
            }
        }
    }
}

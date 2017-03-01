using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model.message
{
    [DataContract(Name = "pair")]
    public class Pair
    {
        [DataMember(Name = "addr")]
        public string Addr { get; set; } 

        [DataMember(Name = "port")]
        public int Port { get; set; }

        public string Nickname { get; set; }
        
        public Pair(string addr, int port)
        {
            Addr = addr;
            Port = port;
            Nickname = addr;
        }

        public Pair(string addr, int port, string nickname)
        {
            Addr = addr;
            Port = port;
            Nickname = nickname;
        }

        public IPEndPoint ep
        {
            get
            {
                IPAddress target = IPAddress.Parse(Addr);
                IPEndPoint ep = new IPEndPoint(target, Port);
                return ep;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model.message
{
    [AttributeMessage("PING/PONG")]
    [DataContract]
    public class PingPongMessage : Message
    {
        [DataMember(Name = "addr_source")]
        public string AddrSrc { get; set; } 

        [DataMember(Name = "port_source")]
        public int PortSrc { get; set; }

        [DataMember(Name = "timestamp")]
        public string Timestamp { get; set; }

        public PingPongMessage(string addr_source, int port, string timestamp): base("PING/PONG")
        {
            AddrSrc = addr_source;
            PortSrc = port;
            Timestamp = timestamp;
        }
    }
}

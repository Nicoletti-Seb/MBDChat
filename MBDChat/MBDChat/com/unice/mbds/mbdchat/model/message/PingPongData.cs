using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model.message
{
    [DataContract]
    public class PingPongData : PayLoad
    {
        [DataMember]
        public string Addr_source { get; set; } 

        [DataMember]
        public int Port { get; set; }

        [DataMember]
        public string Timestamp { get; set; }

        public PingPongData(string addr_source, int port, string timestamp)
        {
            Addr_source = addr_source;
            Port = port;
            Timestamp = timestamp;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model.message
{
    [DataContract]
    [KnownType(typeof(PingPongData))]
    public class PingPongData : PayLoad
    {
        [DataMember]
        public string Addr_source { get; set; } 

        [DataMember]
        public string Timestamp { get; set; }

        public PingPongData(string addr_source, string timestamp)
        {
            Addr_source = addr_source;
            Timestamp = timestamp;
        }
    }
}

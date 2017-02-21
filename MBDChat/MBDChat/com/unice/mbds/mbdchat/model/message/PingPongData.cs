using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model.message
{
    [DataContract]
    class PingPongData : PayLoad
    {
        [DataMember]
        private string Addr_source { get; set; } 

        [DataMember]
        private string Timestamp { get; set; }

        public PingPongData(string addr_source, string timestamp)
        {
            Addr_source = addr_source;
            Timestamp = timestamp;
        }
    }
}

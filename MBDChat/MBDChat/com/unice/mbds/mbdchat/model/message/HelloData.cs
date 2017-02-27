using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model.message
{
    [DataContract]
    [KnownType(typeof(HelloData))]
    public class HelloData : PayLoad
    {
        [DataMember]
        public string Addr_source { get; set; } 

        [DataMember]
        public int Port_source { get; set; } 

        [DataMember] 
        public List<Pair> Pairs { get; set; }

        public HelloData(string addr_source, int port_source, List<Pair> pairs)
        {
            Addr_source = Addr_source;
            Port_source = Port_source;
            Pairs = pairs;
        }
    }
}

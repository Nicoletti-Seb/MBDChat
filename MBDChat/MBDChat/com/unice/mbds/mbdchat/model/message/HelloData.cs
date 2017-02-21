using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model.message
{
    [DataContract]
    public class HelloData : PayLoad
    {
        [DataMember]
        public string Addr_source { get; set; } 

        [DataMember]
        private string Port_source { get; set; } 

        [DataMember] 
        private List<Pair> Pairs { get; set; } 

        public HelloData(string addr_source, string port_source, List<Pair> pairs)
        {
            Addr_source = addr_source;
            Port_source = port_source;
            Pairs = pairs;
        }
    }
}

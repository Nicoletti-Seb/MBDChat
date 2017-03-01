using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MBDChat.com.unice.mbds.mbdchat.model.message
{
    [AttributeMessage("HELLO_A")]
    [AttributeMessage("HELLO_R")]
    [DataContract]
    public class HelloMessage : Message
    {
        [DataMember(Name = "addr_source")]
        public string Addr_source { get; set; } 

        [DataMember(Name = "port_source")]
        public int Port_source { get; set; } 

        [DataMember(Name = "pairs")] 
        public List<Pair> Pairs { get; set; }

        public HelloMessage(string addr_source, int port_source, List<Pair> pairs) : base("HELLO_A")
        {
            Addr_source = addr_source;
            Port_source = port_source;
            Pairs = pairs;
        }
    }
}

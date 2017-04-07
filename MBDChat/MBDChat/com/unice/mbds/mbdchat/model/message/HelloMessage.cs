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
        public ICollection<Pair> Pairs { get; set; }

        public HelloMessage(string addr_source, int port_source, ICollection<Pair> pairs, bool isHelloA) : base(isHelloA?"HELLO_A":"HELLO_R")
        {
            Addr_source = addr_source;
            Port_source = port_source;
            Pairs = pairs;
        }
    }
}

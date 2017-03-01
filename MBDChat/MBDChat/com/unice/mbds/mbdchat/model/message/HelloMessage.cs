﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MBDChat.com.unice.mbds.mbdchat.model.message
{
    [AttributeMessage("HELLO_A")]
    [AttributeMessage("HELLO_R")]
    [DataContract]
    public class HelloMessage : Message
    {
        [DataMember]
        public string Addr_source { get; set; } 

        [DataMember]
        public int Port_source { get; set; } 

        [DataMember] 
        public List<Pair> Pairs { get; set; }

        public HelloMessage(string addr_source, int port_source, List<Pair> pairs) : base("HELLO")
        {
            Addr_source = Addr_source;
            Port_source = Port_source;
            Pairs = pairs;
        }
    }
}
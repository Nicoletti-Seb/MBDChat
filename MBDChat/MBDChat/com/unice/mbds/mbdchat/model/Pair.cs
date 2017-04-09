﻿using System;
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
        
        public Pair(string addr, int port)
        {
            Addr = addr;
            Port = port;
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

        public override bool Equals(object obj)
        {
            if(this == obj){ return true; }
            if(obj.GetType() != typeof(Pair)) { return false; }

            Pair pair = (Pair)obj;
            return pair.Port == Port && pair.Addr == Addr;
        }

        public override string ToString()
        {
            return Addr + ":" + Port;
        }
    }
}

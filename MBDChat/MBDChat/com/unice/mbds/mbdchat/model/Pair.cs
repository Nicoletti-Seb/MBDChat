using System;
using System.Net;
using System.Runtime.Serialization;

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
                try
                {
                    IPAddress target = IPAddress.Parse(Addr);
                    IPEndPoint ep = new IPEndPoint(target, Port);
                    return ep;
                } catch(FormatException e)
                {
                    return null;
                }
                
                
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

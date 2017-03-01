using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model.message
{
    [AttributeMessage("GOODBYE")]
    [DataContract(Name ="GOODBYE")]
    public class GoodByeMessage : Message
    {
        [DataMember(Name = "addr_source")]
        public string AddrSrc { get; set; }

        [DataMember(Name = "nickname")]
        public string Nickname { get; set; }


        public GoodByeMessage(string addr) : base("GOODBYE")
        {
            AddrSrc = addr;
        }
    }
}

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
        public string Addr { get; set; }

        public GoodByeMessage(string addr) : base("GOODBYE")
        {
            Addr = addr;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model.message
{
    public class GoodByeData : PayLoad
    {
        public string Addr { get; set; }

        public GoodByeData(string addr)
        {
            Addr = addr;
        }
    }
}

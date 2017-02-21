using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model.message
{
    public class GoodByeData : PayLoad
    {
        private string addr;

        public GoodByeData(string addr)
        {
            this.addr = addr;
        }
    }
}

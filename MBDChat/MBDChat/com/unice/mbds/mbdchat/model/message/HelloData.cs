using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model.message
{
    class HelloData : PayLoad
    {
        private string addr_source;
        private string port_source;
        private List<Pair> pairs = new List<Pair>();
    }
}

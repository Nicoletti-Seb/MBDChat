using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model
{
    public class Message
    {
        private string type;
        private PayLoad data;

        public Message(string type, PayLoad payload)
        {
            this.type = type;
            this.data = payload;
        }
    }
}

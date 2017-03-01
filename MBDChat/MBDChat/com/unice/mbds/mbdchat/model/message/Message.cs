using MBDChat.com.unice.mbds.mbdchat.model.message;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public string Type { get; set; }

        public Message() { }

        public Message(string type)
        {
            Type = type;
        }
    }
}

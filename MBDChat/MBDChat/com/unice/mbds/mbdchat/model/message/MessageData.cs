using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model.message
{
    [DataContract]
    [KnownType(typeof(MessageData))]
    public class MessageData : PayLoad
    {
        [DataMember]
        public string Nickname { get; set; } 

        [DataMember]
        public string Msg { get; set; } 

        [DataMember]
        public string Timestamp { get; set; } 

        [DataMember]
        public string Destinataire { get; set; } 

        [DataMember]
        public string Hash { get; set; } 

        [DataMember]
        public string Rootedby { get; set; } 

        public MessageData(string nickname, string msg, string timestamp, string destinataire, string hash, string rootedby)
        {
            Nickname = nickname;
            Msg = msg;
            Timestamp = timestamp;
            Destinataire = destinataire;
            Hash = hash;
            Rootedby = rootedby;
        }
    }
}

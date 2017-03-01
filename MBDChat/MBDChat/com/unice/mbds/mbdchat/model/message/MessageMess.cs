using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model.message
{
    [AttributeMessage("MESSAGE")]
    [DataContract]
    public class MessageMess : Message
    {
        [DataMember(Name = "nickname")]
        public string Nickname { get; set; } 

        [DataMember(Name = "msg")]
        public string Msg { get; set; } 

        [DataMember(Name = "timestamp")]
        public string Timestamp { get; set; } 

        [DataMember(Name = "destinataire")]
        public string Destinataire { get; set; } 

        [DataMember(Name = "hash")]
        public string Hash { get; set; } 

        [DataMember(Name = "Rootedby")]
        public string Rootedby { get; set; } 

        public MessageMess(string nickname, string msg, string timestamp, string destinataire, string hash, string rootedby)
             : base("MESSAGE")
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

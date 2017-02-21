using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model.message
{
    [DataContract]
    public class MessageData : PayLoad
    {
        [DataMember]
        private string Nickname { get; set; } 

        [DataMember]
        private string Msg { get; set; } 

        [DataMember]
        private string Timestamp { get; set; } 

        [DataMember]
        private string Destinataire { get; set; } 

        [DataMember]
        private string Hash { get; set; } 

        [DataMember]
        private string Rootedby { get; set; } 

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

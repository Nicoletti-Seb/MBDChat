using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBDChat.com.unice.mbds.mbdchat.model.message;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace MBDChat.com.unice.mbds.mbdchat.model
{
    [DataContract]
    [KnownType(typeof(HelloData))]
    [KnownType(typeof(GoodByeData))]
    [KnownType(typeof(MessageData))]
    [KnownType(typeof(PingPongData))]
    public abstract class PayLoad
    {
        
    }
}

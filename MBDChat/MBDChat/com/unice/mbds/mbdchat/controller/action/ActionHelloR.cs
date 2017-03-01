using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBDChat.com.unice.mbds.mbdchat.model;
using MBDChat.com.unice.mbds.mbdchat.model.message;
using MBDChat.com.unice.mbds.mbdchat.model.clientServer;

namespace MBDChat.com.unice.mbds.mbdchat.controller.action
{
    public class ActionHelloR : Action
    {
        public ActionHelloR() : base("HELLO_R"){}

        public override void onReceiver(Message message)
        {
            base.onReceiver(message);

            HelloMessage helloMsg = (HelloMessage)message;

            foreach(Pair pair in helloMsg.Pairs)
            {
                controller.addPair(pair);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBDChat.com.unice.mbds.mbdchat.model;
using MBDChat.com.unice.mbds.mbdchat.model.message;

namespace MBDChat.com.unice.mbds.mbdchat.controller.action
{
    public class ActionGoodBye: Action
    {
        public ActionGoodBye() : base("GOODBYE"){}

        public override void onReceiver(Message message)
        {
            base.onReceiver(message);

            // remove from nodes
            string addr = ((GoodByeMessage)message).Addr;
            controller.removePair(addr);
        }
    }
}

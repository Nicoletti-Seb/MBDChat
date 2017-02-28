using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBDChat.com.unice.mbds.mbdchat.model;

namespace MBDChat.com.unice.mbds.mbdchat.controller.action
{
    public class ActionHello : Action
    {
        public ActionHello(string type) : base(type){}

        public override void onSender(Message message)
        {
            base.onSender(message);

            controller.sender.sendMessage(message);
        }

        public override void onReceiver(Message message)
        {
            base.onReceiver(message);


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBDChat.com.unice.mbds.mbdchat.model;

namespace MBDChat.com.unice.mbds.mbdchat.controller.action
{
    public class ActionMessage: Action
    {
        public ActionMessage(string type) : base(type){}

        public override void update(Message message)
        {
            base.update(message);
        }
    }
}

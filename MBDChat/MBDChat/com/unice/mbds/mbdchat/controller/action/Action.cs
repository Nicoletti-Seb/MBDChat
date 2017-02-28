using MBDChat.com.unice.mbds.mbdchat.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.controller.action
{
    public abstract class Action
    {
        public string Type { get; }

        public List<Message> History;

        public Action(string type)
        {
            this.Type = type;
            this.History = new List<Message>();
        }

        public virtual void update(Message message)
        {
            History.Add(message);
        }
    }
}

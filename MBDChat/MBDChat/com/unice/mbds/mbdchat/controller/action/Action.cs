using MBDChat.com.unice.mbds.mbdchat.model;
using MBDChat.com.unice.mbds.mbdchat.model.clientServer;
using MBDChat.com.unice.mbds.mbdchat.model.message;
using System;
using System.Collections.Generic;

namespace MBDChat.com.unice.mbds.mbdchat.controller.action
{
    public struct ActionHistorySender
    {
        public ActionHistorySender (Message message, Pair pair)
        {
            this.message = message;
            this.pair = pair;
        }

        public readonly Message message;
        public readonly Pair pair;
    }

    public abstract class Action
    {
        protected ChatRoomController controller = ChatRoomController.Instance;

        public string [] Types { get; set; }

        public List<ActionHistorySender> HistorySender;
        public List<Message> HistoryReceiver;

        public Action(string[] type)
        {
            this.Types = type;
            this.HistorySender = new List<ActionHistorySender>();
            this.HistoryReceiver = new List<Message>();
        }

        public bool containsType(String type)
        {
            foreach(String t in Types)
            {
                if(type == t)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual void onSender(Message message, Pair dest)
        {
            HistorySender.Add(new ActionHistorySender(message, dest));
        }

        public virtual void onReceiver(Message message)
        {
            HistoryReceiver.Add(message);
        }
    }
}

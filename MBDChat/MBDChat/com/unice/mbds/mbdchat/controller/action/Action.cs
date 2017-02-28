using MBDChat.com.unice.mbds.mbdchat.model;
using MBDChat.com.unice.mbds.mbdchat.model.clientServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.controller.action
{
    public abstract class Action
    {
        protected ChatRoomController controller = ChatRoomController.Instance;

        public string Type { get; set; }

        public List<Message> HistorySender;
        public List<Message> HistoryReceiver;

        public Action(string type)
        {
            this.Type = type;
            this.HistorySender = new List<Message>();
            this.HistoryReceiver = new List<Message>();
        }

        public virtual void onSender(Message message)
        {
            HistorySender.Add(message);
        }

        public virtual void onReceiver(Message message)
        {
            HistoryReceiver.Add(message);
        }
    }
}

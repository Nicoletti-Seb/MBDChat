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
    public class ActionPingPong: Action
    {
        public ActionPingPong(string type) : base(type){}

        public override void onSender(Message message)
        {
            base.onSender(message);

            controller.sender.sendPingBroadcast();
        }

        public override void onReceiver(Message message)
        {
            base.onReceiver(message);

            string addr = ((PingPongData)message.Data).Addr_source;
            // timestamp pour mesure la latence

            Message msgToSend = new Message("PING/PONG", new PingPongData(controller.getIpLocal(), ChatRoomController.UnixTimestampFromDateTimeNow().ToString()));
            controller.sender.sendMessage(msgToSend);
        }
    }
}

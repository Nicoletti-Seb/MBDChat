using MBDChat.com.unice.mbds.mbdchat.model;
using MBDChat.com.unice.mbds.mbdchat.model.message;

namespace MBDChat.com.unice.mbds.mbdchat.controller.action
{
    public class ActionGoodBye : Action
    {
        private static readonly string[] TYPES = new string[] { "GOODBYE" };

        public ActionGoodBye() : base(TYPES) {}

        public override void onReceiver(Message message)
        {
            base.onReceiver(message);

            // remove from nodes
            string addr = ((GoodByeMessage)message).AddrSrc;
            controller.removePair(addr);
        }
    }
}

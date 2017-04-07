using MBDChat.com.unice.mbds.mbdchat.model;
using MBDChat.com.unice.mbds.mbdchat.model.message;

namespace MBDChat.com.unice.mbds.mbdchat.controller.action
{
    public class ActionHello : Action
    {
        private static readonly string[] TYPES = new string[] { "HELLO_A", "HELLO_R" };

        public ActionHello() : base(TYPES) { }


        public override void onReceiver(Message message)
        {
            base.onReceiver(message);

            if (message.Type == "HELLO_A")
            {
                onReceivedHelloA((HelloMessage)message);
            } else
            {
                onReceivedHelloR((HelloMessage)message);
            }
        }

        private void onReceivedHelloA(HelloMessage msgReceived)
        {
            Pair senderAddr = new Pair(msgReceived.Addr_source, msgReceived.Port_source);

            controller.addPair(senderAddr);

            //Create HELLO_R
            HelloMessage helloMsg = new HelloMessage(controller.getIpLocal(), controller.port, controller.nodes, false);

            //Send
            controller.sender.sendMessage(helloMsg, senderAddr);
        }

        private void onReceivedHelloR(HelloMessage helloMsg)
        {
            foreach (Pair pair in helloMsg.Pairs)
            {
                controller.addPair(pair);
            }
        }
    }
}

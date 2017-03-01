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
    public class ActionHelloA : Action
    {
        public ActionHelloA() : base("HELLO_A"){}


        public override void onReceiver(Message message)
        {
            base.onReceiver(message);

            //Create sender's addresse
            HelloMessage msgReceived = (HelloMessage)message;
            Pair senderAddr = new Pair(msgReceived.Addr_source, msgReceived.Port_source);

            //Create HELLO_R
            HelloMessage helloMsg = new HelloMessage(controller.getIpLocal(), controller.port, controller.nodes);
            helloMsg.Type = "HELLO_R";

            //Send
            controller.sender.sendMessage(helloMsg, senderAddr);
        }
    }
}

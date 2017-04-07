using System;
using System.Collections.Generic;
using MBDChat.com.unice.mbds.mbdchat.model;
using MBDChat.com.unice.mbds.mbdchat.model.message;
using MBDChat.com.unice.mbds.mbdchat.controller.utils;

namespace MBDChat.com.unice.mbds.mbdchat.controller.action
{
    public class ActionPingPong : Action
    {
        private static readonly string[] TYPES = new string[] { "PING", "PONG" };

        public ActionPingPong() : base(TYPES) { }

        private List<Pair> nodesConnected = new List<Pair>();

        public override void onSender(Message message, Pair dest)
        {
            base.onSender(message, dest);

            if (message.Type == "PONG")
            {
                return;
            }

            if (nodesConnected.Contains(dest))
            {
                Console.WriteLine("Ping/Pong " + controller.nickname + " disconnect " + dest);

                //Disconnect pair
                nodesConnected.Remove(dest);
                controller.removePair(dest.Addr);
            }
            else
            {
                nodesConnected.Add(dest);
            }
        }

        public override void onReceiver(Message message)
        {
            base.onReceiver(message);

            if (message.Type == "PING")
            {
                onReceivedPing((PingPongMessage)message);
            }
            else
            {
                onReceivedPong((PingPongMessage)message);
            }
        }

        private void onReceivedPing(PingPongMessage pongMessage)
        {
            Console.WriteLine(controller.nickname + "OnReceive PING");

            //Send Pong
            PingPongMessage response = new PingPongMessage(controller.getIpLocal(), controller.port, Parser.TimestampNow().ToString(), false);
            Pair dest = new Pair(pongMessage.AddrSrc, pongMessage.PortSrc);
            controller.sender.sendMessage(response, dest);
        }

        private void onReceivedPong(PingPongMessage pongMessage)
        {
            Console.WriteLine(controller.nickname + "OnReceive PONG");
            Pair pairMsg = new Pair(pongMessage.AddrSrc, pongMessage.PortSrc);
            nodesConnected.Remove(pairMsg);
        }
    }
}

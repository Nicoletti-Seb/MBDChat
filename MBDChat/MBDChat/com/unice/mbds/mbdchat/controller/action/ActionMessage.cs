using System;
using MBDChat.com.unice.mbds.mbdchat.model;
using MBDChat.com.unice.mbds.mbdchat.model.message;

namespace MBDChat.com.unice.mbds.mbdchat.controller.action
{
    public class ActionMessage: Action
    {
        private static readonly string[] TYPES = new string[] { "MESSAGE" };

        public ActionMessage() : base(TYPES){}

        public override void onReceiver(Message message)
        {
            MessageMess newMess = (MessageMess)message;
            if (haveAlreadyMessage(newMess) || newMess.Nickname == controller.nickname) {
                Console.WriteLine(controller.nickname + " Already msg ", newMess.Rootedby);
                return;
            }
            else {
                Console.WriteLine(controller.nickname + " new msg ", newMess.Rootedby);
                base.onReceiver(message);
            }

            bool isPrivateMess = isPrivateMessage(newMess.Destinataire);
            string nicknameDest = getNickname(newMess.Destinataire);

            if (isPrivateMess && nicknameDest == controller.nickname)
            {
                //Message for me in private room
                Console.WriteLine("Private room " + newMess.Msg);
                controller.onReveivedMessage(newMess.Msg, newMess.Nickname, nicknameDest);
                return;
            }
            else if(nicknameDest.Length <= 0)
            {
                //Message for me in main room
                Console.WriteLine("Update main room " + newMess.Msg);
                controller.onReveivedMessage(newMess.Msg, newMess.Nickname, nicknameDest);
            }

            //Update RootedBy field
            if(newMess.Rootedby.Length  <= 0) { newMess.Rootedby += controller.nickname; }
            else { newMess.Rootedby += "," + controller.nickname; }

            //Send message with new RootedBy field
            controller.sender.sendMessage(newMess);
        }

        private string getNickname(string destinataire)
        {
            if (!isPrivateMessage(destinataire)) { return destinataire; }

            return destinataire.Substring(1);
        }

        private bool isPrivateMessage(string destinataire)
        {
            if(destinataire.Length <= 0) { return false; }

            return destinataire[0] == '@';
        }
        

        private bool haveAlreadyMessage(MessageMess messToCheck)
        {
            string rootedBy = messToCheck.Rootedby;
            foreach(Message msg in HistoryReceiver)
            {
                MessageMess mess = (MessageMess)msg;
                if(mess.Hash == messToCheck.Hash) { return true;}
            }

            return false;
        }
    }
}

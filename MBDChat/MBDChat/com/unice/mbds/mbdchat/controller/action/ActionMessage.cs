using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBDChat.com.unice.mbds.mbdchat.model;
using MBDChat.com.unice.mbds.mbdchat.model.message;

namespace MBDChat.com.unice.mbds.mbdchat.controller.action
{
    public class ActionMessage: Action
    {
        public ActionMessage() : base("MESSAGE"){}

        public override void onReceiver(Message message)
        {
            MessageMess newMess = (MessageMess)message;
            if (haveAlreadyMessage(newMess)) { return; }
            else { base.onReceiver(message); }

            bool isPrivateMess = isPrivateMessage(newMess.Destinataire);
            string nickname = getNickname(newMess.Destinataire);

            if (isPrivateMess && nickname == controller.nickname)
            {
                //private room
                Console.WriteLine("Private room " + newMess.Msg);
                return;
            }
            else if( nickname.Length > 0)
            {
                //Update nickname room 
                Console.WriteLine("Update room " + newMess.Msg);
            }

            if(newMess.Rootedby.Length  <= 0) { newMess.Rootedby += controller.nickname; }
            else { newMess.Rootedby += "," + controller.nickname; }

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
            foreach(Message msg in HistoryReceiver)
            {
                MessageMess mess = (MessageMess)msg;
                if(mess.Hash == messToCheck.Hash) { return true;}
            }

            return false;
        }
    }
}

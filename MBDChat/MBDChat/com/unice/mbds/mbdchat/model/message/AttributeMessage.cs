using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBDChat.com.unice.mbds.mbdchat.model
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]//Multiple attributes
    public class AttributeMessage: Attribute
    {
        public string name;
        public AttributeMessage(string name)
        {
            this.name = name;
        }
    }
}

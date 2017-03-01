using MBDChat.com.unice.mbds.mbdchat.model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;

namespace MBDChat.com.unice.mbds.mbdchat.controller.utils
{
    class Parser
    {
        private static MemoryStream stream;
        private static StreamReader streamReader;

        public static string parseToJson(Message message)
        {
            DataContractJsonSerializer jsonParser = new DataContractJsonSerializer(message.GetType());
            Console.WriteLine(typeof(Message));
            stream = new MemoryStream();
            streamReader = new StreamReader(stream);
            jsonParser.WriteObject(stream, message);
            stream.Position = 0;

            string result = streamReader.ReadToEnd();
            Console.WriteLine(result);

            stream.Flush();
            return result;
        }

        public static Message parseToMessage(string json)
        {
            Message deserializedMessage = new Message();

            dynamic obj = JObject.Parse(json);
            Assembly ass = typeof(Message).Assembly;
            string typeObj = obj.Type;
            Type type = searchTypeMessageLoad(ass , typeObj);

            if(type == null)
            {
                Console.WriteLine("Message with type = " + obj.Type + " not found...");
                return null;
            }

            DataContractJsonSerializer jsonParser = new DataContractJsonSerializer(type);
            try
            {
                stream.Position = 0;
                stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
                deserializedMessage = jsonParser.ReadObject(stream) as Message;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error parse message : " + json + "\nException : " + e.ToString());
            }


            return deserializedMessage;
        }

        public static long TimestampNow()
        {
            long unixTimestamp = DateTime.Now.Ticks - new DateTime(1970, 1, 1).Ticks;
            unixTimestamp /= TimeSpan.TicksPerSecond;
            return unixTimestamp;
        }

        public static long TimestampFromDateTime(DateTime date)
        {
            long unixTimestamp = date.Ticks - new DateTime(1970, 1, 1).Ticks;
            unixTimestamp /= TimeSpan.TicksPerSecond;
            return unixTimestamp;
        }

        public static SHA256 mySHA256 = new SHA256Managed();

        public static string toHash(string type, string nickname, string message, string timestamp, string dest)
        {
            byte[] hash = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(type + nickname + message + timestamp + dest));

            StringBuilder res = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                res.Append(String.Format("{0:X2}", hash[i]));
            }
            Console.WriteLine("hash:" + res.ToString());
            return res.ToString();
        }


        //Search dynamic type message
        public static Type searchTypeMessageLoad(Assembly assembly, string nameType)
        {   
            foreach (Type type in assembly.GetTypes())
            {
                IEnumerable<AttributeMessage> allMessAttr = type.GetCustomAttributes<AttributeMessage>();
                foreach(AttributeMessage attr in allMessAttr)
                {
                    if(attr.name == nameType)
                    {
                        return type;
                    }
                }
            }

            return null;
        }
    }
}

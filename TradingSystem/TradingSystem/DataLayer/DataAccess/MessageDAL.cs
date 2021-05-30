using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer.DataObjects;

namespace TradingSystem.DataLayer.DataAccess
{
    public class MessageDAL
    {
        private static List<MessageData> messages = new List<MessageData>();

        public static MessageData getMessage(string SenderName, string StoreToSend, string UserToSend)
        {
            foreach (MessageData messageData in messages)
            {
                if (messageData.SenderName == SenderName && messageData.StoreToSend == StoreToSend && messageData.UserToSend == UserToSend)
                    return messageData;
            }
            return null;
        }


        public static bool isExist(string SenderName, string StoreToSend, string UserToSend)
        {
            foreach (MessageData messageData in messages)
            {
                if (messageData.SenderName == SenderName && messageData.StoreToSend == StoreToSend && messageData.UserToSend == UserToSend)
                    return true;
            }
            return false;

        }

        public static void addMessage(MessageData messageData)
        {
            messages.Add(messageData);
        }

        public static bool update(MessageData messageData)
        {
            if (!messages.Remove(messageData))
                return false;
            messages.Add(messageData);
            return true;

        }

        public static bool remove(MessageData messageData)
        {
            return messages.Remove(messageData);
        }
    }
}

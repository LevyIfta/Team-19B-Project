﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.commerce;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer.User
{
    public class Message
    {
        //FIELDS
        public string SenderName { get; set; }
        public string StoreToSend { get; set; }
        public string UserToSend { get; set; }
        public string Msg { get; set; }
        public bool isNew { get; set; }

        public Message(string SenderName, string StoreToSend, string UserToSend, string Message, bool isNew)
        {
            this.SenderName = SenderName;
            this.StoreToSend = StoreToSend;
            this.UserToSend = UserToSend;
            this.Msg = Message;
            this.isNew = isNew;
        }

        public Message(MessageData message)
        {
            this.SenderName = message.sender;
            if (message.StoreToSend == null)
                this.StoreToSend = null;
            else
                this.StoreToSend = message.StoreToSend.storeName;
            if (message.UserToSend == null)
                this.UserToSend = null;
            else
                this.UserToSend = message.UserToSend.userName;
            this.Msg = message.Message;
            this.isNew = false;
            if (message.isNew.Equals("True"))
                this.isNew = true;

        }

        public MessageData toDataObject()
        {
            StoreData store = null;
            MemberData reciver = null;
            if (StoreToSend == null)
                reciver = UserServices.getUser(UserToSend).toDataObject();
            else
                store = Stores.searchStore(StoreToSend).toDataObject();

            return new MessageData(SenderName, store, reciver, this.Msg, this.isNew.ToString());
        }

        public bool sendMessage(string SenderName, string StoreToSend, string UserToSend, string Message)
        {
            return false;
        }
        
    }
}

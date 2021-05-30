using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer.DataObjects
{
    public class MessageData
    {
        //FIELDS
        public string SenderName { get; set; }
        public string StoreToSend { get; set; }
        public string UserToSend { get; set; }
        public string Message { get; set; }

        //CONSTRUCTORS
        public MessageData(string SenderName, string StoreToSend, string UserToSend, string Message)
        {
            this.SenderName = SenderName;
            this.StoreToSend = StoreToSend;
            this.UserToSend = UserToSend;
            this.Message = Message;
        }

        //EQUALS
        public override bool Equals(object obj)
        {
            return false;
        }

        public bool Equals(MessageData other)
        {
            return this.SenderName.Equals(other.SenderName) & this.StoreToSend.Equals(other.StoreToSend) & this.UserToSend.Equals(other.UserToSend) & this.Message.Equals(other.Message);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.User
{
    public class Message
    {
        //FIELDS
        public string SenderName { get; set; }
        public string StoreToSend { get; set; }
        public string UserToSend { get; set; }
        public string Msg { get; set; }

        public Message(string SenderName, string StoreToSend, string UserToSend, string Message)
        {
            this.SenderName = SenderName;
            this.StoreToSend = StoreToSend;
            this.UserToSend = UserToSend;
            this.Msg = Message;
        }
        
    }
}

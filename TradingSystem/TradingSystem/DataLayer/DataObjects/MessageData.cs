using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    public class MessageData
    {
        //FIELDS
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public virtual string sender { get; set; }
        public virtual StoreData StoreToSend { get; set; }
        public virtual MemberData  UserToSend { get; set; }
        public string Message { get; set; }
        public string isNew { get; set; }

        //CONSTRUCTORS
        public MessageData(string sender, StoreData StoreToSend, MemberData UserToSend, string Message, string isNew)
        {
            this.sender = sender;
            this.StoreToSend = StoreToSend;
            this.UserToSend = UserToSend;
            this.Message = Message;
            this.isNew = isNew;
        }

        public MessageData()
        {
        }

        //EQUALS
        public override bool Equals(object obj)
        {
            return false;
        }

        public bool Equals(MessageData other)
        {
            return this.Id == other.Id;
        }
    }
}

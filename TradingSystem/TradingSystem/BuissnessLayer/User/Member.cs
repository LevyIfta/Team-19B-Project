using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.User.Permmisions;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer
{
    class Member : aUser
    {
        public string userName { get; set; }
        public string password { get; set; }

        public ICollection<Reciept> reciepts { get; set; }
        public basePermmision permmisions { get; set; }

        public override string getUserName()
        {
            return userName;
        }

        override
        public object todo(PersmissionsTypes func, object[] args)
        {
            return permmisions.todo(func, args);
        }
        override
        public bool EstablishStore(string storeName)
        {
            throw new NotImplementedException();
        }

        public override bool purchase(PaymentMethod payment)
        {
            //also save recipt
            return false;
        }

        public static Member dataToObject(memberData data)
        {
            if(data == null)
                return null;
            throw new NotImplementedException();
        }
        public static memberData objectToData(Member member)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// call everytime you chane anything in the user data
        /// </summary>
        protected override void update()
        {
            UserDAL.update(new memberData(userName, password), objectToData(this));
        }


    }
}

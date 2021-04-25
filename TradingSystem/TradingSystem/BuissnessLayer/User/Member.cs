using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.User.Permmisions;
using TradingSystem.DataLayer;
using TradingSystem.BuissnessLayer.commerce;

namespace TradingSystem.BuissnessLayer
{
    public class Member : aUser
    {
        public string userName { get; set; }
        public string password { get; set; }

        public ICollection<Receipt> reciepts { get; set; }
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
            Stores.addStore(storeName, this);
            permmisions = new basePermmision("", null);
            aPermission temp1 = new addProduct(storeName, null);
            aPermission temp2 = new editManagerPermissions(storeName, null);
            aPermission temp3 = new editProduct(storeName, null);
            aPermission temp4 = new getInfoEmployees(storeName, null);
            aPermission temp5 = new getPurchaseHistory(storeName, null);
            aPermission temp6 = new hireNewStoreManager(storeName, null);
            aPermission temp7 = new hireNewStoreOwner(storeName, null);
            aPermission temp8 = new removeManager(storeName, null);
            aPermission temp9 = new removeProduct(storeName, null);
            permmisions.addPermission(temp1);
            permmisions.addPermission(temp2);
            permmisions.addPermission(temp3);
            permmisions.addPermission(temp4);
            permmisions.addPermission(temp5);
            permmisions.addPermission(temp6);
            permmisions.addPermission(temp7);
            permmisions.addPermission(temp8);
            permmisions.addPermission(temp9);
            return true;
        }

        public override ICollection<Receipt> purchase(PaymentMethod payment)
        {
            ICollection<Receipt> list = new List<Receipt>();
            foreach (ShoppingBasket basket in myCart.baskets)
            {
                Receipt receipt = basket.store.executePurchase(basket, payment);
                if (receipt == null)
                    return null;
                list.Add(receipt);
            }
            return list;
        }
        public bool editPermission(string storeName, string userSponser, aPermission permission)
        {
            if (!removePermission(storeName, userName))
                return false;
            addPermission(permission);
            return true;
            
        }
        public void addPermission(aPermission permission)
        {
            if(permmisions == null)
                permmisions = new basePermmision("", null);
            permmisions.addPermission(permission);
        }
        public bool removePermission(string storeName, string userSponser)
        {
            if (permmisions == null)
                return false;
            aPermission start = null;
            aPermission corrent = permmisions.next;

            while (corrent != null)
            {
                if (corrent.store.Equals(storeName) && corrent.sponser.Equals(userSponser))
                {
                    if (start == null)
                        start = permmisions;
                }
                else
                {
                    if (start != null)
                        start = corrent;
                    else
                    {
                        start.next = corrent;
                        return true;
                    }
                }
                corrent = corrent.next;
            }
            if (start != null)
            {
                start.next = null;
                return true;
            }
            return false;
        }
        public override ICollection<Receipt> getPurchHistory()
        {
            return reciepts;
        }
        public override bool addNewProduct(string storeName, string productName, double price, int amount, string category, string manufacturer)
        {
            object[] args = new object[] { storeName , productName, price, amount, category, manufacturer };
            return (bool)todo(PersmissionsTypes.AddProduct, args);
        }
        public override bool removeProduct(string storeName, string productName, string manufacturer)
        {
            object[] args = new object[] { storeName, productName, manufacturer };
            return (bool)todo(PersmissionsTypes.RemoveProduct, args);
        }
        public override bool editProduct(string storeName, int productName, double price, string manufacturer)
        {
            object[] args = new object[] { storeName, productName, price, manufacturer };
            return (bool)todo(PersmissionsTypes.EditProduct, args);
        }
        public override bool editManagerPermissions(string storeName, string username, List<PersmissionsTypes> Permissions)
        {
            object[] args = new object[] { storeName, username, this.userName, aPermission.converPer(storeName, this.userName, Permissions) };
            return (bool)todo(PersmissionsTypes.EditManagerPermissions, args);
        }
        public override ICollection<Receipt> getMyPurchaseHistory(string storeName)
        {
            ICollection<Receipt> list = new List<Receipt>();
            foreach(Receipt receipt in reciepts)
            {
                if (receipt.store.Equals(storeName))
                    list.Add(receipt);
            }
            return list;
        }
        public override ICollection<Receipt> getPurchaseHistory(string storeName)
        {
            object[] args = new object[] { storeName };
            return (ICollection<Receipt>)todo(PersmissionsTypes.GetPurchaseHistory, args);
        }
        public override bool hireNewStoreManager(string storeName, string username)
        {
            object[] args = new object[] { storeName, username, this.userName};
            return (bool)todo(PersmissionsTypes.HireNewStoreManager, args);
        }
        public override bool hireNewStoreOwner(string storeName, string username, List<PersmissionsTypes> Permissions)
        {
            object[] args = new object[] { storeName, username, aPermission.converPer(storeName, this.userName, Permissions) };
            return (bool)todo(PersmissionsTypes.HireNewStoreOwner, args);
        }
        public override bool removeManager(string storeName, string username)
        {
            object[] args = new object[] { storeName, username, this.userName };
            return (bool)todo(PersmissionsTypes.RemoveManager, args);
        }
        public override ICollection<aUser> getInfoEmployees(string storeName)
        {
            object[] args = new object[] { storeName };
            return (ICollection<aUser>)todo(PersmissionsTypes.GetInfoEmployees, args);
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
            MemberDAL.update(new memberData(userName, password), objectToData(this));
        }


    }
}

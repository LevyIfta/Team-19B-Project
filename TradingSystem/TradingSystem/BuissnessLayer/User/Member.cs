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
        //public string userName { get; set; }
        public string password { get; set; }
        public double age { get; set; }
        public string gender { get; set; }
        public string address { get; set; }

        
        public basePermmision permmisions { get; set; }


        public Member(string username, string password) : base()
        {
            this.userName = username;
            this.password = password;
            this.age = -1;
            this.gender = "nun";
            this.address = "";
            this.reciepts = new List<Receipt>();
            this.myCart = new ShoppingCart(this);
        }
        public Member(string username, string password, double age, string gender, string address) : base()
        {
            this.userName = username;
            this.password = password;
            this.age = age;
            this.gender = gender;
            this.address = address;
            this.reciepts = new List<Receipt>();
            this.myCart = new ShoppingCart(this);
        }

        public Member(MemberData member)
        {
            this.userName = member.userName;
            this.password = member.password;
            this.reciepts = new List<Receipt>();
            this.myCart = new ShoppingCart(this);
        }

        public override string getUserName()
        {
            return userName;
        }
        public override double getAge()
        {
            return age;
        }
        public override string getGender()
        {
            return gender;
        }
        public override string getAddress()
        {
            return address;
        }
        override
        public object todo(PersmissionsTypes func, object[] args)
        {
            return permmisions.todo(func, args);
        }
        override
        public bool EstablishStore(string storeName)
        {
            if (!Stores.addStore(storeName, this))
                return false;
            
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

        public override string[] purchase(string creditNumber, string validity, string cvv)
        {
            ICollection<Receipt> list = new List<Receipt>();
            foreach (ShoppingBasket basket in getMyCart().baskets)
            {
                string[] ans = basket.store.executePurchase(basket, creditNumber, validity, cvv);
                if (ans == null || ans[0].Equals("false"))
                    return ans;
                Receipt receipt = GetReceiptNow(ans[1]);
                list.Add(receipt);
            }
            string[] arr = new string[list.Count + 1];
            arr[0] = "true";
            int i = 1;
            foreach(Receipt receipt in list)
            {
                arr[i] = convertReceipt(receipt);
                i++;
            }
            return arr;
        }
        private string convertReceipt(Receipt receipt)
        {
            string ans = "";
            foreach (int id in receipt.products.Keys)
            {
                ans += id + "<" + receipt.products[id] + "=";
            }
            if(ans.Length > 0)
                ans = ans.Substring(0, ans.Length - 1);
            return receipt.username + "$" + receipt.store.name + "$" + receipt.price + "$" + receipt.date.ToString("dddd, dd MMMM yyyy HH:mm:ss") + "$" + receipt.receiptId + "$" + ans;
        }//receipt -> user$store$price$date$id$products. products -> pro1&pro2&pro3 -> proInfo^feedback -> feedback_feedback -> user#comment
        private Receipt GetReceiptNow(string id)
        {
            foreach (Receipt receipt in reciepts)
            {
                string check = "" + receipt.receiptId;
                if (check.Equals(id))
                    return receipt;
            }
            return null;
        }
        public bool editPermission(string storeName, string userSponser, aPermission permission)
        {
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
        public override bool editProduct(string storeName, string productName, double price, string manufacturer)
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
        public override bool removeOwner(string storeName, string username)
        {
            object[] args = new object[] { storeName, username, this.userName };
            return (bool)todo(PersmissionsTypes.RemoveOwner, args);
        }
        public override ICollection<aUser> getInfoEmployees(string storeName)
        {
            object[] args = new object[] { storeName };
            return (ICollection<aUser>)todo(PersmissionsTypes.GetInfoEmployees, args);
        }
        public ICollection<PersmissionsTypes> GetPermissions(string storeName)
        {
            if (permmisions == null || permmisions.next == null)
                return null;
            ICollection<PersmissionsTypes> ans = new List<PersmissionsTypes>();
            aPermission corrent = permmisions;
            while (corrent.next != null)
            {
                corrent = corrent.next;
                if (corrent.store.Equals(storeName))
                    ans.Add(corrent.who());
            }
            return ans;
        }
        public override Dictionary<string, ICollection<string>> GetAllPermissions()
        {
            aPermission corrent = permmisions;
            string storeName = "";
            bool first = true;
            Dictionary<string, ICollection<string>> ans = new Dictionary<string, ICollection<string>>();
            ICollection<string> list = new List<string>();
            while (corrent.next != null)
            {
                corrent = corrent.next;
                if (corrent.store.Equals(""))
                    storeName = corrent.store;
                if (!(storeName.Equals(corrent.store)))
                {
                    if(first)
                    {
                        ans[storeName] = list;
                        first = false;
                        storeName = corrent.store;
                    }
                    list = new List<string>();
                }
                list.Add(aPermission.who(corrent.who()));
            }
            return ans;
        }
        
        
        public static Member dataToObject(MemberData data)
        {
            if(data == null)
                return null;
            throw new NotImplementedException();
        }
        public static MemberData objectToData(Member member)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// call everytime you chane anything in the user data
        /// </summary>
        protected override void update()
        {
            MemberDAL.update(new MemberData(userName, password));
        }


    }
}

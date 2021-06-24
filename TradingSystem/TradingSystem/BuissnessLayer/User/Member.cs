using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.User.Permmisions;
using TradingSystem.DataLayer;
using TradingSystem.BuissnessLayer.commerce;
using TradingSystem.BuissnessLayer.User;

namespace TradingSystem.BuissnessLayer
{
    public class Member : aUser
    {
        private string _userName { get; set; }
     
        public string password { get; set; }
        public double age { get; set; }
        public string gender { get; set; }
        public string address { get; set; }

        
        public basePermmision permmisions { get; set; }

        public ICollection<Message> messages { get; set; }
        public ICollection<OfferRequest> requests { get; set; }
        public ICollection<OfferRequest> requestsToAnswer { get; set; }


        public Member(string username, string password) : base()
        {
            this._userName = username;
            this.password = password;
            this.age = -1;
            this.gender = "nun";
            this.address = "";
            this.reciepts = new List<Receipt>();
            this.myCart = new ShoppingCart(this);
            this.messages = new List<Message>();
        }
        public Member(string username, string password, double age, string gender, string address) : base()
        {
            this._userName = username;
            this.password = password;
            this.age = age;
            this.gender = gender;
            this.address = address;
            this.reciepts = new List<Receipt>();
            this.myCart = new ShoppingCart(this);
            this.messages = new List<Message>();
        }

        public Member(MemberData member)
        {
            this._userName = member.userName;
            this.password = member.password;
            this.reciepts = new List<Receipt>();
            foreach (ReceiptData item in member.receipts)
            {
                this.reciepts.Add(new Receipt(item));
            }
            this.myCart = new ShoppingCart(this, member.shopingcart);
            this.messages = new List<Message>();
            foreach (MessageData msg in member.messages)
            {
                this.messages.Add(new Message(msg));
            }

        }

        public override string getUserName()
        {
            return _userName;
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
            if (permmisions == null)
                return null;
            return permmisions.todo(func, args);
        }
        override
        public bool EstablishStore(string storeName)
        {
            if (!Stores.addStore(storeName, this))
                return false;
            Stores.searchStore(storeName).addOwner(this);
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
            aPermission temp10 = new removeOwner(storeName, null);
            permmisions.addPermission(temp1);
            permmisions.addPermission(temp2);
            permmisions.addPermission(temp3);
            permmisions.addPermission(temp4);
            permmisions.addPermission(temp5);
            permmisions.addPermission(temp6);
            permmisions.addPermission(temp7);
            permmisions.addPermission(temp8);
            permmisions.addPermission(temp9);
            permmisions.addPermission(temp10);
            return true;
        }

        public override string[] purchase(string creditNumber, string validity, string cvv)
        {
            ICollection<Receipt> list = new List<Receipt>();
            foreach (ShoppingBasket basket in getMyCart().baskets)
            {
                string[] ans = Stores.searchStore(basket.store.name).executePurchase(basket, creditNumber, validity, cvv);
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
            foreach (Product product in receipt.getProducts())
            {
                ans += product.info.id + "<" + product.amount + "=";
            }
            if(ans.Length > 0)
                ans = ans.Substring(0, ans.Length - 1);
            return this.userName + "$" + receipt.store.name + "$" + receipt.price + "$" + receipt.date.ToString("dddd, dd MMMM yyyy HH:mm:ss") + "$" + receipt.receiptId + "$" + ans;
        }//receipt -> user$store$price$date$id$products. products -> 1<4=5<4
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
            aPermission prev = null;
            aPermission corrent = permmisions.next;

            while (corrent != null)
            {
                if (corrent.store.Equals(storeName) && corrent.sponser != null && corrent.sponser.Equals(userSponser))
                {
                    if (start == null)
                    {
                        start = permmisions;
                        if (prev == null)
                            prev = permmisions;
                    }
                }
                else
                {
                    if (start != null)
                    {
                        prev.next = corrent;
                        return true;
                    }
                }
                corrent = corrent.next;
            }
            if (start != null)
            {
                prev.next = null;
                return true;
            }
            return false;
        }
        
        public override bool addNewProduct(string storeName, string productName, double price, int amount, string category, string manufacturer)
        {
            object[] args = new object[] { storeName , productName, price, amount, category, manufacturer };
            var temp = todo(PersmissionsTypes.AddProduct, args);
            if (temp == null)
                return false;
            return (bool)temp;
        }
        public override bool removeProduct(string storeName, string productName, string manufacturer)
        {
            object[] args = new object[] { storeName, productName, manufacturer };
            var temp = todo(PersmissionsTypes.RemoveProduct, args);
            if (temp == null)
                return false;
            return (bool)temp;
        }
        public override bool editProduct(string storeName, string productName, double price, string manufacturer)
        {
            object[] args = new object[] { storeName, productName, price, manufacturer };
            var temp = todo(PersmissionsTypes.EditProduct, args);
            if (temp == null)
                return false;
            return (bool)temp;
        }
        public override bool supply(string storeName, string productName, int amount, string manufacturer)
        {
            object[] args = new object[] { storeName, productName, amount, manufacturer };
            Store store = Stores.searchStore(storeName);

            if (store == null)
                return false;

            return store.supply(productName, manufacturer, amount);
        }
        public override bool editManagerPermissions(string storeName, string username, List<PersmissionsTypes> Permissions)
        {
            object[] args = new object[] { storeName, username, this.userName, aPermission.converPer(storeName, this.userName, Permissions) };
            var temp = todo(PersmissionsTypes.EditManagerPermissions, args);
            if (temp == null)
                return false;
            return (bool)temp;
        }
        public override ICollection<Receipt> getMyPurchaseHistory(string storeName)
        {
            ICollection<Receipt> list = new List<Receipt>();
            foreach(Receipt receipt in reciepts)
            {
                if (receipt.store.name.Equals(storeName))
                    list.Add(receipt);
            }
            return list;
        }
        public override ICollection<Receipt> getPurchaseHistory(string storeName)
        {
            object[] args = new object[] { storeName };
            var temp = todo(PersmissionsTypes.GetPurchaseHistory, args);
            if (temp == null)
                return null;
            if (temp is bool)
                return null;
            return (ICollection<Receipt>)temp;
        }
        public override bool hireNewStoreManager(string storeName, string username)
        {
            object[] args = new object[] { storeName, username, this.userName};
            var temp = todo(PersmissionsTypes.HireNewStoreManager, args);
            if (temp == null)
                return false;
            return (bool)temp;
        }
        public override bool hireNewStoreOwner(string storeName, string username, List<PersmissionsTypes> Permissions)
        {
            object[] args = new object[] { storeName, username, aPermission.converPer(storeName, this.userName, Permissions) };
            var temp = todo(PersmissionsTypes.HireNewStoreOwner, args);
            if (temp == null)
                return false;
            return (bool)temp;
        }
        public override bool removeManager(string storeName, string username)
        {
            object[] args = new object[] { storeName, username, this.userName };
            var temp = todo(PersmissionsTypes.RemoveManager, args);
            if (temp == null)
                return false;
            return (bool)temp;
        }
        public override bool removeOwner(string storeName, string username)
        {
            object[] args = new object[] { storeName, username, this.userName };
            var temp = todo(PersmissionsTypes.RemoveOwner, args);
            if (temp == null)
                return false;
            return (bool)temp;
        }
        public override ICollection<Member> getInfoEmployees(string storeName)
        {
            object[] args = new object[] { storeName };
            var temp = todo(PersmissionsTypes.GetInfoEmployees, args);
            if (temp == null)
                return null;
            if (temp is bool)
                return null;
            return (ICollection<Member>)temp;
        }
        public override ICollection<PersmissionsTypes> GetPermissions(string storeName)
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
            bool firstfirst = true;
            Dictionary<string, ICollection<string>> ans = new Dictionary<string, ICollection<string>>();
            ICollection<string> list = new List<string>();
            if (corrent == null)
                return null;
            while (corrent.next != null)
            {
                //if (corrent.store.Equals(""))
                    //storeName = corrent.store;
                corrent = corrent.next;
                if (firstfirst)
                {
                    storeName = corrent.store;
                    firstfirst = false;
                }
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
            ans[storeName] = list;
            return ans;
        }
        public override bool sendMessage(Message message)
        {
            if (!message.UserToSend.Equals(this.userName))
                return false;
            messages.Add(message);
            return true;
        }
        public override MemberData toDataObject()
        {
            List<BasketInCart> baskets = new List<BasketInCart>();
            List<ReceiptData> receipts = new List<ReceiptData>();
            List<MessageData> messages = new List<MessageData>();
            foreach (ShoppingBasket basket in myCart.baskets)
                //  baskets.Add(basket.toDataObject());
                baskets.Add(DataLayer.ORM.DataAccess.getBasket(basket.owner.getUserName(), basket.store.name));
            foreach (Receipt receipt in this.reciepts)
                //receipts.Add(receipt.toDataObject());
                receipts.Add(DataLayer.ORM.DataAccess.getReciept(receipt.receiptId));
            foreach (Message item in this.messages)
            {
                // messages.Add(item.toDataObject());
                messages.Add(DataLayer.ORM.DataAccess.getMessage(item.id));
            }
            return new MemberData(userName, password, age, gender, address, baskets, receipts, messages);
        }
        /*
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
        */
        /// <summary>
        /// call everytime you chane anything in the user data
        /// </summary>
        protected override void update()
        {
            DataLayer.ORM.DataAccess.update(toDataObject());
        }

        public override void placeOffer(OfferRequest request)
        {
            this.requests.Add(request);
            request.send();
        }

        public override void addOfferToAnswer(OfferRequest request)
        {
            this.requestsToAnswer.Add(request);
        }

        public override OfferRequest getRequestToAnswer(int requestID)
        {
            foreach (OfferRequest request in this.requestsToAnswer)
                if (request.id == requestID)
                    return request;
            return null;
        }
        public override bool acceptRequest(int id)
        {
            foreach (OfferRequest request in this.requestsToAnswer)
                if (request.id == id)
                    return request.accept(this);
            return false;
        }

        public override Receipt getReceipt(int receiptID)
        {
            foreach (Receipt receipt in this.reciepts)
                if (receipt.receiptId == receiptID)
                    return receipt;
            return null;
        }

        public override bool rejectOffer(int id)
        {
            foreach (OfferRequest request in this.requestsToAnswer)
                if (request.id == id)
                    return request.reject(this);
            return false;
        }

        public override bool negotiateRequest(int id, double price)
        {
            foreach (OfferRequest request in this.requestsToAnswer)
                if (request.id == id)
                    return request.negotiate(price, this);
            return false;
        }
    }
}

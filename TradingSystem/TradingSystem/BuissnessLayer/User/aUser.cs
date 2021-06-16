using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.User.Permmisions;
using TradingSystem.BuissnessLayer.commerce;
using System.Threading;
using TradingSystem.BuissnessLayer.User;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer
{
    public abstract class aUser
    {

      
        public string userName { get; set; }
        public ShoppingCart myCart { get; set; }

        public Queue<Tuple<string, string>> alarms { get; set; } = new Queue<Tuple<string, string>>();
        public ICollection<Receipt> reciepts { get; set; }

        public AutoResetEvent alarmLock = new AutoResetEvent(false);
        private object[] alarmthreadParams;
        public bool canLeaveFeedback = false;

        public ShoppingCart getMyCart()
        {
            if (myCart == null)
                myCart = new ShoppingCart(this);
            return myCart;
        }
        public abstract string getUserName();
        public virtual double getAge()
        {
            return -1;
        }
        public virtual string getGender()
        {
            return "nun";
        }
        public virtual string getAddress()
        {
            return "";
        }

        public virtual object todo(PersmissionsTypes func, object[] args)
        {
            return null;
        }

        public bool saveProduct(ShoppingBasket basket1)
        {
            bool match = false;
            if (myCart == null)
            {
                myCart = new ShoppingCart(this);
            }
            foreach (ShoppingBasket basket2 in myCart.baskets)
            {
                if (!match && basket2.store.Equals(basket1.store))
                {
                    match = true;
                    basket2.margeBasket(basket1);
                }
            }
            if (!match)
            {
                myCart.baskets.Add(basket1);
            }
            return true;
        }

        public bool removeProduct(ShoppingBasket basket1)
        {
            basket1.reverse();
            bool match = false;
            foreach (ShoppingBasket basket2 in myCart.baskets)
            {
                if (!match && basket2.store.Equals(basket1.store))
                {
                    match = true;
                    basket2.margeBasket(basket1);
                }
            }
            return true;
        }

        public ShoppingBasket getBasket(Store store)
        {
            return myCart.getBasket(store);
        }
        public virtual void addReceipt(Receipt receipt)
        {
            reciepts.Add(receipt);
        }

        public ShoppingCart getCart()
        {
            return myCart;
        }

        public virtual bool EstablishStore(string storeName)
        {
            return false;
        }
        public virtual string[] purchase(string creditNumber, string validity, string cvv)
        {
            return null;
        }

        public double checkPrice()
        {
            return myCart.checkPrice();

        }
        public Dictionary<Store, Product> browseProducts(string productName, string manufacturer)
        {
            return Stores.searchProduct(productName, manufacturer);
        }
        public Dictionary<Store, Product> browseProducts(string productName, string category, string manufacturer, double minPrice, double maxPrice)
        {
            return Stores.searchProduct(productName, category, manufacturer, minPrice, maxPrice);
        }
        public Store browseStore(string storeName)
        {
            return Stores.searchStore(storeName);
        }
        public virtual ICollection<Receipt> getPurchHistory()
        {
            return reciepts;
        }
        public virtual bool addNewProduct(string storeName, string productName, double price, int amount, string category, string manufacturer)
        {
            return false;
        }
        public virtual bool removeProduct(string storeName, string productName, string manufacturer)
        {
            return false;
        }
        public virtual bool editProduct(string storeName, string productName, double price, string manufacturer)
        {
            return false;
        }
        public virtual bool supply(string storeName, string productName, int amount, string manufacturer)
        {
            return false;
        }
        public virtual ICollection<Receipt> getMyPurchaseHistory(string storeName)
        {
            return null;
        }
        public virtual ICollection<Receipt> getPurchaseHistory(string storeName)
        {
            return null;
        }
        public virtual bool hireNewStoreManager(string storeName, string username)
        {
            return false;
        }
        public virtual bool editManagerPermissions(string storeName, string username, List<PersmissionsTypes> Permissions)
        {
            return false;
        }
        public virtual bool hireNewStoreOwner(string storeName, string username, List<PersmissionsTypes> Permissions)
        {
            return false;
        }
        public virtual bool removeManager(string storeName, string username)
        {
            return false;
        }
        public virtual bool removeOwner(string storeName, string username)
        {
            return false;
        }
        public virtual ICollection<Member> getInfoEmployees(string storeName)
        {
            return null;
        }
        public virtual Dictionary<string, ICollection<string>> GetAllPermissions()
        {
            return null;
        }
        public virtual ICollection<PersmissionsTypes> GetPermissions(string storeName)
        {
            return null;
        }
        public virtual bool sendMessage(Message message)
        {
            return false;
        }
        

        /// <summary>
        /// call everytime you chane anything in the user data
        /// </summary>
        protected virtual void update()
        {
            return;
        }


        public virtual void addAlarm(string title, string description)
        {
            lock (alarms)
            {
                this.alarms.Enqueue(new Tuple<string, string>(title, description));
                alarmLock.Set();
            }
        }

        public virtual Tuple<string, string> fetchAlarm()
        {
            lock (alarms)
            {
                return this.alarms.Dequeue();
            }
        }

        public virtual bool isAlarmsEmpty()
        {
            return this.alarms.Count == 0;
        }

        public AutoResetEvent getAlarmLock()
        {
            return this.alarmLock;
        }
        public object[] getAlarmParams()
        {
            return this.alarmthreadParams;
        }
        public Thread estblishAlarmHandler(object queue, object waitEvent, AutoResetEvent alarmLock, Func<object, bool> alarmHandler)
        {
            this.alarmLock = alarmLock;
            object[] parameters = new object[] { null, queue, waitEvent, alarmLock, new Func<bool>(isAlarmsEmpty), new Func<Tuple<string, string>>(fetchAlarm) };
            Thread th = new Thread(new ThreadStart(() => { alarmHandler(parameters); }));
            this.alarmthreadParams = new object[] { null, queue, waitEvent };
            th.Start();
            alarmLock.Set();
            return th;
        }
        public Thread estblishAlarmHandler(object[] parametersPartial,   AutoResetEvent alarmLock, Func<object, bool> alarmHandler)
        {
            this.alarmLock = alarmLock;
            object[] parameters = new object[] {parametersPartial[0], parametersPartial[1], parametersPartial[2], alarmLock, new Func<bool>(isAlarmsEmpty), new Func<Tuple<string, string>>(fetchAlarm) };
            Thread th = new Thread(new ThreadStart(() => { alarmHandler(parameters); }));
            this.alarmthreadParams = parametersPartial;
            th.Start();
            alarmLock.Set();
            return th;
        }

        public virtual void placeOffer(OfferRequest request)
        {
            
        }

        public virtual MemberData toDataObject()
        {

            return new MemberData("guest", null, 0, null, null, null, null, null);
        }

        public virtual void addOfferToAnswer(OfferRequest request)
        {
        }

        public virtual OfferRequest getRequestToAnswer(int requestID)
        {
            return null;
        }

        public virtual bool acceptRequest(int id)
        {
            return false;
        }

        public virtual Receipt getReceipt(int receiptID)
        {
            return null;
        }

        public virtual bool rejectOffer(int id)
        {
            return false;
        }
    }
}

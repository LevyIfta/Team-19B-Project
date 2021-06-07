using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce
{
    public class OfferRequest
    {
        public enum Status
        {
            PENDING_STORE,
            PENDING_REQUESTER,
            ACCEPTED,
            REJECTED,
            DONE
        }

        public aUser requester;
        public Store store;
        public Product product;
        public Status status;
        public int id;
        // id generation help fields
        private static int currentId = -1;
        private static Object idLocker = new Object();

        public OfferRequest(Product product, aUser requester, Store store)
        {
            // increment id
            lock (idLocker)
            {
                currentId++;
                this.id = currentId;
            }

            this.product = product;
            this.requester = requester;
            this.store = store;
        }

        public bool editPrice(double price)
        {
            if (price < 0) return false;
            this.product.price = price;
            return true;
        }

        public void send()
        {
            this.status = Status.PENDING_STORE;
            // send a notification to the store owner
            notifyStore();
        }

        public bool accept()
        {
            if (this.status != Status.PENDING_REQUESTER) return false;
            this.status = Status.ACCEPTED;
            // send a notification to the user
            notifyUser();
            return true;
        }

        public bool reject()
        {
            if (this.status != Status.PENDING_REQUESTER) return false;
            this.status = Status.REJECTED;
            // send a notification to the user
            notifyUser();
            return true;
        }

        public bool negotiate(double price)
        {
            if (this.status != Status.PENDING_REQUESTER) return false;
            editPrice(price);
            this.status = Status.PENDING_REQUESTER;

            notifyUser();
            return true;
        }

        private void notifyStore()
        {
            this.store.offer(this);
        }

        private void notifyUser()
        {
            this.requester.addAlarm("Offer update", this.ToString());
        }

        public string purchase(string creditNumber, string validity, string cvv)
        {
            if (this.status != Status.ACCEPTED) return "";
            string receipt = this.store.executeOfferPurchase(this.requester, this.product, creditNumber, validity, cvv);

            this.status = Status.DONE;
            return receipt;
        }

        public override string ToString()
        {
            string output = "";

            output += "Request ID: " + this.id + "\n";
            output += "Username: " + this.requester.userName + "\n";
            output += "Product: " + "\n" + this.product.info.ToString() + "\n";
            output += "Price: " + this.product.price + "\n";
            output += "Status: " + this.status + "\n";

            return output;
        }
    }
}

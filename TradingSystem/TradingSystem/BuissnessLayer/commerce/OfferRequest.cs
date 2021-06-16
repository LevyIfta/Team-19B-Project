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
            DONE,
            NULL
        }

        public aUser requester;
        public Store store;
        public Product product;
        public Status status;
        public int id;
        // id generation help fields
        private static int currentId = -1;
        private static Object idLocker = new Object();
        private Object responseLocker = new object();

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
            this.status = Status.NULL;
        }

        public bool editPrice(double price, aUser editor)
        {
            if (price < 0)
                return false;
            // if the user is waiting for the store to response then he/she can't edit the price
            if (this.status == Status.PENDING_STORE && !userHasPermission(editor))
                return false;
            lock (this.responseLocker)
            {
                this.product.price = price;
                // there is a new price, all the owners have to confirm
                this.store.removeAllAcceptors(this.id);
                // if the editor is an owner then add him/her again
                if (userHasPermission(editor))
                    this.store.acceptOffer(editor, this.id);
            }
            return true;
        }

        private bool userHasPermission(aUser editor)
        {
            // check if the user has the permission to edit the price
            return this.store.hasRequestPermission(editor);
        }

        public bool send()
        {
            if (this.status != Status.NULL)
                return false;

            this.status = Status.PENDING_STORE;
            notifyStore();

            return true;
        }

        public bool accept(aUser acceptor)
        {
            if (this.status != Status.PENDING_STORE | !userHasPermission(acceptor))
                return false;
            this.store.acceptOffer(acceptor, this.id);

            if (store.isOfferAccepted(this.id))
                this.status = Status.ACCEPTED;

            notifyUser();
            return true;
        }

        public bool reject(aUser rejector)
        {
            if (this.status != Status.PENDING_STORE | !userHasPermission(rejector))
                return false;

            lock (responseLocker)
            {
                this.status = Status.REJECTED;
                // send a notification to the user
                notifyUser();
            }
            return true;
        }

        public bool negotiate(double price, aUser negotiator) // used by managers/owners
        {
            if (this.status != Status.PENDING_STORE | !userHasPermission(negotiator))
                return false;

            lock (responseLocker)
            {
                editPrice(price, negotiator);
                this.status = Status.ACCEPTED;

                notifyUser();
            }
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

        public string[] purchase(string creditNumber, string validity, string cvv)
        {
            if (this.status != Status.ACCEPTED)
                return null;
            
            string[] receipt = this.store.executeOfferPurchase(this.requester, this.product, creditNumber, validity, cvv);

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

        public double getPrice()
        {
            return this.product.price;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team19B_Project.DataAccess
{
    public class PurchaseReceipts
    {
        public Dictionary<string, LinkedList<string>> receipts { get; }
        public Dictionary<string, LinkedList<string>> storesReceipts { get; }
        private static PurchaseReceipts instance = null;

        private PurchaseReceipts()
        {
            this.receipts = new Dictionary<string, LinkedList<string>>();
        }

        public static PurchaseReceipts Instance
        {
            get
            {
                if (instance == null)
                    instance = new PurchaseReceipts();
                return instance;
            }
        }

        public LinkedList<string> getReceipts(string username)
        {
            return receipts[username];
        }

        public LinkedList<string> getAllReceipts()
        {
            LinkedList<string> receipts = new LinkedList<string>();

            foreach (string username in this.receipts.Keys)
                foreach (string receipt in this.receipts[username])
                    receipts.AddLast(receipt);

            return receipts;
        }

        public LinkedList<string> getStoresReceipts(string storeName)
        {
            return this.storesReceipts[storeName];
        }

        public void addReceipt(string username, string storeName, int productId, int amount, double totalPrice)
        {
            string receipt = username + " has purchased: " + amount + " " + Products.Instance.products[productId].name + " from: " + storeName + " for " + totalPrice + " NIS.\n";

            if (!this.receipts.Keys.Contains(username))
                this.receipts.Add(username, new LinkedList<string>());
            if (!this.storesReceipts.Keys.Contains(storeName))
                this.storesReceipts.Add(storeName, new LinkedList<string>());

            // add a receipt for the user
            this.receipts[username].AddLast(receipt);
            // add to the store's receipts
            this.storesReceipts[storeName].AddLast(receipt);
        }
    }
}

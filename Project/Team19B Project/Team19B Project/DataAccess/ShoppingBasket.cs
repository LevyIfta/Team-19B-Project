using System.Collections.Generic;
using System.Linq;
using Team19B_Project.Business;

namespace Team19B_Project.DataAccess
{
    public class ShoppingBasket
    {
        public Dictionary<int, int> products { get; } // <product_id, amount>
        public string storeName { get; }
        public string owner { get; }

        public ShoppingBasket(string storeName, string owner)
        {
            this.storeName = storeName;
            this.products = new Dictionary<int, int>();
            this.owner = owner;
        }

        public bool addProducts(int productId, int amount)
        {
            // check if there are enough products in the store
            if (StoresServices.getStore(this.storeName).products[productId] < amount)
                return false;
                
            if (!this.products.Keys.Contains(productId))
                this.products.Add(productId, 0);
            this.products[productId] += amount;

            return true;
        }

        public bool removeProducts(int productId, int amount)
        {
            // check if there is enough amount in the basket
            if (!this.products.Keys.Contains(productId) || this.products[productId] < amount)
                return false;
            this.products[productId] -= amount;
            return true;
        }

        public double calculatePrice()
        {
            return StoresServices.getStore(this.storeName).basketPrice(this);
        }

        public bool checkout()
        {
            return StoresServices.getStore(this.storeName).purchaseBasket(this, new CreditCardInfo("123456789123", 123, "SpongeBob", "12/06/2051"));
        }
    }
}

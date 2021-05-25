using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer.commerce
{
    public class ShoppingBasket
    {
        public ICollection<Product> products { get; set; }
        public Store store { get; set; }
        public aUser owner { get; }

        public ShoppingBasket(Store store, aUser owner)
        {
            this.store = store;
            this.products = new LinkedList<Product>();
            this.owner = owner;
        }
        public ShoppingBasket(BasketData shoppingBasketData)
        {
            this.products = new LinkedList<Product>();
            ICollection<ProductsInBasketData> productsInBasketData = ProductsInBasketDAL.getProductIDs(shoppingBasketData.storeName, shoppingBasketData.useName);

            foreach (ProductsInBasketData p_data in productsInBasketData)
            {
                this.products.Add(new Product(p_data));
            }
            this.store = new Store(StoreDAL.getStore(shoppingBasketData.storeName));
            this.owner = UserServices.getUser(shoppingBasketData.useName);
        }

        public Dictionary<Product, int> GetDictionaryProductQuantity()
        {
            return null;
        }

        public double checkPrice()
        {
            return store.calcPrice(products);
        }

        public Receipt purchase(PaymentMethod payment)
        {
            return store.executePurchase(this, payment);
        }
        public void margeBasket(ShoppingBasket basket)
        {
            bool isMatch = false;
            foreach (Product product1 in basket.products)
            {
                foreach (Product product2 in products)
                {
                    if (!isMatch && product1.Equals(product2))
                    {
                        isMatch = true;
                        product2.addAmount(product1.amount);
                        products.Remove(product1);
                        products.Add(product2);
                    }
                }
                if (!isMatch)
                {
                    products.Add(product1);
                }
                isMatch = false;
            }
        }
        public void addProduct(Product pro)
        {
            products.Add(pro);
        }
        public void saveProduct(Product pro)
        {
            products.Add(pro);
        }
        public void reverse()
        {
            foreach (Product pro in products)
            {
                pro.amount = pro.amount * -1;
            }
        }
        public ShoppingBasket clone()
        {
            ShoppingBasket basket = new ShoppingBasket(this.store, this.owner);
            // clone the products
            basket.products = new LinkedList<Product>(this.products.ToList());
            return basket;
        }
        internal void clean()
        {
            this.products = new LinkedList<Product>();
        }


        public override bool Equals(object obj)
        {
            return false;
        }
        public bool Equals(ShoppingBasket obj)
        {
            bool isMatch = false;
            foreach (Product product1 in obj.products)

            {
                foreach (Product product2 in products)
                {
                    if (product1.Equals(product2))
                    {
                        isMatch = true;
                    }
                }
                if (!isMatch)
                {
                    return false;
                }
            }
            return true;

        }
        
        /*public BasketData toDataObject()
        {
            return new BasketData(store.name, owner.getUserName());
        }// (ICollection<ProductData>)this.products.Select(p => p.toDataObject()),
        */
        public void update()
        {
            BasketDAL.update(new BasketData(this.store.name, this.owner.getUserName()));
            // update products
            foreach (Product product in this.products)
                ProductsInBasketDAL.update(new ProductsInBasketData(this.store.name, this.owner.getUserName(), product.info.id, product.amount));

        }

        public void removeProducts() { }
    }
}

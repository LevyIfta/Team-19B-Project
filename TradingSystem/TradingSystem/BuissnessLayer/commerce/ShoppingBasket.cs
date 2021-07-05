using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TradingSystem.DataLayer;
using TradingSystem.DataLayer.ORM;

namespace TradingSystem.BuissnessLayer.commerce
{
    public class ShoppingBasket
    {
        public ICollection<Product> products { get; set; }
        public Store store { get; set; }
        public aUser owner { get; set; }

        public ShoppingBasket(Store store, aUser owner)
        {
            this.store = store;
            this.products = new LinkedList<Product>();
            this.owner = owner;
        }
        /* public ShoppingBasket(BasketData shoppingBasketData)
         {
             this.products = new LinkedList<Product>();
             ICollection<ProductData> productsInBasketData = shoppingBasketData.products;

             foreach (ProductData p_data in productsInBasketData)
             {
                 this.products.Add(new Product(p_data));
             }
             //this.store = new Store();
             //this.owner = UserServices.getUser(shoppingBasketData);
         }*/
        public ShoppingBasket(BasketInCart shoppingBasketData)
        {
            this.products = new LinkedList<Product>();
            ICollection<ProductData> productsInBasketData = shoppingBasketData.products;

            foreach (ProductData p_data in productsInBasketData)
            {
                this.products.Add(new Product(p_data));
            }

            ThreadStart linkStore = new ThreadStart(() => this.store = Stores.searchStore(shoppingBasketData.storeName));
            ThreadStart linkOwner = new ThreadStart(() => this.owner = UserServices.getUser(shoppingBasketData.userName));
            //  this.store = new Store(shoppingBasketData.recipt.store);
            // this.owner = new Member(shoppingBasketData.recipt.user);
            Build.addLink(linkStore);
            Build.addLink(linkOwner);
        }

        public ShoppingBasket(BasketInRecipt shoppingBasketData)
        {
            this.products = new LinkedList<Product>();
            ICollection<ProductData> productsInBasketData = shoppingBasketData.products;

            foreach (ProductData p_data in productsInBasketData)
            {
                this.products.Add(new Product(p_data));
            }
            ThreadStart linkStore = new ThreadStart( ()=>this.store = Stores.searchStore(shoppingBasketData.recipt.store.storeName ));
            ThreadStart linkOwner = new ThreadStart(() => this.owner = UserServices.getUser(shoppingBasketData.recipt.user.userName));
            //  this.store = new Store(shoppingBasketData.recipt.store);
            // this.owner = new Member(shoppingBasketData.recipt.user);
            Build.addLink(linkStore);
            Build.addLink(linkOwner);
        }

        internal IEnumerable<object> GetDictionaryProductQuantity()
        {
            throw new NotImplementedException();
        }

        public double checkPrice()
        {
            return store.calcPriceBeforeDiscount(products);
        }

        public Receipt purchase(PaymentMethod payment)
        {
            return null; //store.executePurchase(this, payment);
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
        public BasketInRecipt toDataObjectRecipt(int id)
        {
            List<ProductData> products = new List<ProductData>();
            foreach (Product product in this.products)
                products.Add(product.toDataObject(this.store.name));
            BasketInRecipt ans = DataAccess.getBasket(id);
            if(ans == null)
                return new BasketInRecipt( products, id);
            ans.products = products;
            return ans;
                
        }

        public BasketInCart toDataObject()
        {
            List<ProductData> products = new List<ProductData>();
            foreach (Product product in this.products)
                products.Add(product.toDataObject(this.store.name));
            BasketInCart ans = DataAccess.getBasket(this.owner.getUserName(), this.store.name);
            if (ans == null)
                return new BasketInCart(this.store.toDataObject(), ((Member)this.owner).toDataObject(), products);
            ans.products = products;
            return ans;
        }// (ICollection<ProductData>)this.products.Select(p => p.toDataObject()),
        /*public BasketInRecipt toDataObject(string notimportent)
        {
            List<ProductData> products = new List<ProductData>();
            foreach (Product product in this.products)
                products.Add(product.toDataObject(this.store.name));
            return new BasketInRecipt(products, );
        }*/

        public void update()
        {
            DataLayer.ORM.DataAccess.update(this.toDataObject());
            //BasketDAL.update(new BasketData(this.store.name, this.owner.getUserName()));
            // update products
            //foreach (Product product in this.products)
                //ProductsInBasketDAL.update(new ProductsInBasketData(this.store.name, this.owner.getUserName(), product.info.id, product.amount));

        }

        public void removeProducts() { }
    }
}

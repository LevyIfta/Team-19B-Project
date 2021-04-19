using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TradingSystem
{
    public class Products
    {
        // this class contains data about products info
        // it provides all pruct-related services in terms of data
        public ConcurrentDictionary<int, ProductInfo> products { get; set; }
        private static Products instance = null;
        static Object lockObject = new Object();
        static int id = 0;

        private Products()
        {
            this.products = new ConcurrentDictionary<int, ProductInfo>();
        }

        public static Products Instance
        {
            get
            {
                if (instance == null)
                    instance = new Products();
                return instance;
            }
        }

        public int addProduct(ProductInfo product)
        {
            lock (lockObject)
            {
                Interlocked.Increment(ref id);
                // assign the id to product
                product.id = id;
                if (products.TryAdd(id, product))
                    return id;
                return -1;
            }
        }

        public bool removeProduct(int productId)
        {
            ProductInfo p;
            return products.TryRemove(productId, out p);
        }
    }
}

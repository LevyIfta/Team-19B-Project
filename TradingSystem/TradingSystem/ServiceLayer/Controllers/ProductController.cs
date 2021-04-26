using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.ServiceLayer
{
    class ProductController
    {
        public static SLproduct makeSLproduct(BuissnessLayer.Product product)
        {
            Object[] parameters = new Object[SLproduct.PARAMETER_COUNT];
            parameters[0] = product.amount;
            parameters[1] = product.price;
            parameters[2] = product.info.name;
            parameters[3] = product.info.category;
            parameters[4] = product.info.manufacturer;
            parameters[5] = product.id;         //TODO
            parameters[6] = product.info.feedbacks;
            return (new SLproduct(parameters));
        }

        public static ICollection<SLproduct> makeSLproductCollection(ICollection<BuissnessLayer.Product> products)
        {
            ICollection<SLproduct> SLproducts = new List<SLproduct>();
            foreach (BuissnessLayer.Product product in products)
            {
                SLproducts.Add(ProductController.makeSLproduct(product));
            }
            return SLproducts;
        }

        public static SLbasket makeSLbasket(BuissnessLayer.ShoppingBasket basket)
        {
            Object[] parameters = new Object[SLbasket.PARAMETER_COUNT];
            parameters[0] = ProductController.makeSLproductCollection(basket.products);
            parameters[1] = basket.store.name;
            parameters[2] = basket.owner.userName;
            return (new SLbasket(parameters));
        }

        public static SLreceipt makeReceipt(BuissnessLayer.Receipt receipt)
        {
            Object[] parameters = new Object[SLreceipt.PARAMETER_COUNT];
            parameters[0] = ProductController.makeSLproductCollection(receipt.basket.products);
            parameters[1] = receipt.store.name;
            parameters[2] = receipt.username;
            parameters[3] = receipt.date;
            parameters[4] = receipt.price;
            parameters[5] = receipt.id;
            return new SLreceipt(parameters);
        }

        public static ICollection<SLreceipt> makeSLreceiptCollection(ICollection<BuissnessLayer.Receipt> receipts)
        {
            ICollection<SLreceipt> SLreceipts = new List<SLreceipt>();
            foreach (BuissnessLayer.Receipt receipt in receipts)
            {
                SLreceipts.Add(ProductController.makeReceipt(receipt));
            }
            return SLreceipts;
        }
    }
}

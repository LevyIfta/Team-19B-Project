using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer.commerce
{
    static class Stores
    {
        public static Store addStore(string storeName, Member founder)
        {
            throw new NotImplementedException();
        }

        public static Store searchStore(string storeName)
        {
            throw new NotImplementedException();
        }

        public static void removeStore(Store store)
        {
            throw new NotImplementedException();
        }
        public static ICollection<Store> getAllStores()
        {
            throw new NotImplementedException();
        }
        public static Dictionary<Store, Product> searchProduct(string productName, string category, string manufacturer, double minPrice, double maxPrice)
        {
            throw new NotImplementedException();
        }
        public static Dictionary<Store, Product> searchProduct(string productName)
        {
            throw new NotImplementedException();
        }
    }
}
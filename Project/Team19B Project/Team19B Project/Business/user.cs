using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
    abstract class User
    {
        public static Member login(string username, string password)
        {
            if (!DataClass.isUserExists(username))
                return null;
            userDetails uDetails = DataClass.confirmPassword(username, password);
            if (uDetails == null)
                return null;
            return new Registered(username, password, uDetails);
        }
        public abstract void register(string username, string password);
        public object browseProducts(string name, string category, double minPrice, double maxPrice, double storeRating)
        {
            return null;
        }
        public abstract object browseStore(string name);
        public bool saveProduct(object store, List<object> Products)
        {
            return false;
        }
        public object manageBasket()
        {
            return null;
        }
    }
}

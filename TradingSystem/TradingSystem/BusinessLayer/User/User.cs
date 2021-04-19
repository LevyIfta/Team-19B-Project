using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem;

namespace TradingSystem
{
    internal abstract class User
    {
        public static Member login(string username, string password)
        {
            if (!DataClass.isUserExists(username))
                return null;
            userDetails uDetails = DataClass.confirmPassword(username, password);
            if (uDetails == null)
                return null;
            return new Member(username, password, uDetails);
        }
        public abstract bool isManager(string storeName);
        public abstract bool register(string username, string password);

        public abstract bool saveProduct(string storeName, Dictionary<int, int> Products);

        public abstract bool removeProduct(string storeName, Dictionary<int, int> products);
        public abstract ShoppingBasket checkShoppingBasketDetails(string storeName);
        public abstract bool purchase(string storeName);
        public abstract bool EstablishStore(string storeName);
        public abstract LinkedList<string> getPurchHistory();
        public abstract string getUserName();
    }
}

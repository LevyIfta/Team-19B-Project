using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.commerce;

namespace TradingSystem.BuissnessLayer.User.Permmisions
{
    public class addProduct : aPermission
    {
        public addProduct(string storeName, string sponser) : base(storeName, sponser) { }
        public override object todo(PersmissionsTypes func, object[] args)
        {//string storeName 0,string productName 1, double price 2, int amount 3, string category 4, string manufacturer 5
            if (func == PersmissionsTypes.AddProduct && this.store.Equals((string)args[0]))
            {
                if(Stores.searchStore((string)args[0]).isProductExist((string)args[1], (string)args[5]))
                {
                    Stores.searchStore((string)args[0]).addProduct((string)args[1], (string)args[4], (string)args[5]);
                }
                else
                {
                    Stores.searchStore((string)args[0]).supply((string)args[1], (string)args[5], (int)args[3]);
                }
                Stores.searchStore((string)args[0]).editPrice((string)args[1], (string)args[5], (double)args[2]);
                return true;
            }
            return base.todo(func, args);
        }
    }
}

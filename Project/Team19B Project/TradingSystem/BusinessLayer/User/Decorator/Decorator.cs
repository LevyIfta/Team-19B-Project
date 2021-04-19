using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
    internal static class Decorator
    {
        public static iDecorator createPremisstion(string name){
            switch (name){
                case "addProduct":
                    return new addProduct();
                    break;
                case "editManagerPermissions":
                    return new editManagerPermissions();
                    break;
                case "editProduct":
                    return new editProduct();
                    break;
                case "getInfoEmployees":
                    return new getInfoEmployees();
                    break;
                case "getPurchaseHistory":
                    return new getPurchaseHistory();
                    break;
                case "hireNewStoreManager":
                    return new hireNewStoreManager();
                    break;
                case "hireNewStoreOwner":
                    return new hireNewStoreOwner();
                    break;
                case "removeProduct":
                    return new removeProduct();
                    break;
                case "removeManager":
                    return new removeManager();
                    break;
            }
            return null;

        }
    }
}

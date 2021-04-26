using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.User.Permmisions
{
    public abstract class aPermission
    {
        public string store { get; set; }

        public string sponser { get; set; }
        public aPermission next { get; set; }

        public aPermission(string storeName, string sponser)
        {
            this.store = storeName;
            this.sponser = sponser;
            next = null;
        }
        public virtual object todo(PersmissionsTypes func, object[] args)
        {
            if (next == null)
                return null;
            return next.todo(func, args);
        }
        public void addPermission(aPermission permission)
        {
            if (this.next != null)
                this.next.addPermission(permission);
            else
                this.next = permission;
        }
        public static aPermission converPer(string storeName, string userSponser, List<PersmissionsTypes> Permissions)
        {
            aPermission permission = new basePermmision("", "");
            foreach (PersmissionsTypes pt in Permissions)
            {
                permission.next = who(pt, storeName, userSponser);
                permission = permission.next;
            }
            return permission.next;
        }
        public static aPermission who(PersmissionsTypes type, string storeName, string userSponser)
        {
            switch (type)
            {
                case PersmissionsTypes.AddProduct:
                    return new addProduct(storeName, userSponser);
                    break;
                case PersmissionsTypes.EditManagerPermissions:
                    return new editManagerPermissions(storeName, userSponser);
                    break;
                case PersmissionsTypes.EditProduct:
                    return new editProduct(storeName, userSponser);
                    break;
                case PersmissionsTypes.GetInfoEmployees:
                    return new getInfoEmployees(storeName, userSponser);
                    break;
                case PersmissionsTypes.GetPurchaseHistory:
                    return new getPurchaseHistory(storeName, userSponser);
                    break;
                case PersmissionsTypes.HireNewStoreManager:
                    return new hireNewStoreManager(storeName, userSponser);
                    break;
                case PersmissionsTypes.HireNewStoreOwner:
                    return new hireNewStoreOwner(storeName, userSponser);
                    break;
                case PersmissionsTypes.RemoveManager:
                    return new removeManager(storeName, userSponser);
                    break;
                case PersmissionsTypes.RemoveProduct:
                    return new removeProduct(storeName, userSponser);
                    break;
            }
            return null;
        }
    }
}

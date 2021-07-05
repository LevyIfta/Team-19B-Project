using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer.Permissions;

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
                return false;
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
            aPermission corrent = permission;
            foreach (PersmissionsTypes pt in Permissions)
            {
                corrent.next = who(pt, storeName, userSponser);
                corrent = corrent.next;
            }
            return permission.next;
        }
        public static aPermission who(PersmissionsTypes type, string storeName, string userSponser)
        {
            switch (type)
            {
                case PersmissionsTypes.AddProduct:
                    return new addProduct(storeName, userSponser);
                case PersmissionsTypes.EditManagerPermissions:
                    return new editManagerPermissions(storeName, userSponser);
                case PersmissionsTypes.EditProduct:
                    return new editProduct(storeName, userSponser);
                case PersmissionsTypes.GetInfoEmployees:
                    return new getInfoEmployees(storeName, userSponser);
                case PersmissionsTypes.GetPurchaseHistory:
                    return new getPurchaseHistory(storeName, userSponser);
                case PersmissionsTypes.HireNewStoreManager:
                    return new hireNewStoreManager(storeName, userSponser);
                case PersmissionsTypes.HireNewStoreOwner:
                    return new hireNewStoreOwner(storeName, userSponser);
                case PersmissionsTypes.RemoveManager:
                    return new removeManager(storeName, userSponser);
                case PersmissionsTypes.RemoveProduct:
                    return new removeProduct(storeName, userSponser);
                case PersmissionsTypes.RemoveOwner:
                    return new removeOwner(storeName, userSponser);
            }
            return null; 
        }
        public static string who(PersmissionsTypes type)
        {
            switch (type)
            {
                case PersmissionsTypes.AddProduct:
                    return "AddProduct";
                case PersmissionsTypes.EditManagerPermissions:
                    return "EditManagerPermissions";
                case PersmissionsTypes.EditProduct:
                    return "EditProduct";
                case PersmissionsTypes.GetInfoEmployees:
                    return "GetInfoEmployees";
                case PersmissionsTypes.GetPurchaseHistory:
                    return "GetPurchaseHistory";
                case PersmissionsTypes.HireNewStoreManager:
                    return "HireNewStoreManager";
                case PersmissionsTypes.HireNewStoreOwner:
                    return "HireNewStoreOwner";
                case PersmissionsTypes.RemoveManager:
                    return "RemoveManager";
                case PersmissionsTypes.RemoveProduct:
                    return "RemoveProduct";
                case PersmissionsTypes.RemoveOwner:
                    return "RemoveOwner";
            }
            return "";
        }
        public virtual PersmissionsTypes who()
        {
            return PersmissionsTypes.AddProduct;
        }

        public abstract ICollection<aPermissionData> toDataObject();


        public aPermission toObject(ICollection<aPermissionData> data)
        {
            basePermmision ans = new basePermmision("", null);
            aPermission pointer = ans;
            foreach (aPermissionData item in data)
            {
                pointer.next = toObject(item);
                pointer = pointer.next;
            }
            return ans;
        }

        public aPermission toObject(aPermissionData data)
        {
            throw new Exception("override failed");
        }




        public aPermission toObject(addProductPermissionData data)
        {
            return new addProduct(data.store, data.sponser);
        }

        public aPermission toObject(editManagerPermissionsData data)
        {
            return new editManagerPermissions(data.store, data.sponser);
        }
        public aPermission toObject(editProductPermissionData data)
        {
            return new editProduct(data.store, data.sponser);
        }
        public aPermission toObject(getInfoEmployeesPermissionData data)
        {
            return new getInfoEmployees(data.store, data.sponser);
        }
        public aPermission toObject(getPurchaseHistoryPermissionData data)
        {
            return new getPurchaseHistory(data.store, data.sponser);
        }
         public aPermission toObject(hireNewStoreManagerPermissionData data)
        {
            return new hireNewStoreManager(data.store, data.sponser);
        }
        public aPermission toObject(hireNewStoreOwnerPermissionData data)
        {
            return new hireNewStoreOwner(data.store, data.sponser);
        }
        public aPermission toObject(removeManagerPermissionData data)
        {
            return new removeManager(data.store, data.sponser);
        }
        public aPermission toObject(removeOwnerPermissionData data)
        {
            return new removeOwner(data.store, data.sponser);
        }

        public aPermission toObject(removeProductPermissionData data)
        {
            return new removeProduct(data.store, data.sponser);
        }



    }
}

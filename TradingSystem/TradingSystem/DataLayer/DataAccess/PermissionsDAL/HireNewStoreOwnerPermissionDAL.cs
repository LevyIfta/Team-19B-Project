using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    static class HireNewStoreOwnerPermissionDAL
    {
        private static List<HireNewStoreOwnerPermissionData> HireNewStoreOwnerPermissions;

        public static HireNewStoreOwnerPermissionData getHireNewStoreOwnerPermission(string userName, string storeName)
        {
            foreach (HireNewStoreOwnerPermissionData hireNewStoreOwnerPermissionData in HireNewStoreOwnerPermissions)
            {
                if (hireNewStoreOwnerPermissionData.userName == userName && hireNewStoreOwnerPermissionData.storeName == storeName)
                    return hireNewStoreOwnerPermissionData;
            }
            return null;
        }


        public static bool isExist(string userName, string storeName)
        {
            foreach (HireNewStoreOwnerPermissionData hireNewStoreOwnerPermissionData in HireNewStoreOwnerPermissions)
            {
                if (hireNewStoreOwnerPermissionData.userName == userName && hireNewStoreOwnerPermissionData.storeName == storeName)
                    return true;
            }
            return false;

        }

        public static void addHireNewStoreOwnerPermission(HireNewStoreOwnerPermissionData hireNewStoreOwnerPermissionData)
        {
            HireNewStoreOwnerPermissions.Add(hireNewStoreOwnerPermissionData);
        }

        public static bool update(HireNewStoreOwnerPermissionData hireNewStoreOwnerPermissionData)
        {
            if (!HireNewStoreOwnerPermissions.Remove(hireNewStoreOwnerPermissionData))
                return false;
            HireNewStoreOwnerPermissions.Add(hireNewStoreOwnerPermissionData);
            return true;

        }

        public static bool remove(HireNewStoreOwnerPermissionData hireNewStoreOwnerPermissionData)
        {
            return HireNewStoreOwnerPermissions.Remove(hireNewStoreOwnerPermissionData);
        }
    }
}

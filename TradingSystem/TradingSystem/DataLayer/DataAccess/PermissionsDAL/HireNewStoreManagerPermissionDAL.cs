using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    static class HireNewStoreManagerPermissionDAL
    {
        private static List<HireNewStoreManagerPermissionData> HireNewStoreManagerPermissions = new List<HireNewStoreManagerPermissionData>();

        public static HireNewStoreManagerPermissionData getHireNewStoreManagerPermission(string userName, string storeName)
        {
            foreach (HireNewStoreManagerPermissionData hireNewStoreManagerPermissionData in HireNewStoreManagerPermissions)
            {
                if (hireNewStoreManagerPermissionData.userName == userName && hireNewStoreManagerPermissionData.storeName == storeName)
                    return hireNewStoreManagerPermissionData;
            }
            return null;
        }


        public static bool isExist(string userName, string storeName)
        {
            foreach (HireNewStoreManagerPermissionData hireNewStoreManagerPermissionData in HireNewStoreManagerPermissions)
            {
                if (hireNewStoreManagerPermissionData.userName == userName && hireNewStoreManagerPermissionData.storeName == storeName)
                    return true;
            }
            return false;

        }

        public static void addHireNewStoreManagerPermission(HireNewStoreManagerPermissionData hireNewStoreManagerPermissionData)
        {
            HireNewStoreManagerPermissions.Add(hireNewStoreManagerPermissionData);
        }

        public static bool update(HireNewStoreManagerPermissionData hireNewStoreManagerPermissionData)
        {
            if (!HireNewStoreManagerPermissions.Remove(hireNewStoreManagerPermissionData))
                return false;
            HireNewStoreManagerPermissions.Add(hireNewStoreManagerPermissionData);
            return true;

        }

        public static bool remove(HireNewStoreManagerPermissionData hireNewStoreManagerPermissionData)
        {
            return HireNewStoreManagerPermissions.Remove(hireNewStoreManagerPermissionData);
        }
        public static ICollection<HireNewStoreManagerPermissionData> getStoreManagers(string storeName)
        {
            ICollection<HireNewStoreManagerPermissionData> storeManagers = new LinkedList<HireNewStoreManagerPermissionData>();
            foreach (HireNewStoreManagerPermissionData manager in HireNewStoreManagerPermissions)
                if (manager.storeName.Equals(storeName))
                    storeManagers.Add(manager);
            return storeManagers;
        }
    }
}

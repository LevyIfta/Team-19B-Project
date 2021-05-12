using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    static class AddProductPermissionDAL
    {
        private static List<AddProductPermissionData> AddProductPermissions = new List<AddProductPermissionData>();

        public static AddProductPermissionData getAddProductPermission(string userName, string storeName)
        {
            foreach (AddProductPermissionData addProductPermissionData in AddProductPermissions)
            {
                if (addProductPermissionData.userName == userName && addProductPermissionData.storeName == storeName)
                    return addProductPermissionData;
            }
            return null;
        }


        public static bool isExist(string userName, string storeName)
        {
            foreach (AddProductPermissionData addProductPermissionData in AddProductPermissions)
            {
                if (addProductPermissionData.userName == userName && addProductPermissionData.storeName == storeName)
                    return true;
            }
            return false;

        }

        public static void addAddProductPermission(AddProductPermissionData addProductPermissionData)
        {
            AddProductPermissions.Add(addProductPermissionData);
        }

        public static bool update(AddProductPermissionData addProductPermissionData)
        {
            if (!AddProductPermissions.Remove(addProductPermissionData))
                return false;
            AddProductPermissions.Add(addProductPermissionData);
            return true;

        }

        public static bool remove(AddProductPermissionData addProductPermissionData)
        {
            return AddProductPermissions.Remove(addProductPermissionData);
        }
    }
}

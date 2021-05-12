using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer.DataAccess.PermissionsDAL
{
    static class RemoveOwnerPermissionDAL
    {
        private static List<RemoveOwnerPermissionData> RemoveOwnerPermission = new List<RemoveOwnerPermissionData>();

        public static RemoveOwnerPermissionData getRemoveManagerPermission(string userName, string storeName)
        {
            foreach (RemoveOwnerPermissionData removeOwnerPermissionData in RemoveOwnerPermission)
            {
                if (removeOwnerPermissionData.userName == userName && removeOwnerPermissionData.storeName == storeName)
                    return removeOwnerPermissionData;
            }
            return null;
        }


        public static bool isExist(string userName, string storeName)
        {
            foreach (RemoveOwnerPermissionData removeOwnerPermissionData in RemoveOwnerPermission)
            {
                if (removeOwnerPermissionData.userName == userName && removeOwnerPermissionData.storeName == storeName)
                    return true;
            }
            return false;

        }

        public static void addRemoveManagerPermission(RemoveOwnerPermissionData removeOwnerPermissionData)
        {
            RemoveOwnerPermission.Add(removeOwnerPermissionData);
        }

        public static bool update(RemoveOwnerPermissionData removeOwnerPermissionData)
        {
            if (!RemoveOwnerPermission.Remove(removeOwnerPermissionData))
                return false;
            RemoveOwnerPermission.Add(removeOwnerPermissionData);
            return true;

        }

        public static bool remove(RemoveOwnerPermissionData removeOwnerPermissionData)
        {
            return RemoveOwnerPermission.Remove(removeOwnerPermissionData);
        }
    }
}

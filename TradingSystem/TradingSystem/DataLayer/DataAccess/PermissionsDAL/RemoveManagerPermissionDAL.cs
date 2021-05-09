using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    static class RemoveManagerPermissionDAL
    {
        private static List<RemoveManagerPermissionData> RemoveManagerPermissions = new List<RemoveManagerPermissionData>();

        public static RemoveManagerPermissionData getRemoveManagerPermission(string userName, string storeName)
        {
            foreach (RemoveManagerPermissionData removeManagerPermissionData in RemoveManagerPermissions)
            {
                if (removeManagerPermissionData.userName == userName && removeManagerPermissionData.storeName == storeName)
                    return removeManagerPermissionData;
            }
            return null;
        }


        public static bool isExist(string userName, string storeName)
        {
            foreach (RemoveManagerPermissionData removeManagerPermissionData in RemoveManagerPermissions)
            {
                if (removeManagerPermissionData.userName == userName && removeManagerPermissionData.storeName == storeName)
                    return true;
            }
            return false;

        }

        public static void addRemoveManagerPermission(RemoveManagerPermissionData removeManagerPermissionData)
        {
            RemoveManagerPermissions.Add(removeManagerPermissionData);
        }

        public static bool update(RemoveManagerPermissionData removeManagerPermissionData)
        {
            if (!RemoveManagerPermissions.Remove(removeManagerPermissionData))
                return false;
            RemoveManagerPermissions.Add(removeManagerPermissionData);
            return true;

        }

        public static bool remove(RemoveManagerPermissionData removeManagerPermissionData)
        {
            return RemoveManagerPermissions.Remove(removeManagerPermissionData);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    static class OwnerPermissionDAL
    {
        private static List<OwnerPermissionData> OwnerPermissions = new List<OwnerPermissionData>();

        public static OwnerPermissionData getOwnerPermission(string ownerName, string storeName)
        {
            foreach (OwnerPermissionData ownerPermissionData in OwnerPermissions)
            {
                if (ownerPermissionData.ownerName == ownerName && ownerPermissionData.storeName == storeName)
                    return ownerPermissionData;
            }
            return null;
        }


        public static bool isExist(string ownerName, string storeName)
        {
            foreach (OwnerPermissionData ownerPermissionData in OwnerPermissions)
            {
                if (ownerPermissionData.ownerName == ownerName && ownerPermissionData.storeName == storeName)
                    return true;
            }
            return false;

        }

        public static void addOwnerPermission(OwnerPermissionData ownerPermissionData)
        {
            OwnerPermissions.Add(ownerPermissionData);
        }

        public static bool update(OwnerPermissionData ownerPermissionData)
        {
            if (!OwnerPermissions.Remove(ownerPermissionData))
                return false;
            OwnerPermissions.Add(ownerPermissionData);
            return true;

        }

        public static bool remove(OwnerPermissionData ownerPermissionData)
        {
            return OwnerPermissions.Remove(ownerPermissionData);
        }
    }
}

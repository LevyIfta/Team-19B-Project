using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    static class GetPurchaseHistoryPermissionDAL
    {
        private static List<GetPurchaseHistoryPermissionData> GetPurchaseHistoryPermissions;

        public static GetPurchaseHistoryPermissionData getGetPurchaseHistoryPermission(string userName, string storeName)
        {
            foreach (GetPurchaseHistoryPermissionData getPurchaseHistoryPermissionData in GetPurchaseHistoryPermissions)
            {
                if (getPurchaseHistoryPermissionData.userName == userName && getPurchaseHistoryPermissionData.storeName == storeName)
                    return getPurchaseHistoryPermissionData;
            }
            return null;
        }


        public static bool isExist(string userName, string storeName)
        {
            foreach (GetPurchaseHistoryPermissionData getPurchaseHistoryPermissionData in GetPurchaseHistoryPermissions)
            {
                if (getPurchaseHistoryPermissionData.userName == userName && getPurchaseHistoryPermissionData.storeName == storeName)
                    return true;
            }
            return false;

        }

        public static void addGetPurchaseHistoryPermission(GetPurchaseHistoryPermissionData getPurchaseHistoryPermissionData)
        {
            GetPurchaseHistoryPermissions.Add(getPurchaseHistoryPermissionData);
        }

        public static bool update(GetPurchaseHistoryPermissionData getPurchaseHistoryPermissionData)
        {
            if (!GetPurchaseHistoryPermissions.Remove(getPurchaseHistoryPermissionData))
                return false;
            GetPurchaseHistoryPermissions.Add(getPurchaseHistoryPermissionData);
            return true;

        }

        public static bool remove(GetPurchaseHistoryPermissionData getPurchaseHistoryPermissionData)
        {
            return GetPurchaseHistoryPermissions.Remove(getPurchaseHistoryPermissionData);
        }
    }
}

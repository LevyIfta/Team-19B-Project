using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    static class GetInfoEmployeesPermissionDAL
    {
        private static List<GetInfoEmployeesPermissionData> GetInfoEmployeesPermissions = new List<GetInfoEmployeesPermissionData>();

        public static GetInfoEmployeesPermissionData getGetInfoEmployeesPermission(string userName, string storeName)
        {
            foreach (GetInfoEmployeesPermissionData getInfoEmployeesPermissionData in GetInfoEmployeesPermissions)
            {
                if (getInfoEmployeesPermissionData.userName == userName && getInfoEmployeesPermissionData.storeName == storeName)
                    return getInfoEmployeesPermissionData;
            }
            return null;
        }


        public static bool isExist(string userName, string storeName)
        {
            foreach (GetInfoEmployeesPermissionData getInfoEmployeesPermissionData in GetInfoEmployeesPermissions)
            {
                if (getInfoEmployeesPermissionData.userName == userName && getInfoEmployeesPermissionData.storeName == storeName)
                    return true;
            }
            return false;

        }

        public static void addGetInfoEmployeesPermission(GetInfoEmployeesPermissionData getInfoEmployeesPermissionData)
        {
            GetInfoEmployeesPermissions.Add(getInfoEmployeesPermissionData);
        }

        public static bool update(GetInfoEmployeesPermissionData getInfoEmployeesPermissionData)
        {
            if (!GetInfoEmployeesPermissions.Remove(getInfoEmployeesPermissionData))
                return false;
            GetInfoEmployeesPermissions.Add(getInfoEmployeesPermissionData);
            return true;

        }

        public static bool remove(GetInfoEmployeesPermissionData getInfoEmployeesPermissionData)
        {
            return GetInfoEmployeesPermissions.Remove(getInfoEmployeesPermissionData);
        }
    }
}

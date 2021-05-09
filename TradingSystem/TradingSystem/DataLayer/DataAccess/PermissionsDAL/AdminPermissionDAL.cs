using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    static class AdminPermissionDAL
    {
        private static List<AdminPermissionData> AdminPermissions = new List<AdminPermissionData>();

        public static AdminPermissionData getAdminPermission(string adminName)
        {
            foreach (AdminPermissionData adminPermissionData in AdminPermissions)
            {
                if (adminPermissionData.adminName == adminName)
                    return adminPermissionData;
            }
            return null;
        }


        public static bool isExist(string adminName)
        {
            foreach (AdminPermissionData adminPermissionData in AdminPermissions)
            {
                if (adminPermissionData.adminName == adminName)
                    return true;
            }
            return false;

        }

        public static void addAdminPermission(AdminPermissionData adminPermissionData)
        {
            AdminPermissions.Add(adminPermissionData);
        }

        public static bool update(AdminPermissionData adminPermissionData)
        {
            if (!AdminPermissions.Remove(adminPermissionData))
                return false;
            AdminPermissions.Add(adminPermissionData);
            return true;

        }

        public static bool remove(AdminPermissionData adminPermissionData)
        {
            return AdminPermissions.Remove(adminPermissionData);
        }
    }
}

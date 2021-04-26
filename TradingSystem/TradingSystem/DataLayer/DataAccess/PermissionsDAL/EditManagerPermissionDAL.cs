using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    static class EditManagerPermissionDAL
    {
        private static List<EditManagerPermissionData> EditManagerPermissions;

        public static EditManagerPermissionData getEditManagerPermissions(string userName, string storeName)
        {
            foreach (EditManagerPermissionData editManagerPermissionData in EditManagerPermissions)
            {
                if (editManagerPermissionData.userName == userName && editManagerPermissionData.storeName == storeName)
                    return editManagerPermissionData;
            }
            return null;
        }


        public static bool isExist(string userName, string storeName)
        {
            foreach (EditManagerPermissionData editManagerPermissionData in EditManagerPermissions)
            {
                if (editManagerPermissionData.userName == userName && editManagerPermissionData.storeName == storeName)
                    return true;
            }
            return false;

        }

        public static void addEditManagerPermissions(EditManagerPermissionData editManagerPermissionData)
        {
            EditManagerPermissions.Add(editManagerPermissionData);
        }

        public static bool update(EditManagerPermissionData editManagerPermissionData)
        {
            if (!EditManagerPermissions.Remove(editManagerPermissionData))
                return false;
            EditManagerPermissions.Add(editManagerPermissionData);
            return true;

        }

        public static bool remove(EditManagerPermissionData editManagerPermissionData)
        {
            return EditManagerPermissions.Remove(editManagerPermissionData);
        }
    }
}

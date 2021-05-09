using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    static class EditProductPermissionDAL
    {
        private static List<EditProductPermissionData> EditProductPermissions = new List<EditProductPermissionData>();

        public static EditProductPermissionData getEditProductPermission(string userName, string storeName)
        {
            foreach (EditProductPermissionData editProductPermissionData in EditProductPermissions)
            {
                if (editProductPermissionData.userName == userName && editProductPermissionData.storeName == storeName)
                    return editProductPermissionData;
            }
            return null;
        }


        public static bool isExist(string userName, string storeName)
        {
            foreach (EditProductPermissionData editProductPermissionData in EditProductPermissions)
            {
                if (editProductPermissionData.userName == userName && editProductPermissionData.storeName == storeName)
                    return true;
            }
            return false;

        }

        public static void addEditProductPermission(EditProductPermissionData editProductPermissionData)
        {
            EditProductPermissions.Add(editProductPermissionData);
        }

        public static bool update(EditProductPermissionData editProductPermissionData)
        {
            if (!EditProductPermissions.Remove(editProductPermissionData))
                return false;
            EditProductPermissions.Add(editProductPermissionData);
            return true;

        }

        public static bool remove(EditProductPermissionData editProductPermissionData)
        {
            return EditProductPermissions.Remove(editProductPermissionData);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    class AdminPermissionData
    {
        //FIELDS
        public string adminName { get; set; }


        //CONSTRUCTORS
        public AdminPermissionData(string adminName)
        {
            this.adminName = adminName;
        }

        //EQUALS
        public override bool Equals(object obj)
        {
            return false;
        }

        public bool Equals(AdminPermissionData other)
        {
            return this.adminName.Equals(other.adminName);
        }
    }
}

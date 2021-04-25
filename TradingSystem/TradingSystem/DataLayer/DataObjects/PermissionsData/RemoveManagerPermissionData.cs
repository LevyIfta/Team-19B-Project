﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    class RemoveManagerPermissionData
    {
        //FIELDS
        public string userName { get; set; }
        public string storeName { get; set; }


        //CONSTRUCTORS
        public RemoveManagerPermissionData(string userName, string storeName)
        {
            this.userName = userName;
            this.storeName = storeName;
        }

        //EQUALS
        public override bool Equals(object obj)
        {
            return false;
        }

        public bool Equals(RemoveManagerPermissionData other)
        {
            return this.userName.Equals(other.userName) & this.storeName.Equals(other.storeName);
        }
    }
}

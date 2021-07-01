using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer.Permissions
{
    public abstract class aPermissionData
    {
   /*     protected aPermissionData()
        {
        }*/

        protected aPermissionData(MemberData myOwner, string store, string sponser)
        {
            this.myOwnerName = myOwner.userName;
            this.myOwner = myOwner ?? throw new ArgumentNullException(nameof(myOwner));
            this.store = store ?? throw new ArgumentNullException(nameof(store));
            this.sponser = sponser ?? throw new ArgumentNullException(nameof(sponser));
        }

        protected aPermissionData(string store, string sponser)
        {
           
            this.store = store ?? throw new ArgumentNullException(nameof(store));
            this.sponser = sponser ?? throw new ArgumentNullException(nameof(sponser));
        }

        [Key, Column(Order = 0)]
        public virtual string myOwnerName { get; set; }
        public MemberData myOwner { get; set; }
        [Key, Column(Order = 1)]
        public string store { get; set; }
        public string sponser { get; set; }
    }
}

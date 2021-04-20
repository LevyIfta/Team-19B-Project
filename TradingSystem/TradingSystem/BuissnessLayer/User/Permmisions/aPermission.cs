using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.User.Permmisions
{
    abstract class aPermission
    {
        public Store store { get; set; }

        public aPermission next { get; set; }

        public virtual object todo(PersmissionsTypes func, object[] args)
        {
            if (next == null)
                return null;
            return next.todo(func, args);
        }
    }
}

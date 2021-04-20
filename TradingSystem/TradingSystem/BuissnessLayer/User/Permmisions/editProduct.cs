using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.User.Permmisions
{
    class editProduct : aPermission
    {
        public override object todo(PersmissionsTypes func, object[] args)
        {
            if (func == PersmissionsTypes.editProduct)
                return null; //todo
            return base.todo(func, args);
        }
    }
}

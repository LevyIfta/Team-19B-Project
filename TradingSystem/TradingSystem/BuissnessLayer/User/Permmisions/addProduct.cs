using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.User.Permmisions
{
    public class addProduct : aPermission
    {
        public override object todo(PersmissionsTypes func, object[] args)
        {
            if (func == PersmissionsTypes.AddProduct)
                return null; //todo
            return base.todo(func, args);
        }
    }
}

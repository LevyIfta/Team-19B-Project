using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.DicountPolicy
{
    interface iPolicyDiscount
    {
        /*  isValid on the products to check every product have policy*/
        bool isValid(ICollection<Product> products);

     //  void addPolicy(iPolicy policy);
     //   bool removePolicy();



    }
}

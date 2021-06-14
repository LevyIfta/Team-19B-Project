using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce
{
    class feedbackSystem
    {
        public static bool leaveFeedback(string userName, string productID, string manufacturer, string comment)
        {
            if (!DataLayer.ORM.DataAccess.isProductExist(int.Parse(productID)))
            {
                return false;
            }
            if (!DataLayer.ORM.DataAccess.isFeedBackExist(userName, int.Parse(productID))){
                return false;
            }
            
            return true;
        }
    }
}

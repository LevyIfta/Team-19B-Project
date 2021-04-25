using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce
{
    class feedbackSystem
    {
        public static bool leaveFeedback(string userName, string productName, string manufacturer, string comment)
        {
            if(! DataLayer.DataAccess.ProductInfoDAL.isExist(productName, manufacturer))
            {
                return false;
            }
            if (!DataLayer.DataAccess.FeedbackDAL.isExist(productName, manufacturer, userName){
                return false;
            }

            return true;
        }
    }
}

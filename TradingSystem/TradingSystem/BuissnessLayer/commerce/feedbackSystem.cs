using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce
{
    public class feedbackSystem
    {
        public static bool leaveFeedback(string userName, string productName, string manufacturer, string comment)
        {
            if (!DataLayer.ProductInfoDAL.isExist(productName, manufacturer))
            {
                return false;
            }
            if (!DataLayer.FeedbackDAL.isExist(productName, manufacturer, userName)){
                return false;
            }

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer.DataAccess
{
    static class FeedbackDAL
    {
        private static List<FeedbackData> feedbacks;

        public static FeedbackData getFeedback(string productInfo, string manufacturer, string userName)
        {
            foreach (FeedbackData feedbackData in feedbacks)
            {
                if (feedbackData.productInfo == productInfo && feedbackData.manufacturer == manufacturer && feedbackData.userName == userName)
                    return feedbackData;
            }
            return null;
        }


        public static bool isExist(string productInfo, string manufacturer, string userName)
        {
            foreach (FeedbackData feedbackData in feedbacks)
            {
                if (feedbackData.productInfo == productInfo && feedbackData.manufacturer == manufacturer && feedbackData.userName == userName)
                    return true;
            }
            return false;

        }

        public static void addFeedback(FeedbackData feedbackData)
        {
            feedbacks.Add(feedbackData);
        }

        public static bool update(FeedbackData feedbackData)
        {
            if (!feedbacks.Remove(feedbackData))
                return false;
            feedbacks.Add(feedbackData);
            return true;

        }

        public static bool remove(FeedbackData feedbackData)
        {
            return feedbacks.Remove(feedbackData);
        }
    }
}

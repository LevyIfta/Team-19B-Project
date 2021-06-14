using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.Policy
{
    public class BuyingPoliciesGenerator
    {
        // this is a class that has static methods
        // it provides services for generating buying policies

        public static iPolicy generateAgePolicyByProduct(string name, string category, string man, int minAge)
        {
            iPolicy policy = new BasePolicy((Product p) => p.info.Equals(ProductInfo.getProductInfo(name, category, man)), (Product p, aUser u) => u.getAge() >= minAge);
            return policy;
        }

        public static iPolicy generateAgePolicyByCategory(string category, int minAge)
        {
            iPolicy policy = new BasePolicy(p => p.info.category.Equals(category), (Product p, aUser u) => u.getAge() >= minAge);
            return policy;
        }

        public static iPolicy generateDailyPolicyByProduct(string name, string category, string man, int maxHour)
        {
            iPolicy policy = new BasePolicy(p => p.info.Equals(ProductInfo.getProductInfo(name, category, man)), (Product p, aUser u) => DateTime.Now.Hour <= maxHour);
            return policy;
        }

        public static iPolicy generateDailyPolicyByCategory(string category, int hour)
        {
            iPolicy policy = new BasePolicy(p => p.info.category.Equals(category), (Product p, aUser u) => DateTime.Now.Hour <= hour);
            return policy;
        }

        public static iPolicy generateMaxAmountPolicyByProduct(string name, string category, string man, int maxAmount)
        {
            iPolicy policy = new BasePolicy(p => p.info.Equals(ProductInfo.getProductInfo(name, category, man)), (Product p, aUser u) => p.amount <= maxAmount);
            return policy;
        }

        public static iPolicy generateMinAmountPolicyByProduct(string name, string category, string man, int minAmount)
        {
            iPolicy policy = new BasePolicy(p => p.info.Equals(ProductInfo.getProductInfo(name, category, man)), (Product p, aUser u) => p.amount >= minAmount);
            return policy;
        }

        public static iPolicy generateMaxAmountPolicyByCategory(string category, int maxAmount)
        {
            iPolicy policy = new BasePolicy(p => p.info.category.Equals(category), (Product p, aUser u) => p.amount <= maxAmount);
            return policy;
        }

        public static iPolicy generateMinAmountPolicyByCategory(string category, int minAmount)
        {
            iPolicy policy = new BasePolicy(p => p.info.category.Equals(category), (Product p, aUser u) => p.amount >= minAmount);
            return policy;
        }

        public static iPolicy generateWeeklyTimePolicyByCategory(string category, int minDay, int minHour, int maxDay, int maxHour)
        {
            if (minDay > maxDay | minDay < 1 | minDay > 7 | maxDay < 1 | maxDay > 7 | minHour < 0 | minHour > 23 | maxHour < 0 | maxHour > 23) return null;
            iPolicy policy = new BasePolicy(p => p.info.category.Equals(category), (Product p, aUser u) => DateTime.Now.Day < minDay | DateTime.Now.Day > maxDay | (DateTime.Now.Day == minDay & DateTime.Now.Hour < minHour) | (DateTime.Now.Day == maxDay & DateTime.Now.Hour > maxHour));
            
            return policy;
        }

        public static iPolicy generateWeeklyTimePolicyByProduct(string name, string category, string man, int minDay, int minHour, int maxDay, int maxHour)
        {
            if (minDay > maxDay | minDay < 1 | minDay > 7 | maxDay < 1 | maxDay > 7 | minHour < 0 | minHour > 23 | maxHour < 0 | maxHour > 23) return null;
            iPolicy policy = new BasePolicy(p => p.info.Equals(ProductInfo.getProductInfo(name, category, man)), (Product p, aUser u) => DateTime.Now.Day < minDay | DateTime.Now.Day > maxDay | (DateTime.Now.Day == minDay & DateTime.Now.Hour < minHour) | (DateTime.Now.Day == maxDay & DateTime.Now.Hour > maxHour));
            
            return policy;
        }

        public static iPolicy generateDateTimePolicyByCategory(string category, DateTime minDate, DateTime maxDate)
        {
            if (minDate > maxDate) return null;
            iPolicy policy = new BasePolicy(p => p.info.category.Equals(category), (p, u) => DateTime.Now < minDate | DateTime.Now > maxDate);
            
            return policy;
        }

        public static iPolicy generateDateTimePolicyByProduct(string name, string category, string man, DateTime minDate, DateTime maxDate)
        {
            if (minDate > maxDate) return null;
            iPolicy policy = new BasePolicy(p => p.info.Equals(ProductInfo.getProductInfo(name, category, man)), (p, u) => DateTime.Now < minDate | DateTime.Now > maxDate);
            
            return policy;
        }

        public static iPolicy generateAndPolicy(ICollection<iPolicy> policies)
        {
            if (policies == null || policies.Count == 0) return null; // ? 0 or less than 2

            iPolicy andPolicy = new AndPolicy();

            foreach (iPolicy policy in policies)
                andPolicy.addPolicy(policy);

            return andPolicy;
        }

        public static iPolicy generateOrPolicy(ICollection<iPolicy> policies)
        {
            if (policies == null || policies.Count == 0) return null; // ? 0 or less than 2

            iPolicy orPolicy = new OrPolicy();

            foreach (iPolicy policy in policies)
                orPolicy.addPolicy(policy);

            return orPolicy;
        }
        
    }
}

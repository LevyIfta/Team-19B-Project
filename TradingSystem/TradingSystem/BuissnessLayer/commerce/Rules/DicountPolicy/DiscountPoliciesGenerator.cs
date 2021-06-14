using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.DicountPolicy
{
    public class DiscountPoliciesGenerator
    {
        // this class has static methods
        // it provides services for generating discount policies

        public static ConditioningPolicyDiscount generateAndPolicyDisc(ICollection<ConditioningPolicyDiscount> policies)
        {
            if (policies == null || policies.Count == 0) return null; // ? 0 or less than 2

            ConditioningPolicyDiscount andPolicy = new AndPolicyDiscount();

            foreach (ConditioningPolicyDiscount policy in policies)
                andPolicy.addPolicy(policy);

            return andPolicy;
        }

        public static ConditioningPolicyDiscount generateOrPolicyDisc(ICollection<ConditioningPolicyDiscount> policies)
        {
            if (policies == null || policies.Count == 0) return null; // ? 0 or less than 2

            ConditioningPolicyDiscount orPolicy = new OrPolicyDiscount();

            foreach (ConditioningPolicyDiscount policy in policies)
                orPolicy.addPolicy(policy);

            return orPolicy;
        }

        public static ConditioningPolicyDiscount generateXorPolicyDisc(ICollection<ConditioningPolicyDiscount> policies)
        {
            if (policies == null || policies.Count == 0) return null; // ? 0 or less than 2

            ConditioningPolicyDiscount XorPolicy = new XorPolicyDiscount();

            foreach (ConditioningPolicyDiscount policy in policies)
                XorPolicy.addPolicy(policy);

            return XorPolicy;
        }

        public static iPolicyDiscount generateDiscountPolicyByProduct(string productName, string category, string man, double discountPercent, DateTime deadLine)
        {
            return new baseDiscountPolicy(p => p.info.Equals(ProductInfo.getProductInfo(productName, category, man)), p => true, deadLine, discountPercent);
        }

        public static iPolicyDiscount generateDiscountPolicyByCategory(string category, double discountPercent, DateTime deadLine)
        {
            return new baseDiscountPolicy(p => p.info.category.Equals(category), p => true, deadLine, discountPercent);
        }
        
        public static ConditioningPolicyDiscount generateMinProductsCondition(string productName, string category, string man, int minAmount)
        {
            return new BaseCondition((basket, totalPrice) =>
            {
                foreach (Product p in basket.products) if (p.info.Equals(ProductInfo.getProductInfo(productName, category, man)) & p.amount >= minAmount)
                        return true;
                return false;
            });
        }

        public static ConditioningPolicyDiscount generateMinProductsConditionByCategory(string category, int minAmount)
        {
            return new BaseCondition((basket, totalPrice) =>
            {
                foreach (Product p in basket.products) if (p.info.category.Equals(category) & p.amount >= minAmount)
                        return true;
                return false;
            });
        }

        public static ConditioningPolicyDiscount generateMinTotalPriceCondition(double minTotalPrice)
        {
            return new BaseCondition((basket, totalPrice) =>
            {
                return totalPrice >= minTotalPrice;
            });
        }

        
    }
}

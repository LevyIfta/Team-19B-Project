using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.DicountPolicy
{
    class DicountPoliciesGenerator
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
    }
}

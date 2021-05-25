using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.commerce.Rules;

namespace TradingSystem.ServiceLayer.Controllers
{
    class RulesController
    {
        private static readonly Lazy<RulesController> instanceLazy = new Lazy<RulesController>(() => new RulesController(), true);

        private readonly MarketRules marketRules;
        private IDictionary<string, ICollection<IRule>> user_rules;

        private RulesController()
        {
            marketRules = MarketRules.Instance;
            user_rules = new Dictionary<string, ICollection<IRule>>();
        }

        public Guid CreateRule(string username, RuleContext ruleContext, RuleType ruleType, string category = "", Guid productId = new Guid(), string ruleUsername = "", double valueLessThan = int.MaxValue, double valueGreaterEQThan = 0, DateTime d1 = new DateTime(), DateTime d2 = new DateTime())
        {
            var r = marketRules.CreateRule(ruleContext, ruleType, category, productId, ruleUsername, valueLessThan, valueGreaterEQThan, d1, d2);
            if (user_rules.Keys.Contains(username))
            {
                user_rules.Add(username, new HashSet<IRule>());
            }
            user_rules[username].Add(r);
            return r.GetId();
        }


        //Add New / Complex Discounts
        public Guid AddSimpleDiscount(string username, Guid storeId, RuleContext discountType, double precent, string category = "", Guid productId = new Guid())
        {
            return marketRules.CreateSimpleDiscount(username, storeId, discountType, precent, category, productId);
        }
        public Guid AddConditionalDiscount(string username, Guid storeId, RuleContext discountType, RuleType ruleType, double precent, string category = "", Guid productId = new Guid(), string ruleUsername = "",
                                        double valueLessThan = int.MaxValue, double valueGreaterEQThan = 0, DateTime d1 = new DateTime(), DateTime d2 = new DateTime())
        {
            return marketRules.CreateConditionalDiscount(username, storeId, discountType, ruleType, precent, category, productId, ruleUsername, valueLessThan, valueGreaterEQThan, d1, d2);
        }

        public Guid AddDiscountRule(string username, DiscountRuleRelation discountRuleRelation, Guid storeId, Guid ruleId1, Guid ruleId2, Guid discountId, Guid discountId2 = new Guid(), bool decide = false)
        {
            return marketRules.GenerateConditionalDiscounts(username, discountRuleRelation, storeId, ruleId1, ruleId2, discountId, discountId2, decide);
        }

        public Guid RemoveDiscount(string username, Guid storeId, Guid discountId)
        {
            return marketRules.RemoveDiscount(username, storeId, discountId);
        }

        //Add New / Complex Policy
        public void AddPolicyRule(string username, Guid storeId, PolicyRuleRelation policyRuleRelation, RuleContext ruleContext, RuleType ruleType, string category = "", Guid productId = new Guid(), string ruleUsername = "",
                                        double valueLessThan = int.MaxValue, double valueGreaterEQThan = 0, DateTime d1 = new DateTime(), DateTime d2 = new DateTime())
        {
            marketRules.AddPolicyRule(username, storeId, policyRuleRelation, ruleContext, ruleType, category, productId, ruleUsername, valueLessThan, valueGreaterEQThan, d1, d2);
        }
        public void RemovePolicyRule(string username, Guid storeId)
        {
            marketRules.RemovePolicyRule(username, storeId);
        }
    }
}

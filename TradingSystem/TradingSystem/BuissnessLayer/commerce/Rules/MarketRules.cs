using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.commerce.Rules.Discount;

namespace TradingSystem.BuissnessLayer.commerce.Rules
{
    public enum RuleContext
    {
        Store,
        Category,
        Product
    }
    public enum RuleType
    {
        Age,
        Quantity,
        Price,
        Weight,
        Time
    }
    public enum PolicyRuleRelation
    {
        Simple,
        And,
        Or,
        Condition
    }
    public enum DiscountRuleRelation
    {
        And,
        Or,
        Xor
    }

    class MarketRules
    {
     /*   private RulesCreator rulesCreator;
        private IMarketStores marketStores;

        private static readonly Lazy<MarketRules>
        _lazy =
        new Lazy<MarketRules>
            (() => new MarketRules());

        public static MarketRules Instance { get { return _lazy.Value; } }

        private MarketRules()
        {
            this.rulesCreator = new RulesCreator();
            this.marketStores = MarketStores.Instance;
        }

        public Guid CreateSimpleDiscount(string username, Guid storeId, RuleContext discountType, double precent, string category = "", Guid productId = new Guid())
        {
            Store store = marketStores.GetStoreById(storeId);
            var d = CreateCalculator(discountType, precent, category, productId);
            Discount.Discount discount = new Discount.Discount(d);
            return store.AddDiscount(username, discount);
        }
        public Guid CreateConditionalDiscount(string username, Guid storeId, RuleContext discountType, RuleType ruleType, double precent, string category = "", Guid productId = new Guid(), string ruleUsername = "",
            double valueLessThan = int.MaxValue, double valueGreaterEQThan = 0,
            DateTime d1 = new DateTime(), DateTime d2 = new DateTime())
        {
            Store store = marketStores.GetStoreById(storeId);
            var d = CreateCalculator(discountType, precent, category, productId);
            var r = CreateRule(discountType, ruleType, category, productId, ruleUsername, valueLessThan, valueGreaterEQThan, d1, d2);
            ConditionDiscount discount = new ConditionDiscount(d);
            discount.AddRule(r);
            return store.AddDiscount(username, discount);
        }

        public Guid GenerateConditionalDiscounts(string username, DiscountRuleRelation discountRuleRelation, Guid storeId, Guid ruleId1, Guid ruleId2, Guid discountId, Guid discountId2, bool decide)
        {
            switch (discountRuleRelation)
            {
                case DiscountRuleRelation.And:
                    return AddDiscountAndRule(username, storeId, ruleId1, ruleId2, discountId);
                case DiscountRuleRelation.Or:
                    return AddDiscountOrRule(username, storeId, ruleId1, ruleId2, discountId);
                default:
                    return AddDiscountXorRule(username, storeId, discountId, discountId2, decide);
            }
        }

        public Guid AddDiscountAndRule(string username, Guid storeId, Guid ruleId1, Guid ruleId2, Guid discountId)
        {
            Store store = marketStores.GetStoreById(storeId);
            var rule1 = store.GetDiscountRuleById(ruleId1);
            var rule2 = store.GetDiscountRuleById(ruleId2);
            var andRule = Rule.AddTwoRules(rule1, rule2);
            var discount = (ConditionDiscount)store.GetDiscountById(discountId);
            discount.AddRule(andRule);
            return store.AddDiscount(username, discount);
        }

        public Guid AddDiscountOrRule(string username, Guid storeId, Guid ruleId1, Guid ruleId2, Guid discountId)
        {
            Store store = marketStores.GetStoreById(storeId);
            var rule1 = store.GetDiscountRuleById(ruleId1);
            var rule2 = store.GetDiscountRuleById(ruleId2);
            var andRule = Rule.OrTwoRules(rule1, rule2);
            var discount = (ConditionDiscount)store.GetDiscountById(discountId);
            discount.AddRule(andRule);
            return store.AddDiscount(username, discount);
        }

        public Guid AddDiscountXorRule(string username, Guid storeId, Guid discountId1, Guid discountId2, bool decide)
        {
            Store store = marketStores.GetStoreById(storeId);
            var discount1 = (ConditionDiscount)store.GetDiscountById(discountId1);
            var discount2 = (ConditionDiscount)store.GetDiscountById(discountId2);
            var xorDiscount = discount1.Xor(discount2, decide);
            return store.AddDiscount(username, xorDiscount);
        }

        public Guid RemoveDiscount(string username, Guid storeId, Guid discountId)
        {
            Store store = marketStores.GetStoreById(storeId);
            return store.RemoveDiscount(username, discountId);
        }

        public void AddPolicyRule(string username, Guid storeId, PolicyRuleRelation policyRuleRelation, RuleContext ruleContext, RuleType ruleType, string category = "", Guid productId = new Guid(), string usernameRule = "",
            double valueLessThan = int.MaxValue, double valueGreaterEQThan = 0, DateTime d1 = new DateTime(), DateTime d2 = new DateTime())
        {
            var r = CreateRule(ruleContext, ruleType, category, productId, usernameRule, valueLessThan, valueGreaterEQThan, d1, d2);
            switch (policyRuleRelation)
            {
                case PolicyRuleRelation.Simple:
                    CreateRuleToStore(username, storeId, r);
                    break;
                case PolicyRuleRelation.And:
                    GeneratePolicyAndRule(username, storeId, r);
                    break;
                case PolicyRuleRelation.Or:
                    GeneratePolicyOrRule(username, storeId, r);
                    break;
                default:
                    GeneratePolicyConditionRule(username, storeId, r);
                    break;
            }
        }

        public Guid CreateRuleToStore(string username, Guid storeId, Rule rule)
        {
            Store store = marketStores.GetStoreById(storeId);
            return store.AddRule(username, rule);
        }

        public void GeneratePolicyAndRule(string username, Guid storeId, Rule rule)
        {
            Store store = marketStores.GetStoreById(storeId);
            var newPolicy = store.GetPolicy().And(rule);
            store.SetPolicy(username, newPolicy);
        }

        public void GeneratePolicyOrRule(string username, Guid storeId, Rule rule)
        {
            Store store = marketStores.GetStoreById(storeId);
            var newPolicy = store.GetPolicy().Or(rule);
            store.SetPolicy(username, newPolicy);
        }

        public void GeneratePolicyConditionRule(string username, Guid storeId, Rule rule)
        {
            Store store = marketStores.GetStoreById(storeId);
            var newPolicy = store.GetPolicy().Condition(rule);
            store.SetPolicy(username, newPolicy);
        }

        public void RemovePolicyRule(string username, Guid storeId)
        {
            Store store = marketStores.GetStoreById(storeId);
            store.RemoveRule(username);
        }

        public Rule CreateRule(RuleContext discountType, RuleType ruleType, string category, Guid productId, string usernameRule, double valueLessThan, double valueGreaterEQThan, DateTime d1, DateTime d2)
        {
            Rule r;
            switch (ruleType)
            {
                case RuleType.Age:
                    r = rulesCreator.CreateUserAgeRule(usernameRule, Convert.ToInt32(valueLessThan), Convert.ToInt32(valueGreaterEQThan));
                    break;
                case RuleType.Price:
                    r = rulesCreator.CreateStorePriceRule(Convert.ToInt32(valueLessThan), Convert.ToInt32(valueGreaterEQThan));
                    break;
                case RuleType.Quantity:
                    switch (discountType)
                    {
                        case RuleContext.Store:
                            r = rulesCreator.CreateStoreRule(Convert.ToInt32(valueLessThan), Convert.ToInt32(valueGreaterEQThan));
                            break;
                        case RuleContext.Category:
                            r = rulesCreator.CreateCategoryRule(category, Convert.ToInt32(valueLessThan), Convert.ToInt32(valueGreaterEQThan));
                            break;
                        default:
                            r = rulesCreator.CreateProductRule(productId, Convert.ToInt32(valueLessThan), Convert.ToInt32(valueGreaterEQThan));
                            break;
                    }
                    break;
                case RuleType.Weight:
                    r = rulesCreator.CreateProductWeightRule(productId, Convert.ToInt32(valueLessThan), Convert.ToInt32(valueGreaterEQThan));
                    break;
                default:
                    r = rulesCreator.CreateTimeRule(d1, d2);
                    break;
            }
            return r;
        }

        private IDiscount CreateCalculator(RuleContext discountType, double precent, string category = "", Guid productId = new Guid())
        {
            IDiscount d;
            switch (discountType)
            {
                case RuleContext.Store:
                    d = new StoreDiscountCalc(precent);
                    break;
                case RuleContext.Category:
                    d = new CategoryDiscountCalc(category, precent);
                    break;
                default:
                    d = new ProductDiscountCalc(productId, precent);
                    break;
            }
            return d;
        }*/
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.policy
{
    public class Policy
    {
        private IRule _rule;

        public IRule Rule { get => _rule; set => _rule = value; }

        public Policy()
        {

        }

        public Policy(IRule rule)
        {
            this._rule = rule;
        }

        public void AddRule(IRule rule)
        {
            _rule = rule;
        }

        public void RemoveRule()
        {
            _rule = null;
        }

        public bool Check(ShoppingBasket shoppingBasket)
        {
            return _rule == null || _rule.Check(shoppingBasket);
        }

        public Policy And(IRule additionalPolicy)
        {
            IRule and = new Rule(new Func<ShoppingBasket, bool>((ShoppingBasket shoppingBasket) => Check(shoppingBasket) && additionalPolicy.Check(shoppingBasket)));
            return new Policy(and);
        }

        public Policy Or(IRule orPolicy)
        {
            IRule or = new Rule(new Func<ShoppingBasket, bool>((ShoppingBasket shoppingBasket) => Check(shoppingBasket) || orPolicy.Check(shoppingBasket)));
            return new Policy(or);
        }

        public Policy Condition(IRule orPolicy)
        {
            IRule condition = new Rule(new Func<ShoppingBasket, bool>((ShoppingBasket shoppingBasket) => Check(shoppingBasket) ? orPolicy.Check(shoppingBasket) : true));
            return new Policy(condition);
        }
    }
}

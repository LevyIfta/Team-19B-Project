using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules
{
    public class BasePolicy : iPolicy
    {

        //public ProductInfo subject { get; set; }
        //public int amount { get; set; }
        public Func<Product, aUser, bool> predicate { get; set; }
        public Func<Product, bool> isRelevant;
        public bool Default { get; set; }
        
        public BasePolicy(Func<Product, bool> isRelevant, Func<Product, aUser, bool> pred)
        {
            this.isRelevant = isRelevant;
            this.predicate = pred;
            this.Default = true;
        }

        public BasePolicy(Func<Product, bool> isRelevant, Func<Product, aUser, bool> pred, bool isRequired)
        {
            this.isRelevant = isRelevant;
            this.predicate = pred;
            this.Default = !isRequired;
        }

        public override bool isValid(ICollection<Product> products, aUser user)
        {
            foreach (Product item in products)
            {
                if (this.isRelevant(item))
                    return predicate(item, user);
            }
            return this.Default;
        }
    }
}

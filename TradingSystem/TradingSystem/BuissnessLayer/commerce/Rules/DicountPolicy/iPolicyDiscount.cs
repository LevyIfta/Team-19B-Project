using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer.commerce.Rules.DicountPolicy
{
    public abstract class iPolicyDiscount
    {

        public List<ConditioningPolicyDiscount> policies = new List<ConditioningPolicyDiscount>();
        private Guid id { get; set; } = Guid.NewGuid();

        public iPolicyDiscount(iPolicyDiscountData ipd)
        {
            //todo
        }

        protected iPolicyDiscount()
        {
        }

        public iPolicyDiscountData toDataObject()
        {
            return null; //todo
        }

        public abstract double ApplyDiscount(ShoppingBasket basket, double totalPrice);
        //public abstract bool isValid(ICollection<Product> products, double totalprice);


        public bool check_discount_deadline(DateTime deadline)
        {
            return DateTime.Compare(DateTime.Now, deadline) < 0; //check < or >
        }

        public double calculateDiscountPrice(Product product, double discount_percent)
        {
            return  product.price - (product.price * discount_percent);
        }

        public void addCondition(ConditioningPolicyDiscount policy)
        {
            this.policies.Add(policy);
        }
        public bool removeCondition()
        {
            if (this.policies.Count == 0) return false;
            this.policies.Remove(this.policies[this.policies.Count - 1]);
            return true;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer.commerce.Rules
{
    public abstract class iPolicy
    {
        public List<iPolicy> policies = new List<iPolicy>();
        public abstract bool isValid(ICollection<Product> products, aUser user);

        public iPolicy(iPolicyData policy)
        {
            //todo
        }

        protected iPolicy()
        {
        }

        public iPolicyData toDataObject()
        {
            return null; //todo
        }


        public void addPolicy(iPolicy policy)
        {
            this.policies.Add(policy);
        }

        public bool removePolicy()
        {
            if (this.policies.Count == 0) return false;
            this.policies.Remove(this.policies[this.policies.Count - 1]);
            return true;
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.DicountPolicy
{
    public abstract class iPolicyDiscount
    {

        public List<iPolicyDiscount> policies = new List<iPolicyDiscount>();

        public abstract void ApplyDiscount(ICollection<Product> products);
        public abstract bool isValid(ICollection<Product> products, double totalprice);


        public bool check_discount_deadline(DateTime deadline)
        {
            return DateTime.Compare(DateTime.Now, deadline) < 0; //check < or >
        }

        public double calculateDiscountPrice(Product product, double discount_percent)
        {
            return  product.price - (product.price * discount_percent);
        }

        public void addPolicy(iPolicyDiscount policy)
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
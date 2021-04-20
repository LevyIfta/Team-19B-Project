using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer
{
    class Store
    {
        public string name { get; private set; }
        public ICollection<Reciept> recipts { get; private set; }
        public ICollection<Product> inventory { get; private set; }
        public Member founder { get; private set; }

        public Store(string name, Member founder)
        {
            this.name = name;
            this.founder = founder;
            this.recipts = new List<Reciept>();
            this.inventory = new List<Product>();
        }

        public double calcPrice(ICollection<Product> products)
        {
            throw new NotImplementedException();
        }

        public Reciept executePurchase(ICollection<Product> products)
        {
            throw new NotImplementedException();
        }
    }
}

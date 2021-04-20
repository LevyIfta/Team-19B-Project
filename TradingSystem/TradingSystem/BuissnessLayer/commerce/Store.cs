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
        public ICollection<aUser> owners { get; private set; }
        public ICollection<aUser> managers { get; private set; }
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
        public void addOwner(string username)
        {

        }
        public void addManager(string username)
        {

        }
        public void removeOwner(string username)
        {
        }

        public void removeManager(string username)
        {
        }

        public bool isManager(string userrname)
        {
            throw new NotImplementedException();
        }

        public bool isOwner(string username)
        {
            throw new NotImplementedException();
        }

        public ICollection<aUser> getOwners()
        {
            return owners;
        }

        public ICollection<aUser> getManagers()
        {
            return managers;
        }

    }
}

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
        public ICollection<string> owners { get; private set; }
        public ICollection<string> managers { get; private set; }
        public Member founder { get; private set; }

        public Store(string name, Member founder)
        {
            this.name = name;
            this.founder = founder;
            this.recipts = new List<Reciept>();
            this.inventory = new List<Product>();
            this.owners = new List<string>();
            this.managers = new List<string>();
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
            this.owners.Add(username);
        }
        public void addManager(string username)
        {
            this.managers.Add(username);
        }
        public void removeOwner(string username)
        {
            this.owners.Remove(username);
        }

        public void removeManager(string username)
        {
            this.managers.Remove(username);
        }

        public bool isManager(string userrname)
        {
            return this.managers.Contains(userrname);
        }

        public bool isOwner(string username)
        {
            return this.owners.Contains(username);
        }

        public ICollection<string> getOwners()
        {
            return owners;
        }

        public ICollection<string> getManagers()
        {
            return managers;
        }

    }
}

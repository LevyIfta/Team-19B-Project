using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer
{
    class Store
    {
        public string name { get; private set; }
        public ICollection<Reciept> recipts { get; private set; }
        public ICollection<Product> inventory { get; private set; }
        public ICollection<Member> owners { get; private set; }
        public ICollection<Member> managers { get; private set; }
        public Member founder { get; private set; }

        public Store(string name, Member founder)
        {
            this.name = name;
            this.founder = founder;
            this.recipts = new List<Reciept>();
            this.inventory = new List<Product>();
            this.owners = new List<Member>();
            this.managers = new List<Member>();
        }

        public double calcPrice(ICollection<Product> products)
        {
            throw new NotImplementedException();
        }

        public Reciept executePurchase(ICollection<Product> products)
        {
            throw new NotImplementedException();
        }
        public void addOwner(Member username)
        {
            this.owners.Add(username);
        }
        public void addManager(Member username)
        {
            this.managers.Add(username);
        }
        public void removeOwner(Member username)
        {
            this.owners.Remove(username);
        }

        public void removeManager(Member username)
        {
            this.managers.Remove(username);
        }

        public bool isManager(Member userrname)
        {
            return this.managers.Contains(userrname);
        }

        public bool isOwner(Member username)
        {
            return this.owners.Contains(username);
        }

        public ICollection<Member> getOwners()
        {
            return owners;
        }

        public ICollection<Member> getManagers()
        {
            return managers;
        }

        public StoreData toDataObject()
        {
            return null;
        }

    }
}

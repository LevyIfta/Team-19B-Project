using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer
{
    public class Store
    {
        public string name { get; private set; }
        public ICollection<Receipt> recipts { get; private set; }
        public ICollection<Product> inventory { get; private set; }
        public ICollection<aUser> owners { get; private set; }
        public ICollection<aUser> managers { get; private set; }
        public Member founder { get; private set; }

        public Store(string name, Member founder)
        {
            this.name = name;
            this.founder = founder;
            this.recipts = new List<Receipt>();
            this.inventory = new List<Product>();
        }

        public double calcPrice(ICollection<Product> products)
        {
            throw new NotImplementedException();
        }

        public Receipt executePurchase(ShoppingBasket basket, PaymentMethod paymentMethod)
        {
            throw new NotImplementedException();
        }
        public void addOwner(Member owner)
        {

        }
        public void addManager(Member manager)
        {

        }
        public void removeOwner(Member owner)
        {
        }

        public void removeManager(Member manager)
        {
        }

        public bool isManager(string manager)
        {
            throw new NotImplementedException();
        }

        public bool isOwner(string owner)
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
        public ICollection<Receipt> getAllReceipts()
        {
            throw new NotImplementedException();
        }
        public bool isProductExist(string name, string manufacturer)
        {
            throw new NotImplementedException();
        }
        public bool editPrice(string productName, string manufacturer, double newPrice)
        {
            throw new NotImplementedException();
        }
        public bool supply(string name, string manufacturer, int amount)
        {
            throw new NotImplementedException();
        }
        public ProductInfo addProduct(string name, string category, string manufacturer)
        {
            throw new NotImplementedException();
        }
        public void removeProduct(string name, string manufacturer)
        {
            throw new NotImplementedException();
        }
        public override bool Equals(object obj)
        {
            return false;
        }
        public bool Equals(Store obj)
        {
            return obj.name.Equals(name);
        }

    }
}

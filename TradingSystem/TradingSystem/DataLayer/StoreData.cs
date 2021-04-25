
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer;

namespace TradingSystem.DataLayer
{
    class StoreData
    {
        public string name { get; set; }
        public ICollection<ReceiptData> receipts { get; private set; }
        public ICollection<ProductData> inventory { get; private set; }
        public ICollection<memberData> owners { get; private set; }
        public ICollection<memberData> managers { get; private set; }
        public memberData founder { get; private set; }
        private Object purchaseLock = new Object();

        public StoreData(string name, memberData founder)
        {
            this.name = name;
            this.founder = founder;
        }

        public void addOwner(memberData owner)
        {
            this.owners.Add(owner);
        }

        public void addManager(memberData manager)
        {
            this.managers.Add(manager);
        }

        public ICollection<memberData> getOwners()
        {
            return this.owners;
        }

        public ICollection<memberData> getManagers()
        {
            return this.managers;
        }

        public void removeOwner(memberData owner)
        {
            // this loop is needed because memberData is not IEquatable
            foreach (memberData o in this.owners)
                if (o.CompareTo(owner) == 0)
                    this.owners.Remove(o);
        }

        public void removeManager(memberData manager)
        {
            // this loop is needed because memberData is not IEquatable
            foreach (memberData m in this.managers)
                if (m.CompareTo(manager) == 0)
                    this.managers.Remove(m);
        }

        public Object getPurchaseLock() { return this.purchaseLock; }

        public ICollection<ReceiptData> getReceipts()
        {
            return this.receipts;
        }

        public ICollection<ProductData> getInventory()
        {
            return this.inventory;
        }

        public void removeProducts(ProductData productData)
        {
            throw new NotImplementedException();
        }

        internal void addProduct(ProductData productData)
        {
            this.inventory.Add(productData);
        }

        public double getPrice(ProductInfoData product)
        {
            foreach (ProductData productData in this.inventory)
                if (productData.info.Equals(product))
                    return productData.price;
            return 0.0;
        }
    }
}
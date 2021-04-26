using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    class StoreDAL_IsITNeeded
    {

        public void addOwner(MemberData owner)
        {
            this.owners.Add(owner);
        }

        public void addManager(MemberData manager)
        {
            this.managers.Add(manager);
        }

        public ICollection<MemberData> getOwners()
        {
            return this.owners;
        }

        public ICollection<MemberData> getManagers()
        {
            return this.managers;
        }

        public void removeOwner(MemberData owner)
        {
            // this loop is needed because memberData is not IEquatable
            foreach (MemberData o in this.owners)
                if (o.CompareTo(owner) == 0)
                    this.owners.Remove(o);
        }

        public void removeManager(MemberData manager)
        {
            // this loop is needed because memberData is not IEquatable
            foreach (MemberData m in this.managers)
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

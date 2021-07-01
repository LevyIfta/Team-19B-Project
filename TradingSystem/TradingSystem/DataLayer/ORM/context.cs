using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer.Permissions;

namespace TradingSystem.DataLayer.ORM
{
    class context : DbContext
    {
        public static context instance { get; private set; } = null;

        static context()
        {
            instance = new context();
            

        }

        private context() : base() {
            this.Database.EnsureCreated();
           
        }

        private context(DbContextOptions opt) : base(opt)
        {
            this.Database.EnsureCreated();
           
        }


        public DbSet<MemberData> members { get; set; }
        public DbSet<BasketInCart> basketsInCarts { get; set; }
        public DbSet<BasketInRecipt> basketsInReceipts { get; set; }
        public DbSet<FeedbackData> feedbacks { get; set; }
        public DbSet<iPolicyData> policies { get; set; }
        public DbSet<iPolicyDiscountData> discountPolicies { get; set; }
        public DbSet<MessageData> messages { get; set; }
        public DbSet<ProductData> products { get; set; }
        public DbSet<ProductInfoData> productInfos { get; set; }
        public DbSet<ReceiptData> recipts { get; set; }
        public DbSet<StoreData> stores { get; set; }

        //permissions
        public DbSet<addProductPermissionData> addProductPermissions { get; set; }
        public DbSet<editManagerPermissionsData> editManagerPermissions{ get; set; }
        public DbSet<editProductPermissionData> editProductPermissions { get; set; }
        public DbSet<editManagerPermissionsData> getInfoEmployeesPermissions { get; set; }
        public DbSet<getPurchaseHistoryPermissionData> getPurchaseHistoryPermissions { get; set; }
        public DbSet<hireNewStoreManagerPermissionData> hireNewStoreManagerPermissions { get; set; }
        public DbSet<hireNewStoreOwnerPermissionData> hireNewStoreOwnerPermissions { get; set; }
        public DbSet<removeManagerPermissionData> removeManagerPermissions { get; set; }
        public DbSet<removeProductPermissionData> removeProductPermissions { get; set; }
        public DbSet<removeOwnerPermissionData> removeOwnerPermissions { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder options)
      => options.UseSqlite("DataSource=" + ServiceLayer.Connection.ConfigurationFile.DBName);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BasketInCart>().HasKey(u => new
            {
                u.storeName,
                u.userName
            });

            modelBuilder.Entity<FeedbackData>().HasKey(u => new
            {
                u.productID,
                u.userName
            });
            modelBuilder.Entity<aPermissionData>().HasKey(u => new
            {
                u.myOwnerName,
                u.store
            });


            /*
                        modelBuilder.Entity<ReceiptData>()
                        .HasOne(p => p.discount)
                        .WithMany(b => b.recipts);
                        modelBuilder.Entity<ReceiptData>()
                        .HasOne(p => p.purchasePolicy)
                        .WithMany(b => b.recipts);*/
        }


        public void tearDown()
        {
            members.RemoveRange(members);
            basketsInCarts.RemoveRange(basketsInCarts);
            basketsInReceipts.RemoveRange(basketsInReceipts);
            feedbacks.RemoveRange(feedbacks);
            policies.RemoveRange(policies);
            discountPolicies.RemoveRange(discountPolicies);
            messages.RemoveRange(messages);
            products.RemoveRange(products);
            productInfos.RemoveRange(productInfos);
            recipts.RemoveRange(recipts);
            stores.RemoveRange(stores);
            addProductPermissions.RemoveRange(addProductPermissions);
            editManagerPermissions.RemoveRange(editManagerPermissions);
            editProductPermissions.RemoveRange(editProductPermissions);
            getInfoEmployeesPermissions.RemoveRange(getInfoEmployeesPermissions);
            getPurchaseHistoryPermissions.RemoveRange(getPurchaseHistoryPermissions);
            hireNewStoreManagerPermissions.RemoveRange(hireNewStoreManagerPermissions);
            hireNewStoreOwnerPermissions.RemoveRange(hireNewStoreOwnerPermissions);
            removeManagerPermissions.RemoveRange(removeManagerPermissions);
            removeProductPermissions.RemoveRange(removeProductPermissions);
            removeOwnerPermissions.RemoveRange(removeOwnerPermissions);
        }

    }
}

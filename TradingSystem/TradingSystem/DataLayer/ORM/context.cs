using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer;
using TradingSystem.BuissnessLayer.commerce;

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
        public DbSet<BasePolicyData> basePolicies { get; set; }
        public DbSet<AndPolicyData> andPolicies { get; set; }
        public DbSet<OrPolicyData> orPolicies { get; set; }
        public DbSet<ConditioningPolicyData> condPolicies { get; set; }
        public DbSet<iPolicyDiscountData> discountPolicies { get; set; }
        public DbSet<MessageData> messages { get; set; }
        public DbSet<ProductData> products { get; set; }
        public DbSet<ProductInfoData> productInfos { get; set; }
        public DbSet<ReceiptData> recipts { get; set; }
        public DbSet<StoreData> stores { get; set; }



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

            modelBuilder.Entity<BasePolicyData>().HasKey(u => new
            {
                u.id,
                u.storeName
            });
            

            modelBuilder.Entity<AndPolicyData>().HasKey(u => new
            {
                u.id,
                u.storeName
            });

            modelBuilder.Entity<OrPolicyData>().HasKey(u => new
            {
                u.id,
                u.storeName
            });

            modelBuilder.Entity<ConditioningPolicyData>().HasKey(u => new
            {
                u.id,
                u.storeName
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
            basePolicies.RemoveRange(basePolicies);
            discountPolicies.RemoveRange(discountPolicies);
            messages.RemoveRange(messages);
            products.RemoveRange(products);
            productInfos.RemoveRange(productInfos);
            recipts.RemoveRange(recipts);
            stores.RemoveRange(stores);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer.Permissions;

namespace TradingSystem.DataLayer.ORM
{
  

    class DataAccessInit
    {
        protected static context context { get { return context.instance; } set { } }

        private static bool initiated = false;
        protected static object Lock = new object();

        static DataAccessInit()
        {
            lock (Lock)
            {
                if (!initiated)
                {
                    foreach (var item in context.members) ;
                    foreach (var item in context.basketsInCarts) ;
                    foreach (var item in context.basketsInReceipts) ;
                    foreach (var item in context.feedbacks) ;
                    foreach (var item in context.policies) ;
                    foreach (var item in context.discountPolicies) ;
                    foreach (var item in context.messages) ;
                    foreach (var item in context.products) ;
                    foreach (var item in context.productInfos) ;
                    foreach (var item in context.recipts) ;
                    foreach (var item in context.stores) ;
                    foreach (var item in context.addProductPermissions) ;
                    foreach (var item in context.editManagerPermissions) ;
                    foreach (var item in context.editProductPermissions) ;
                    foreach (var item in context.getInfoEmployeesPermissions) ;
                    foreach (var item in context.getPurchaseHistoryPermissions) ;
                    foreach (var item in context.hireNewStoreManagerPermissions) ;
                    foreach (var item in context.hireNewStoreOwnerPermissions) ;
                    foreach (var item in context.removeManagerPermissions) ;
                    foreach (var item in context.removeProductPermissions) ;
                    foreach (var item in context.removeOwnerPermissions) ;

                    initiated = true;
                }
            }
            
        }


        public static void tearDown()
        {
            context.tearDown();
        }
       
    }

    class DataAccessCreate : DataAccessInit
    {
        public static void create(BasketInCart basket)
        {
            lock (Lock)
            {
                context.basketsInCarts.Add(basket);
                context.SaveChanges();
            }
        }
        public static void create(BasketInRecipt basket)
        {
            lock (Lock)
            {
                context.basketsInReceipts.Add(basket);
                context.SaveChanges();
            }
        }
        public static void create(FeedbackData feedback)
        {
            lock (Lock)
            {
                context.feedbacks.Add(feedback);
                context.SaveChanges();
            }
        }
        public static void create(iPolicyData policy)
        {
            lock (Lock)
            {
                context.policies.Add(policy);
                context.SaveChanges();
            }
        }
        public static void create(iPolicyDiscountData discountPolicy)
        {
            lock (Lock)
            {
                context.discountPolicies.Add(discountPolicy);
                context.SaveChanges();
            }
        }
        public static void create(MemberData member)
        {
            lock (Lock)
            {
                if (member.userName.Equals("guest"))
                    return;
                context.members.Add(member);
                //context.Entry(member).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                context.SaveChanges();
            }
        }
        public static void create(MessageData message)
        {
            lock (Lock)
            {
                context.messages.Add(message);
                context.SaveChanges();
            }
        }
        public static void create(ProductData product)
        {
            lock (Lock)
            {
                context.products.Add(product);
                context.SaveChanges();
            }
        }
        public static void create(ProductInfoData productInfo)
        {
            lock (Lock)
            {
                context.productInfos.Add(productInfo);
                context.SaveChanges();
            }
        }
        public static void create(ReceiptData receipt)
        {
            lock (Lock)
            {
                context.recipts.Add(receipt);
                context.SaveChanges();
            }
        }
        public static void create(StoreData store)
        {
            lock (Lock)
            {
                MemberData member = DataAccess.getMember(store.founder.userName);
                store.founder = member;
                context.stores.Add(store);
                context.SaveChanges();
            }
        }
    }

    class DataAccessDelete : DataAccessCreate
    {
        public static void Delete(BasketInCart basket, bool propogate = false)
        {
            lock (Lock)
            {
               if(propogate)
                {
                    foreach (var item in basket.products)
                    {
                        Delete(item, propogate);
                    }
                }
                context.basketsInCarts.Remove(basket);
                
                context.SaveChanges();
            }
        }
        public static void Delete(BasketInRecipt basket, bool propogate = false)
        {
            lock (Lock)
            {
                if (propogate)
                {
                    foreach (var item in basket.products)
                    {
                        Delete(item, propogate);
                    }
                }
                context.basketsInReceipts.Remove(basket);

                context.SaveChanges();
            }
        }
        public static void Delete(FeedbackData feedback, bool propogate = false)
        {
            lock (Lock)
            {
                context.feedbacks.Remove(feedback);
                context.SaveChanges();
            }
        }
        public static void Delete(iPolicyData policy, bool propogate = false)
        {
            lock (Lock)
            {
                context.policies.Remove(policy);
                context.SaveChanges();
            }
        }
        public static void Delete(iPolicyDiscountData discountPolicy, bool propogate = false)
        {
            lock (Lock)
            {
                context.discountPolicies.Remove(discountPolicy);
                context.SaveChanges();
            }
        }
        public static void Delete(MemberData member, bool propogate = false)
        {
            lock (Lock)
            {
                if(propogate)
                {
                    foreach (var item in member.receipts)
                    {
                        Delete(item, propogate);
                    }
                    foreach (var item in member.shopingcart)
                    {
                        Delete(item, propogate);
                    }
                }
                if (member.userName.Equals("guest"))
                    return;
                context.members.Remove(member);
                context.SaveChanges();
            }
        }
        public static void Delete(MessageData message, bool propogate = false)
        {
            lock (Lock)
            {
                context.messages.Remove(message);
                context.SaveChanges();
            }
        }
        public static void Delete(ProductData product, bool propogate = false)
        {
            lock (Lock)
            {
                context.products.Remove(product);
                context.SaveChanges();
            }
        }
        public static void Delete(ProductInfoData productInfo, bool propogate = false)
        {
            lock (Lock)
            {
                context.productInfos.Remove(productInfo);
                context.SaveChanges();
            }
        }
        public static void Delete(ReceiptData receipt, bool propogate = false)
        {
            lock (Lock)
            {
                if (propogate)
                {
                    Delete(receipt.basket, propogate);
                  
                }
                context.recipts.Remove(receipt);
                context.SaveChanges();
            }
        }
        public static void Delete(StoreData store, bool propogate = false)
        {
            lock (Lock)
            {
                if (propogate)
                {
                    foreach (var item in store.discountPolicies)
                    {
                        Delete(item, propogate);
                    }
                    foreach (var item in store.purchasePolicies)
                    {
                        Delete(item, propogate);
                    }
                    foreach (var item in store.inventory)
                    {
                        Delete(item, propogate);
                    }
                }
                context.stores.Remove(store);
                context.SaveChanges();
            }

        }
    }

    class DataAccessUpdate : DataAccessDelete
    {

        public static void update(BasketInCart basket)
        {
            lock (Lock)
            {
                context.basketsInCarts.Update(basket);
                context.SaveChanges();
            }
        }
        public static void update(BasketInRecipt basket)
        {
            lock (Lock)
            {
                context.basketsInReceipts.Update(basket);
                context.SaveChanges();
            }
        }
        public static void update(FeedbackData feedback)
        {
            lock (Lock)
            {
                context.feedbacks.Update(feedback);
                context.SaveChanges();
            }
        }
        public static void update(iPolicyData policy)
        {
            lock (Lock)
            {
                context.policies.Update(policy);
                context.SaveChanges();
            }
        }
        public static void update(iPolicyDiscountData discountPolicy)
        {
            lock (Lock)
            {
                context.discountPolicies.Update(discountPolicy);
                context.SaveChanges();
            }
        }
        public static void update(MemberData member)
        {
            lock (Lock)
            {
                if (member.userName.Equals("guest"))
                    return;
                context.members.Update(member);
                context.SaveChanges();
            }
        }
        public static void update(MessageData message)
        {
            lock (Lock)
            {
                context.messages.Update(message);
                context.SaveChanges();
            }
        }
        public static void update(ProductData product)
        {
            lock (Lock)
            {
                context.products.Update(product);
                context.SaveChanges();
            }
        }
        public static void update(ProductInfoData productInfo)
        {
            lock (Lock)
            {
                context.productInfos.Update(productInfo);
                context.SaveChanges();
            }
        }
        public static void update(ReceiptData receipt)
        {
            lock (Lock)
            {
                context.recipts.Update(receipt);
                context.SaveChanges();
            }
        }
        public static void update(StoreData store)
        {
            lock (Lock)
            {
                context.stores.Update(store);
                context.SaveChanges();
            }
        }

    }

    internal class DataAccessGet : DataAccessUpdate
    {
        public static BasketInCart getBasket(string userName, string storeName)
        {
            lock (Lock)
            {
                MemberData member = getMember(userName);
                StoreData store = getStore(storeName);
                return context.basketsInCarts.Find(new object[] { store, member });
            }
        }
        public static BasketInRecipt getBasket(int reciptID)
        {
            lock (Lock)
            {
                ReceiptData receipt = getReciept(reciptID);
                return receipt.basket;
            }
        }
        public static FeedbackData getFeedback(string userName, int productID)
        {
            lock (Lock)
            {
                MemberData member = getMember(userName);
                ProductInfoData product = getProductInfo(productID);
                return context.feedbacks.Find(new object[] { product, member });
            }
        }
        public static ICollection<FeedbackData> getAllFeedbacksOnProduct(int productID)
        {
            lock (Lock)
            {
                
                ProductInfoData product = getProductInfo(productID);
              
                return context.feedbacks.Where((FeedbackData x) => x.product.Equals(product)).ToList();
            }
        }

        public static ICollection<FeedbackData> getAllFeedbacksByUser(string userName)
        {
            lock (Lock)
            {

                MemberData member = getMember(userName);

                return context.feedbacks.Where((FeedbackData x) => x.user.Equals(member)).ToList();
            }
        }
        public static bool isFeedBackExist(string userName, int productID)
        {
            return false;
        }



        public static iPolicyData getPolicy(/*todo*/)
        {
            lock (Lock)
            {
                return null;
            }
        }
        public static iPolicyDiscountData getDiscountPolicy(/*todo*/)
        {
            lock (Lock)
            {
                return null;
            }
        }
        public static MemberData getMember(string userName)
        {
            lock (Lock)
            {
                return context.members.Find(new object[] { userName });
            }
        }
        public static ICollection<MemberData> getAllMembers()
        {
            lock (Lock)
            {
                return context.members.ToList();
            }
        }
        public static bool isMemberExist(string userName)
        {
            lock (Lock)
            {
                return context.members.Any((MemberData x) => x.userName.Equals(userName) );
            }
        }


        public static MessageData getMessage(Guid id)
        {
            lock (Lock)
            {
                return context.messages.Find(new object[] { id });
            }
        }
        public static ProductData getProduct(Guid id)
        {
            lock (Lock)
            {
                return context.products.Find(new object[] { id });
            }
        }
        public static ProductInfoData getProductInfo(int productID)
        {
            lock (Lock)
            {
                return context.productInfos.Find(new object[] { productID });
            }
        }
        public static ICollection<ProductInfoData> getAllProductsInfo()
        {
            lock (Lock)
            {
                return context.productInfos.ToList();
            }
        }
        public static bool isProductExist(int productID)
        {
            return false;
        }



        public static ReceiptData getReciept(int receiptID)
        {
            lock (Lock)
            {
                return context.recipts.Find(new object[] { receiptID });
            }
        }
        public static ICollection<ReceiptData> getAllUserRecipts(string userName)
        {

            MemberData member= getMember(userName);
            return member.receipts;
        }
        public static ICollection<ReceiptData> getAllStoreRecipts(string storeName)
        {

            StoreData store= getStore(storeName);
            return store.receipts;
        }



        public static StoreData getStore(string storeName)
        {
            lock (Lock)
            {
                return context.stores.Find(new object[] { storeName });
            }
        }

        public static ICollection<StoreData> getAllStore()
        {
            lock (Lock)
            {
                return context.stores.ToList();
            }
        }
        public static bool isStoreExist(string storeName)
        {
            lock (Lock)
            {
                return context.stores.Any((StoreData x) => x.storeName.Equals(storeName));
            }
        }

       public static addProductPermissionData getPremmisionAP(string ownername, string storeName )
        {
            lock(Lock){
                return context.addProductPermissions.Find(new object[] { ownername, storeName });
            }
        }
        public static editManagerPermissionsData getPremmisionEM(string ownername, string storeName)
        {
            lock (Lock)
            {
                return context.editManagerPermissions.Find(new object[] { ownername, storeName });
            }
        }
        public static editProductPermissionData getPremmisionEP(string ownername, string storeName)
        {
            lock (Lock)
            {
                return context.editProductPermissions.Find(new object[] { ownername, storeName });
            }
        }
        public static getInfoEmployeesPermissionData getPremmisionGIE(string ownername, string storeName)
        {
            lock (Lock)
            {
                return context.getInfoEmployeesPermissions.Find(new object[] { ownername, storeName });
            }
        }
        public static getPurchaseHistoryPermissionData getPremmisionGPH(string ownername, string storeName)
        {
            lock (Lock)
            {
                return context.getPurchaseHistoryPermissions.Find(new object[] { ownername, storeName });
            }
        }
        public static hireNewStoreManagerPermissionData getPremmisionHNM(string ownername, string storeName)
        {
            lock (Lock)
            {
                return context.hireNewStoreManagerPermissions.Find(new object[] { ownername, storeName });
            }
        }
        public static hireNewStoreOwnerPermissionData getPremmisionHNO(string ownername, string storeName)
        {
            lock (Lock)
            {
                return context.hireNewStoreOwnerPermissions.Find(new object[] { ownername, storeName });
            }
        }
        public static removeManagerPermissionData getPremmisionRM(string ownername, string storeName)
        {
            lock (Lock)
            {
                return context.removeManagerPermissions.Find(new object[] { ownername, storeName });
            }
        }
        public static removeOwnerPermissionData getPremmisionRO(string ownername, string storeName)
        {
            lock (Lock)
            {
                return context.removeOwnerPermissions.Find(new object[] { ownername, storeName });
            }
        }
        public static removeProductPermissionData getPremmisionRP(string ownername, string storeName)
        {
            lock (Lock)
            {
                return context.removeProductPermissions.Find(new object[] { ownername, storeName });
            }
        }
    }

     class DataAccess : DataAccessGet
    {

    }


}

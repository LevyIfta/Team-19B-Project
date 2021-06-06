using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.User.Permmisions;
using TradingSystem.BuissnessLayer.commerce;


namespace TradingSystem.BuissnessLayer
{
    public class Guest : aUser
    {
        public override string getUserName()
        {
            return "guest";
        }
        public override object todo(PersmissionsTypes func, object[] args)
        {
            return null; 
        }
        public override bool EstablishStore(string storeName)
        {
            return false;
        }

        public override string[] purchase(string creditNumber, string validity, string cvv)
        {
            ICollection<Receipt> list = new List<Receipt>();
            foreach (ShoppingBasket basket in getMyCart().baskets)
            {
                string[] ans = basket.store.executePurchase(basket, creditNumber, validity, cvv);
                if (ans == null || ans[0].Equals("false"))
                    return null;
                Receipt receipt = GetReceiptNow(ans[1]);
                list.Add(receipt);
            }
            string[] arr = new string[list.Count + 1];
            arr[0] = "true";
            int i = 1;
            foreach (Receipt receipt in list)
            {
                arr[i] = convertReceipt(receipt);
                i++;
            }
            return arr;
        }
        private string convertReceipt(Receipt receipt)
        {
            string ans = "";
            foreach (int id in receipt.products.Keys)
            {
                ans += id + "<" + receipt.products[id] + "=";
            }
            if (ans.Length > 0)
                ans = ans.Substring(0, ans.Length - 1);
            return receipt.username + "$" + receipt.store.name + "$" + receipt.price + "$" + receipt.date.ToString("dddd, dd MMMM yyyy HH:mm:ss") + "$" + receipt.receiptId + "$" + ans;
        }//receipt -> user$store$price$date$id$products. products -> pro1&pro2&pro3 -> proInfo^feedback -> feedback_feedback -> user#comment
        private Receipt GetReceiptNow(string id)
        {
            foreach (Receipt receipt in reciepts)
            {
                string check = "" + receipt.receiptId;
                if (check.Equals(id))
                    return receipt;
            }
            return null;
        }



    }
}

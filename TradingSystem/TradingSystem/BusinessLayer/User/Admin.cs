using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
    internal class Admin : Member
    {

        public LinkedList<string> getAllReceipts(){
            return PurchaseReceipts.Instance.getAllReceipts();
        }
        public object browseStore(string name)
        {
            throw new NotImplementedException();
        }

        public override bool EstablishStore(string storeName)
        {
            throw new NotImplementedException();
        }
        
        public override bool register(string username, string password)
        {
            throw new NotImplementedException();
        }

        public override ShoppingBasket checkShoppingBasketDetails(string storeName)
        {
            throw new NotImplementedException();
        }

        public override bool purchase(string storeName)
        {
            throw new NotImplementedException();
        }
    }
}

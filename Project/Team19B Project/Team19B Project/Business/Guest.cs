using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
    class Guest : User
    {
        public Guest()
        {
            //shopping basket
        }


        private void addProductsToBasket(List<object> products)
        {

        }
        private void removeProductsToBasket(List<object> products)
        {

        }
        public object getStoreAndProductsInfo(object shop)
        {
            throw new NotImplementedException();
        }
        public object checkShoppingBusketDetails()
        {
            return null;
        }
        override
        public void register(string name, string pass)
        {
            DataClass.addRegistered(name, pass);
        }
        public bool purchase(object store)
        {
            return false;
        }
        
        public object browseStore(string name)
        {
            throw new NotImplementedException();
        }
    }
}

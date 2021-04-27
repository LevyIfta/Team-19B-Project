using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.ServiceLayer
{
    public class SLbasket
    {
        public ICollection<SLproduct> products { get; }
        public string storeName { get; }
        public string userName { get; }

        public const int PARAMETER_COUNT = 3;

        public SLbasket(ICollection<SLproduct> products, string storeName, string userName)
        {
            this.products = products;
            this.storeName = storeName;
            this.userName = userName;
        }

        public SLbasket(Object[] parameters)
        {
            this.products = (ICollection<SLproduct>)parameters[0];
            this.storeName = (string)parameters[1];
            this.userName = (string)parameters[2];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.ServiceLayer
{
    public class SLreceipt
    {
        private static readonly int BEL = 7;
        public ICollection<SLproduct> products { get; }
        public string storeName { get; }
        public string userName { get; }
        public double price { get; }
        public DateTime date { get; }
        public int receiptID { get; }

        public const int PARAMETER_COUNT = 6;

        public SLreceipt(ICollection<SLproduct> products, string storeName, string userName, double price, DateTime date, int receiptID)
        {
            this.products = products;
            this.storeName = storeName;
            this.userName = userName;
            this.price = price;
            this.date = date;
            this.receiptID = receiptID;
        }

        public SLreceipt(Object[] parameters)
        {
            this.products = (ICollection<SLproduct>)parameters[0];
            this.storeName = (string)parameters[1];
            this.userName = (string)parameters[2];
            this.price = (double)parameters[3];
            this.date = (DateTime)parameters[4];
            this.receiptID = (int)parameters[5];
        }


        public List<string> toStringList()
        {
            throw new NotImplementedException();
        }

        public static  DecodedMessge toMessage(ICollection<SLreceipt> recipts)
        {
            DecodedMessge ans = new DecodedMessge();
            ans.type = msgType.OBJ;
            ans.name = "recipts";
            throw new NotImplementedException();
        }


    }
}

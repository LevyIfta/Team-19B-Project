using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team19B_Project.Business
{
    public class CreditCardInfo
    {
        public string creditCardNumber { get; }
        public int cvv { get; }
        public string holderName { get; }
        public string experationDate { get; }

        public CreditCardInfo(string creditCardNumber, int cvv, string holderName, string experationDate)
        {
            this.creditCardNumber = creditCardNumber;
            this.cvv = cvv;
            this.experationDate = experationDate;
            this.holderName = holderName;
        }
    }
}

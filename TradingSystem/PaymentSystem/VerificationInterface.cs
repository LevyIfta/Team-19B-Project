using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem
{
    public interface VerificationInterface
    {
        bool Pay(string userName, string creditNumber, string validity, string cvv);
        void setReal(VerificationInterface real);
    }
}

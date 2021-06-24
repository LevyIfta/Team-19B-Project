using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem
{
    public static class VerificationSystem
    {
        public static VerificationInterface paymentSystem = new VerificationProxy();
        public static void setReal(string url)
        {
            VerificationInterface real = new VerificationReal(url);
            paymentSystem.setReal(real);
        }

    }
}

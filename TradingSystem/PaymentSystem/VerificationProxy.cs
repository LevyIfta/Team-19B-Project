using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace PaymentSystem
{
    public class VerificationProxy : VerificationInterface
    {
        public VerificationInterface real { get; set; }

        public bool Pay(string userName, string creditNumber, string validity, string cvv)
        {
            if (creditNumber == null || userName == null || validity == null || cvv == null)
                return false;
            if (creditNumber.Length == 0 || userName.Length == 0 || validity.Length == 0 || cvv.Length == 0)
                return false;
            if (containLatter(creditNumber) || containLatter(validity) || containLatter(cvv))
                return false;
            if (11 > creditNumber.Length || creditNumber.Length > 19 || cvv.Length != 3)
                return false;
            string[] date = validity.Split('/');
            if (date.Length != 2)
                return false;
            if (date[0].Length > 3 || date[0].Length == 0 || date[1].Length < 2 || date[1].Length > 4)
                return false;

            // check for real
            if (this.real == null)
                return true;

            bool success = real.Pay(userName, creditNumber, validity, cvv);
            return success;
        }

        private static bool containLatter(string str)
        {
            foreach (char letter in str)
            {
                if (97 <= (int)letter && (int)letter <= 122)
                    return true;
            }
            return false;
        }

        public void setReal(VerificationInterface real)
        {
            this.real = real;
        }
    }
}

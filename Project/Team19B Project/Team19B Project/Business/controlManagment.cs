using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
    class controlManagment
    {
        public void createRegInstance()
        {
           
        }
        private bool checkValidity(string password)
        {
            if (password.Length < 8)
                return false;

            return true;
        }
    }
}

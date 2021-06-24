using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplySystem
{
    public interface SupplyInterface
    {
        bool OrderPackage(string storeName, string userName, string address, string orderInfo);
        void InformStore(string storeName);
        bool RemoveStore(string storeName);
        void setReal(SupplyInterface real);

    }
}

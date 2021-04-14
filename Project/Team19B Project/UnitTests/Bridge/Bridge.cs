using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Bridge
{
    interface Bridge
    {
        bool login(string username, string password);

        bool register(string username, string password);

        bool promote(Object todo);

        Object searchItem(Object todo);

        Object retriveBasket(Object todo);

        Object getReciept(Object todo);

        Object buyBasket(Object todo);




    }
}

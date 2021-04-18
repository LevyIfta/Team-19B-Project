using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
    class interfaceClass
    {
        private static User user = new Guest();
        public static void loadMenu()
        {
            string username = "";
            string password;

            while (true)
            {
                printMenu();
                string option = Console.ReadLine();
                
                switch (option)
                {
                    case "1"://login
                        Console.WriteLine("Login- please enter username\n");
                        username = Console.ReadLine();
                        Console.WriteLine("Login- please enter your password, " + username + '\n');
                        password = Console.ReadLine();
                        Member reg = User.login(username, password);
                        if (reg == null)
                            Console.WriteLine("invalid username/password");
                        else
                            user = reg;
                        break;

                    case "2"://register
                        Console.WriteLine("Register- please enter username\n");
                        username = Console.ReadLine();
                        Console.WriteLine("Register- please enter your password, " + username + '\n');
                        password = Console.ReadLine();
                        if (!checkUserValidity(username) || !checkPasswordValidity(password))
                            Console.WriteLine("can't register with this details");
                        else
                            user.register(username, password);
                        break;
                    case "3"://logout
                        user = new Guest();
                        break;
                }
            }
        }
        public static void printMenu()
        {
            Console.WriteLine("");











            Console.WriteLine("Login- please enter username\n");
            string username = Console.ReadLine();
            Console.WriteLine("Login- please enter your password, " + username + '\n');
            string password = Console.ReadLine();
            Registered registered = new Registered();
            registered.login(username, password);
        }

        public static bool checkUserValidity(string username)
        {
            return DataClass.isUserExists(username);
        }
        public static bool checkPasswordValidity(string password)
        {
            int countNum = 0;
            int countCap = 0;
            int countLet = 0;
            if (password.Length < 4 || password.Length > 20)
                return false;
            foreach (char value in password)
            {
                if (Char.IsDigit(value))
                    countNum++;
                if (97 <= (int)value && (int)value <= 122)
                    countLet++;
                if (60 <= (int)value && (int)value <= 90)
                    countCap++;
            }
            return (countNum > 0 & countLet > 0 & countCap > 0);
        }
    }
}

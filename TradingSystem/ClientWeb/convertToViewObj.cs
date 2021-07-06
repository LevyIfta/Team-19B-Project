using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWeb
{
    class convertToViewObj
    {
        /*
        public static string[] Reciept(string[] arr)
        {
            string[,] ans = new string[arr.Length, 4];
            for (int i = 0; i < arr.Length; i++)
            {
                string[] temp = arr[i].Split('$');
                ans[i, 1] = temp[2];
            }
        }
        */

        public static string[,] Cart(string[] arr)
        {// {basket, basket}. basket -> username&storename&pros. pros -> -> pro$pro -> proInfo^feedback -> feedback_feedback -> user#comment
            // bamba^10.3^manu1^food^10^almog#what i think_gal#what he think
            //string[][] ans = null;
            //int h = 0;  almog&nana&bamb^15^mmm^food^2^
            int count = 0;
            for (int h = 0; h < arr.Length; h++)
            {
                string[] basket = arr[h].Split('&');
                string[] pros = basket[2].Split('$');
                count += pros.Length;
            }
            string[,] ans = new string[count, 5];
            int f = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                string[] basket = arr[i].Split('&');
                string[] pros = basket[2].Split('$');
                for (int j = 0; j < pros.Length; j++)
                {
                    string[] proInfo = pros[j].Split('^');
                    ans[f, 0] = proInfo[0];
                    ans[f, 1] = proInfo[1];
                    ans[f, 2] = proInfo[4];
                    ans[f, 3] = basket[1];
                    ans[f, 4] = proInfo[2];
                    f++;
                }
            }
            return ans;
        }
        public static string[,] Product(string[] arr)
        {
            string[,] ans = new string[arr.Length, 4];
            for (int i = 0; i < arr.Length; i++)
            {
                string[] temp = arr[i].Split('$');
                ans[i, 3] = temp[0];
                string[] products = temp[1].Split('^');
                ans[i, 0] = products[0];
                ans[i, 1] = products[1];
                ans[i, 4] = products[2];
            }//[name ,price , amount ,storename]
            return ans;
        }
        public static string[] Feedback(string[] arr, string productName)
        {
            string[] feedback = null;
            for (int i = 0; i < arr.Length; i++)
            {
                string[] temp = arr[i].Split('$');
                string[] products = temp[1].Split('^');
                if (products[0].Equals(productName))
                {
                    feedback = products[5].Split('_');
                }
            }//[user#feeback , user#feedback ...]
            return feedback;
        }

        public static string PermmisionsInsert(string perm)
        {//1 - AddProduct\n2 - EditManagerPermissions\n3 - EditProduct\n4 - GetInfoEmployees\n5 - GetPurchaseHistory\n
         ////6 - HireNewStoreManager\n7 - HireNewStoreOwner\n8 - RemoveManager\n9 - RemoveProduct\n10 - RemoveOwner
            string[] permission = perm.Split('#');
            string ans = "";
            for (int i=0; i<permission.Length; i++)
            {
                ans += convertPerm(permission[i]) + "$";
            }
            if (ans.Length > 0)
                ans = ans.Substring(0, ans.Length - 1);
            return ans;
        }
        private static string convertPerm(string num)
        {
            switch (num)
            {
                case "1":
                    return "AddProduct";
                case "2":
                    return "EditManagerPermissions";
                case "3":
                    return "EditProduct";
                case "4":
                    return "GetInfoEmployees";
                case "5":
                    return "GetPurchaseHistory";
                case "6":
                    return "HireNewStoreManager";
                case "7":
                    return "HireNewStoreOwner";
                case "8":
                    return "RemoveManager";
                case "9":
                    return "RemoveProduct";
                case "10":
                    return "RemoveOwner";
            }
            return "";
        }
    }
}

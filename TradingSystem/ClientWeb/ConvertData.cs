using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWeb
{
    public static class ConvertData
    {
        public static string[] Reciept(string[] arr)
        {
            string[,] ans = new string[arr.Length, 4];
            for (int i = 0; i < arr.Length; i++)
            {
                string[] temp = arr[i].Split('$');
                ans[i, 1] = temp[2];
            }
            return null;
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

    }
}

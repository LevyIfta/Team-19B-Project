using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientProject.Connection
{
    class Encoder
    {
        public static readonly int EOT = 4;
        public static readonly int ETB = 27;
        public static byte[] encode(DecodedMessge messge)
        {
            string ans = "";
            switch(messge.type)
            {
                case (msgType.FUNC):
                    ans += "fuction";
                    break;
                case (msgType.OBJ):
                    ans += "object";
                    break;
            }
            ans += (char)ETB;
            ans += messge.name;
            ans += (char)ETB;
            foreach (string param in messge.param_list)
            {
                ans += param;
                ans += (char)ETB;
            }
            ans += (char)EOT;

            return Encoding.ASCII.GetBytes(ans);

        }
    }
}

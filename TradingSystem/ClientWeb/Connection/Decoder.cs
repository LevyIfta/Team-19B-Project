using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWeb.Connection
{
    class Decoder
    {
        public static readonly int EOT = 4;
        public static readonly int ETB = 27;

        public static DecodedMessge decode(byte[] msg)
        {
            DecodedMessge ans = new DecodedMessge();

            string currentPart = "";
            List<string> brokenMsg = new List<string>();
            foreach (byte b in msg)
            {
                if(Convert.ToChar(b) ==  ETB  )
                {
                    brokenMsg.Add(currentPart);
                    currentPart = "";
                }
                else
                {
                    currentPart += Convert.ToChar(b);
                }
            }
            switch(brokenMsg[0])
            {
                case ("fuction"):
                    ans.type = msgType.FUNC;
                    break;
                case ("object"):
                    ans.type = msgType.OBJ;
                    break;
                case ("alarm"):
                    ans.type = msgType.ALARM;
                    break;
            }
            ans.name = brokenMsg[1];
            ans.param_list = brokenMsg.GetRange(2, brokenMsg.Count-2).ToArray();
            return ans;


        }
    }
}

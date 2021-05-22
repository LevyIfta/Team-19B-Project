using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Trial2.PresentationLayer.Windows
{
    static class WindowManager
    {
        public static string username = "guest";
        public static List<String> actions;

        public static List<String> getActions()
        {
            if (actions == null)
                actions = new List<string>();

            if (username.Equals("guest"))
            {
                actions.Add("register");
                actions.Add("login");
            }
            else
            {
                actions.Add("open store");
                actions.Add("logout");
            }

            return actions;
        }

        
        
    }
}

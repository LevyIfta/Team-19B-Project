using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientProject.Connection;

namespace ClientProject
{
    class Controller
    {
        private static Controller instance = null;


        private Controller()
        {

        }

        public static Controller GetController()
        {
            if (instance == null)
                instance = new Controller();
            return instance;
        }

        public void disconnect()
        {
            ConnectionManager.disconnect();
        }


        public string test()
        {
            DecodedMessge msg = new DecodedMessge();    
            msg.type = msgType.FUNC;
            msg.name = "register";
            msg.param_list = new string[] { "username", "Password1"};

            byte[] enc = Connection.Encoder.encode(msg);

            Connection.ConnectionManager.sendMessage(enc);
            return "blup";
        }

    }
}

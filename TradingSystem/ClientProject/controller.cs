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
            msg.type = msgType.OBJ;
            msg.name = "basket";
            msg.param_list = new string[] { "item1", "item2", "item3", "item4"};

            byte[] enc = Connection.Encoder.encode(msg);

            Connection.ConnectionManager.sendMessage(enc);
            return "blup";
        }

    }
}

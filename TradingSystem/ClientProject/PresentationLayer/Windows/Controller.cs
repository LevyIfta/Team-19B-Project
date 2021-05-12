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
        public bool Login(string username, string password)
        {
            DecodedMessge msg = new DecodedMessge();
            msg.type = msgType.FUNC;
            msg.name = "login";
            msg.param_list = new string[] { username, password };
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);
            byte[] ans_e = Connection.ConnectionManager.readMessage();
            DecodedMessge ans_d = Connection.Decoder.decode(ans_e);
            bool ans = false;
            if (ans_d.type == msgType.OBJ && ans_d.name == "bool")
            {
                ans = ans_d.param_list[0] == "true";
            }
            return ans;
        }
        public bool Logout()
        {
            DecodedMessge msg = new DecodedMessge();
            msg.type = msgType.FUNC;
            msg.name = "login";
            msg.param_list = new string[] { };
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);
            byte[] ans_e = Connection.ConnectionManager.readMessage();
            DecodedMessge ans_d = Connection.Decoder.decode(ans_e);
            bool ans = false;
            if (ans_d.type == msgType.OBJ && ans_d.name == "bool")
            {
                ans = ans_d.param_list[0] == "true";
            }
            return ans;
        }
        public string getUserName()
        {
            DecodedMessge msg = new DecodedMessge();
            msg.type = msgType.FUNC;
            msg.name = "login";
            msg.param_list = new string[] { };
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);
            byte[] ans_e = Connection.ConnectionManager.readMessage();
            DecodedMessge ans_d = Connection.Decoder.decode(ans_e);
            string ans = "guest";
            if (ans_d.type == msgType.OBJ && ans_d.name == "string")
            {
                ans = ans_d.param_list[0];
            }
            return ans;
        }

        public bool Register(string username, string password)
        {
            DecodedMessge msg = new DecodedMessge();
            msg.type = msgType.FUNC;
            msg.name = "register";
            msg.param_list = new string[] { username, password };
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);
            byte[] ans_e = Connection.ConnectionManager.readMessage();
            DecodedMessge ans_d = Connection.Decoder.decode(ans_e);
            bool ans = false;
            if (ans_d.type == msgType.OBJ && ans_d.name == "bool")
            {
                ans = ans_d.param_list[0] == "true";
            }
            return ans;
        }

        public bool OpenStore(string username, string storename)
        {
            DecodedMessge msg = new DecodedMessge();
            msg.type = msgType.FUNC;
            msg.name = "open store";
            msg.param_list = new string[] { username, storename };
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);
            byte[] ans_e = Connection.ConnectionManager.readMessage();
            DecodedMessge ans_d = Connection.Decoder.decode(ans_e);
            bool ans = false;
            if(ans_d.type == msgType.OBJ && ans_d.name == "bool")
            {
                ans = ans_d.param_list[0] == "true";
            }
            return ans;
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

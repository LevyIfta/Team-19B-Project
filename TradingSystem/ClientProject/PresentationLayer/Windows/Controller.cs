using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientProject.Connection;
using System.Windows;
using WPF_Trial2.PresentationLayer.Windows;
using System.Threading;

namespace ClientProject
{
    class Controller
    {
        private static Controller instance = null;


        private Controller()
        {
            Thread alarmthread = new Thread(new ThreadStart( Alarmanager.start ));
            alarmthread.Start();
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


        private DecodedMessge readMessage()
        {
           
            return Alarmanager.getMsg();
        }


        public bool Login(string username, string password)
        {
            DecodedMessge msg = new DecodedMessge();
            msg.type = msgType.FUNC;
            msg.name = "login";
            msg.param_list = new string[] { username, password };
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);
          
            DecodedMessge ans_d = readMessage();
            bool ans = false;
            if (ans_d.type == msgType.OBJ && ans_d.name == "bool")
            {
                ans = ans_d.param_list[0] == "true";
            }
            return ans;
        }
        public bool Logoutfunc()
        {
            DecodedMessge msg = new DecodedMessge();
            msg.type = msgType.FUNC;
            msg.name = "logout";
            msg.param_list = new string[] { };
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);
    
            DecodedMessge ans_d = readMessage();
            bool ans = false;
            if (ans_d.type == msgType.OBJ && ans_d.name == "bool")
            {
                ans = ans_d.param_list[0] == "true";
            }
            return ans;
        }
        public string getUserName()
        {
            Connection.ConnectionManager.Connect();
            DecodedMessge msg = new DecodedMessge();
            msg.type = msgType.FUNC;
            msg.name = "get online user name";
            msg.param_list = new string[] { };
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
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

            DecodedMessge ans_d = readMessage();
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
       
            DecodedMessge ans_d = readMessage();
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
            msg.type = msgType.FUNC;
            msg.name = "fail";
            msg.param_list = new string[] { "zzz"};

            byte[] enc = Connection.Encoder.encode(msg);

            Connection.ConnectionManager.sendMessage(enc);
 
            DecodedMessge ans_d = readMessage();
            return "blup";
        }



    }
}

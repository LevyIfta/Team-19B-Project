using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientProject.Connection;
using System.Windows;
using WPF_Trial2.PresentationLayer.Windows;

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

        private void handleAlarm(DecodedMessge alarm)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => {
                Window mainwindow = Application.Current.MainWindow;
                if (mainwindow is MainWindow)
                    ((MainWindow)mainwindow).getAlarm(alarm.name, alarm.param_list[0]);
                if (mainwindow is LoginWindow)
                    ((LoginWindow)mainwindow).getAlarm(alarm.name, alarm.param_list[0]);
                if (mainwindow is RegistrationWindow)
                    ((RegistrationWindow)mainwindow).getAlarm(alarm.name, alarm.param_list[0]);
            }));
        }

        private DecodedMessge readMessage()
        {
            byte[] ans_e = Connection.ConnectionManager.readMessageCon();
            DecodedMessge ans = Connection.Decoder.decode(ans_e);
            while(ans.type == msgType.ALARM) //check if msg is alarm
            {
                handleAlarm(ans);
                ans_e = Connection.ConnectionManager.readMessageCon();
                 ans = Connection.Decoder.decode(ans_e);
            }
            return ans;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_Trial2.PresentationLayer.DataContext;
using ClientProject;

namespace WPF_Trial2.PresentationLayer.Windows
{
    class UserDataR : ANotifyPropChange
    {
        private String Username;

        public String username
        {
            get { return Username; }
            set
            {
                Username = value;
                OnPropertyChanged();
            }
        }
        private String Password;

        public String password
        {
            get { return Password; }
            set
            {
                Password = value;
                OnPropertyChanged();
            }
        }
        private String Usermsg;

        public String usermsg
        {
            get { return Usermsg; }
            set
            {
                Usermsg = value;
                OnPropertyChanged();
            }
        }
        private String Passwordmsg;

        public String passwordmsg
        {
            get { return Passwordmsg; }
            set
            {
                Passwordmsg = value;
                OnPropertyChanged();
            }
        }
        private String Msgvalid;

        public String msgvalid
        {
            get { return Msgvalid; }
            set
            {
                Msgvalid = value;
                OnPropertyChanged();
            }
        }
        /*
        private String Passwordvalid;

        public String passwordvalid
        {
            get { return Passwordvalid; }
            set
            {
                Passwordvalid = value;
                OnPropertyChanged();
            }
        }
        */
        private String PasswordC;

        public String passwordC
        {
            get { return PasswordC; }
            set
            {
                PasswordC = value;
                OnPropertyChanged();
            }
        }

    }
    public partial class RegistrationWindow : Window
    {
        UserDataR user = new UserDataR();
        List<string> errors;

        string msgFailed = "";
        static Controller controler = Controller.GetController();
        public RegistrationWindow()
        {
            InitializeComponent();
            this.DataContext = user;
        }
        private void CleanButton()
        {
            errors = new List<string>();
            user.usermsg = "";
            user.msgvalid = "";
            user.passwordmsg = "";
            //user.passwordvalid = "";
        }
        private void ButtomCheck() // textbox valid check 
        {
            String pass = "c";
            String passC = "t";
            if (user.username == null)
                errors.Add("User name empty");
            if (user.password == null)
                errors.Add("Password empty");
            else
                pass = user.password;
            if (user.passwordC == null)
                errors.Add("PasswordC empty");
            else
                passC = user.passwordC;
            if (!(pass.Equals(passC)) & (pass != "c") & (passC != "t"))
                errors.Add("Password isn't confirm");
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CleanButton();
            bool insertValid = true;
            if (errors.Count > 0)
            {
                insertValid = false;
                if (errors.Contains("User name empty"))
                    user.usermsg = "User name empty";
                if (errors.Contains("Password empty"))
                    user.passwordmsg = "Password empty";
                if (errors.Contains("PasswordC empty"))
                    user.passwordmsg = "Password confirm empty";
                if (errors.Contains("Password isn't confirm"))
                    user.passwordmsg = "Passwords must match";
            }
            if (insertValid)
            {
                bool ans = controler.Register(user.username, user.password);
                if (!ans)
                {
                    user.msgvalid = "Registretion Faild";
                    user.usermsg = "";
                    user.password = "";
                    string[] check1 = msgFailed.Split(' ');
                    if (check1[0].Equals("password"))
                        user.passwordmsg = msgFailed;
                    else
                        user.usermsg = msgFailed;
                }
                else
                {
                    user.msgvalid = "Registretion Succeeded";
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoginWindow window = new LoginWindow();
            window.Show();
            App.Current.MainWindow = window;
            this.Close();
        }
        public void getAlarm(string title, string des)
        {
            if (title.Equals("register"))
            {
                msgFailed = des;
            }
        }
    }
}

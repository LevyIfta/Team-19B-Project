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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Trial2.PresentationLayer.DataContext;
using ClientProject;

namespace WPF_Trial2.PresentationLayer.Windows
{

    class UserData : ANotifyPropChange
    {
        private String Usernamer;

        public String username
        {
            get { return Usernamer; }
            set
            {
                Usernamer = value;
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


        
    }

    public partial class LoginWindow
    {
        //private UserDataContext userDataContext;
        UserData user = new UserData();
        List<string> errors;

        string msgFailed = "";
        static Controller controler = Controller.GetController();

        
        public LoginWindow()
        {
            InitializeComponent();
            //this.userDataContext = new UserDataContext();
            this.DataContext = user;
        }
        private void CleanButton()
        {
            errors = new List<string>();
            user.usermsg = "";
            user.msgvalid = "";
            user.passwordmsg = "";
            user.passwordvalid = "";
        }
        private void ButtomCheck() // textbox valid check 
        {
            if (user.username == null)
                errors.Add("User name empty");
            if (user.password == null)
                errors.Add("Password empty");
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            CleanButton();
            bool insertValid = true;
            if(errors.Count > 0)
            {
                insertValid = false;
                if (errors.Contains("User name empty"))
                    user.usermsg = "User name empty";
                if (errors.Contains("Password empty"))
                    user.passwordmsg = "Password empty";
            }
            if(insertValid)
            {
                bool ans = controler.Login(user.username, user.password);
                //controler.getUserName();
                if (!ans)
                {
                    user.msgvalid = "Login Faild";
                    user.usermsg = "";
                    user.password = "";
                    string[] check1 = msgFailed.Split(' ');
                    if(check1[0].Equals("password"))
                        user.passwordmsg = msgFailed;
                    else
                        user.usermsg = msgFailed;
                }
                else
                {
                    //string username = controler.getUserName();
                    WindowManager.username = user.username;
                    MainWindow window1 = new MainWindow();
                    window1.Show();
                    App.Current.MainWindow = window1;
                    window1.username = user.username;
                    //((MainWindow)Application.Current.MainWindow).username = user.username;
                    this.Close();
                }
            }
            
            
            //checked for errors
            //call applyLogin(Businesslayer.user user)
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            //bool ans = controler.Register(user.username, user.password);
            RegistrationWindow window = new RegistrationWindow();
            window.Show();
            App.Current.MainWindow = window;
            this.Close();
        }

        //gets interface layer user object
        private void applyLogin()
        {
            //login the user through interface layer
            UserDataContext udc = new UserDataContext(); //gets as param the business layer user object and converts it to userdatacontext
            ((MainWindow)Application.Current.MainWindow).setLoggedInUser(udc);
            this.Close();
        }

        public void getAlarm(string title, string des)
        {
            if(title.Equals("login failed"))
            {
                msgFailed = des;
            }
        }


    }
}

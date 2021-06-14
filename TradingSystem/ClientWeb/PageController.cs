using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClientWeb
{
    abstract class ANotifyPropChange : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));  
        }
    }
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
        private String Username;

        public String loginname
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
    }
    class UserR : ANotifyPropChange
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
        private String Age;

        public String age
        {
            get { return Age; }
            set
            {
                Age = value;
                OnPropertyChanged();
            }
        }
        private String Gender;

        public String gender
        {
            get { return Gender; }
            set
            {
                Gender = value;
                OnPropertyChanged();
            }
        }
        private String Address;

        public String address
        {
            get { return Address; }
            set
            {
                Address = value;
                OnPropertyChanged();
            }
        }
    }

    public class PageController
    {
        public static string username = "guest";

    }
}

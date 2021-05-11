using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Trial2.PresentationLayer.DataContext
{
    public class UserDataContext : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged(name);
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged(password);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public UserDataContext()
        {
            this.name = "guest";
        }

        protected void OnPropertyChanged(string text)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(text));
            }
        }
    }
}

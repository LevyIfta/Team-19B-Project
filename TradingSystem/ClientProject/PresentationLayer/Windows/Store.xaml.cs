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
    /// <summary>
    /// Interaction logic for Store.xaml
    /// </summary>
    public partial class Store : Window
    {
        static Controller controler = Controller.GetController();
        UserDataName user = new UserDataName();
        public Store()
        {
            InitializeComponent();
            this.DataContext = user;
            user.storemsg = "start";
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            string username = WindowManager.username;
            if (user.storename == null)
            {
                user.storemsg = "insert store name";
            }
            else
            {
                if (username.Equals("guest"))
                {
                    user.storename = "";
                    user.storemsg = "login first";
                }
                else
                {
                    bool ans = controler.OpenStore(username, user.storename);
                    if (!ans)
                    {
                        user.storename = "";
                        user.storemsg = "the name is already taken";
                    }
                    else
                    {
                        user.storename = "";
                        user.storemsg = "the store is open!";
                    }
                }
            }
        }
    }
}

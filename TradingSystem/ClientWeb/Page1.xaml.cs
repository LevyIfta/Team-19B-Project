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

namespace ClientWeb
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        UserData userInfo = new UserData();
        string username;
        static Controller controler = Controller.GetController();
        public Page1()
        {
            InitializeComponent();
            this.DataContext = userInfo;
            string username = PageController.username;
            userInfo.username = "hello, " + username;
            //Controller.GetController().test();
        }
        private void checkMember()
        {
            if (username.Equals("admin"))
            {
                register.Visibility = Visibility.Collapsed;
                login.Visibility = Visibility.Collapsed;
            }
            else if (username.Equals("guest"))
            {
                logout.Visibility = Visibility.Collapsed;
                openStore.Visibility = Visibility.Collapsed;
                myCart.Visibility = Visibility.Collapsed;
                Receipts.Visibility = Visibility.Collapsed;
                user.Visibility = Visibility.Collapsed;
                stores.Visibility = Visibility.Collapsed;
            }
            else if (username.Length > 0)
            {
                register.Visibility = Visibility.Collapsed;
                login.Visibility = Visibility.Collapsed;
            }
        }

        private void openStore_Click(object sender, RoutedEventArgs e)
        {
            openStore o = new openStore();
            NavigationService.Navigate(o);
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            register r = new register();
            NavigationService.Navigate(r);
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            login l = new login();
            NavigationService.Navigate(l);
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            bool ans = controler.Logoutfunc();
            if (ans)
            {
                PageController.username = "guest";
                checkMember();
            }
            else
            {

            }
        }

        private void myCart_Click(object sender, RoutedEventArgs e)
        {
            myCart m = new myCart();
            NavigationService.Navigate(m);
        }

        private void Receipts_Click(object sender, RoutedEventArgs e)
        {
            myReceipts m = new myReceipts();
            NavigationService.Navigate(m);
        }

        private void stores_Click(object sender, RoutedEventArgs e)
        {

            myStores m = new myStores();
            NavigationService.Navigate(m);

        }
    }
}

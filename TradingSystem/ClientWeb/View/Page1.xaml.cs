using ClientWeb.Objects;
using ClientWeb.Windows;
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


        static Controller controler = Controller.GetController();

        public Page1()
        {
            // mockData data = new mockData();

            InitializeComponent();
            this.DataContext = userInfo;
            userInfo.username = "almog";
            checkMember();

            string a = controler.SearchStore("Castro");



            //Controller.GetController().test();

            //test area

            // bamba ^ 10.3 ^ manu1 ^ food ^ 10 ^ almog#what i think_gal#what he think

            /*
            controler.Register("aaaa", "Abc123456");
            string[] a = controler.Login("aaaa", "Abc123456");
            MessageBox.Show(a[0]);
            string[] products = new string[] { "bamba", "besli", "ok" };

            this.dataGrid.ItemsSource = products;
              

            List<Object> mylist = new List<Object>(){
            new {FirstName="myfirstName1", LastName="mylastName1", Phone="+123-123-123"},
            new {FirstName="myfirstName2", LastName="mylastName2", Phone="+124-124-124"},
            new {FirstName="myfirstName3", LastName="mylastName3", Phone="+125-125-125"}
                };

            this.dataGrid.ItemsSource = mylist;
          */

        }




        private void checkMember()
        {

            if (userInfo.username.Equals("admin"))
            {
                register.Visibility = Visibility.Collapsed;
                login.Visibility = Visibility.Collapsed;
            }
            else if (userInfo.username.Equals("guest"))
            {
                logout.Visibility = Visibility.Collapsed;
                openStore.Visibility = Visibility.Collapsed;
                myCart.Visibility = Visibility.Collapsed;
                Receipts.Visibility = Visibility.Collapsed;
                user.Visibility = Visibility.Collapsed;
                stores.Visibility = Visibility.Collapsed;
            }
            else if (userInfo.username.Length > 0)
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
                userInfo.username = "guest";
                checkMember();
            }
            else
            {

            }
        }

        private void myCart_Click(object sender, RoutedEventArgs e)
        {
            myCart m = new myCart(userInfo.username);
            NavigationService.Navigate(m);
        }

        private void Receipts_Click(object sender, RoutedEventArgs e)
        {
            myReceipts m = new myReceipts();
            NavigationService.Navigate(m);
        }

        private void stores_Click(object sender, RoutedEventArgs e)
        {
            //   myStores m = new myStores(controler.GetMyStore(userInfo.username), userInfo.username);
            myStores m = new myStores(controler.GetMyStore(userInfo.username), userInfo.username);
            NavigationService.Navigate(m);
        }

        private void browse_Click(object sender, RoutedEventArgs e)
        {
            BrowseItems m = new BrowseItems();
            NavigationService.Navigate(m);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            //  string product = this.textBox1.Text;
            string product = "";
            string manufacturer = "";
            Dictionary<string, SLproduct> products = controler.BrowseProducts(userInfo.username, product, manufacturer);
            foreach (KeyValuePair<string, SLproduct> item in products)
            {

            }

        }

        private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Ensure row was clicked and not empty space
            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                                                e.OriginalSource as DependencyObject) as DataGridRow;

            if (row == null) return;

            row.Item.ToString();




        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

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
       // StoreData storeInfo = new StoreData();

        static Controller controler = Controller.GetController();

        List<productView> productToView = new List<productView>();

        public Page1(string username)
        {
            // mockData data = new mockData();

            InitializeComponent();

            if (userName.Equals(""))
            {
                userInfo.username = "guest";
            }
            else
            {
                userInfo.username = username;
            }
          
            this.DataContext = userInfo;
         
            checkMember();

            string a = controler.SearchStore("Castro");    
            
            productToView.Add(new productView() { name = "Pro1", price = "15", amount = "12", storeName = "Castro", amounttoAdd = "0" });
            productToView.Add(new productView() { name = "Pro2", price = "15", amount = "12", storeName = "Castro", amounttoAdd = "0" });
            productToView.Add(new productView() { name = "Pro3", price = "15", amount = "12", storeName = "Castro", amounttoAdd = "0" });



            dgProducts.ItemsSource = productToView;
           
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


        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            var filtered = productToView.Where(product => product.name.StartsWith(textBox21.Text));
            dgProducts.ItemsSource = null;
            dgProducts.ItemsSource = filtered;
            dgProducts.Items.Refresh();

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
            MessageBox.Show(userInfo.username);
            openStore o = new openStore(userInfo.username);
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
                FirstPage p = new FirstPage();
                NavigationService.Navigate(p);
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

        public void addToBasket(object sender, RoutedEventArgs e)
        {
            productView p = (productView)dgProducts.SelectedItem;

            //add item to cart ( basket)
          bool ans =  controler.SaveProduct(user.Name, p.storeName , "man", p.name + "&" +p.amounttoAdd);
            if (ans)
            {
                MessageBox.Show("added to cart");
                MessageBox.Show(ans.ToString());
            }

        }
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void textBox21_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filtered = productToView.Where(product => product.name.StartsWith(textBox21.Text));
            dgProducts.ItemsSource = null;
            dgProducts.ItemsSource = filtered;
            dgProducts.Items.Refresh();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //Change this
            Store page = new Store("Castro", "almog");
            NavigationService.Navigate(page);

        }
    }
}

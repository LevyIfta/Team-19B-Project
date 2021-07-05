using ClientWeb.Objects;
using ClientWeb.View.member;
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

        static Controller controler = Controller.GetController();

        List<productView> productToView = new List<productView>();
        string username = PageController.username;
        public Page1()
        {
            // mockData data = new mockData();

            InitializeComponent();
            username = PageController.username;
            userName.Content = PageController.username;

            checkMember();

            //string a = controler.SearchStore("Castro");

            var productArr = controler.GetAllProducts();
            for (int i = 0; i < productArr.Length; i++)
            {
                string[] pro = productArr[i].Split('&');
                string[] stores = pro[3].Split('$');
                string[] prices = pro[4].Split('$');
                for (int j = 0; j < stores.Length; j++)
                {
                    productToView.Add(new productView() { name = pro[0], price = prices[j], amount = "0", storeName = stores[j], amounttoAdd = "0", cat = pro[1], manu=pro[2], feedback = controler.getAllFeedbacksSearch(stores[j], pro[0]) });
                }

            }



            dgProducts.ItemsSource = productToView;

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
                FirstPage p = new FirstPage();
                NavigationService.Navigate(p);
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
            myStores m = new myStores(controler.GetMyStore(PageController.username));
            NavigationService.Navigate(m);
        }

        private void browse_Click(object sender, RoutedEventArgs e)
        {
            BrowseItems m = new BrowseItems();
            NavigationService.Navigate(m);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {


            //string a = controler.SearchStore("Castro");

            var productArr = controler.GetAllProducts();
            for (int i = 0; i < productArr.Length; i++)
            {
                string[] pro = productArr[i].Split('&');
                string[] stores = pro[2].Split('$');
                string[] prices = pro[3].Split('$');
                for (int j = 0; j < stores.Length; j++)
                {
                    productToView.Add(new productView() { name = pro[0], price = prices[j], storeName = stores[j], amounttoAdd = "0", cat = pro[1], manu="manu", feedback = controler.getAllFeedbacksSearch(stores[j], pro[0]) });
                }

            }



            dgProducts.ItemsSource = productToView;

            checkMember();
            /*
            //  string product = this.textBox1.Text;
            string product = "";
            string manufacturer = "";
            Dictionary<string, SLproduct> products = controler.BrowseProducts(PageController.username, product, manufacturer);
            foreach (KeyValuePair<string, SLproduct> item in products)
            {

            }*/

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
            bool ans = controler.SaveProduct(PageController.username, p.storeName, p.manu, p.name + "$" + p.amounttoAdd);
            if (ans)
            {
                msg.Content = "the item add to youre cart!";
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
            //Store page = new Store("Castro");
            //NavigationService.Navigate(page);

        }
        private void msg_Click(object sender, RoutedEventArgs e)
        {
            MyMessage page = new MyMessage();
            NavigationService.Navigate(page);

        }
    }
}

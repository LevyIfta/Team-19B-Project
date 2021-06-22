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

namespace ClientWeb.View.components
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Page
    {
        ProductDataToAdd product = new ProductDataToAdd();
        private string username;
        private string storeName;
        static Controller controler = Controller.GetController();

        public AddProduct()
        {
            InitializeComponent();
            this.DataContext = product;
        }

        public AddProduct(string username, string storeName)
        {
            this.username = username;
            this.storeName = storeName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Add product to store 
            controler.AddNewProduct(username, storeName, product.pname, product.price, product.amount, product.cat, product.man);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Store page = new Store(username,storeName);
            NavigationService.Navigate(page);
        }
    }
}

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
            username = PageController.username;
            storeName = PageController.storeForManager;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Add product to store 
            bool ans = controler.AddNewProduct(username, storeName, product.pname, product.price, product.amount, product.cat, product.man);
            if (ans)
            {

                //MessageBox.Show("Product Added succesfully");
                product.msg = "Product Added succesfully";
            }
            else
                product.msg = "Product not Added";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Store page = new Store(storeName);
            NavigationService.Navigate(page);
        }

        private void button_Click_2(object sender, RoutedEventArgs e)
        {
            Page1 p = new Page1();
            NavigationService.Navigate(p);
        }
    }
}

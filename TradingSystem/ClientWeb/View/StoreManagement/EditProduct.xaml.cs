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

namespace ClientWeb.View.StoreManagement
{
    /// <summary>
    /// Interaction logic for EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Page
    {
        ProductDataToAdd product = new ProductDataToAdd();
        private string username;
        private string storeName;
        static Controller controler = Controller.GetController();
        public EditProduct()
        {
            InitializeComponent();
            this.DataContext = product;
            username = PageController.username;
            storeName = PageController.storeForManager;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Add product to store 
            bool ans = controler.EditPrice(username, storeName, product.pname, product.price, product.man);
            if (ans)
            {
                //MessageBox.Show("Product Added succesfully");
                product.msg = "Product saved succesfully";
            }
            else
                product.msg = "Product not saved";
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

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
    /// Interaction logic for RemoveProduct.xaml
    /// </summary>
    public partial class RemoveProduct : Page
    {
        ProductDataToAdd product = new ProductDataToAdd();
        private string username;
        private string storeName;
        static Controller controler = Controller.GetController();
        public RemoveProduct()
        {
            InitializeComponent();
            this.DataContext = product;
            username = PageController.username;
            storeName = PageController.storeForManager;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Add product to store 
            bool ans = controler.RemoveProducts(username, storeName, product.man, product.pname + "$" + product.amountToEdit);
            if (ans)
            {
                product.msg = "Product remove succesfully";
            }
            else
                product.msg = "Product not remove";
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
        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            var productArr = controler.GetAllProducts();
            for (int i = 0; i < productArr.Length; i++)
            {
                string[] pro = productArr[i].Split('&');
                string[] stores = pro[3].Split('$');
                string[] prices = pro[4].Split('$');
                string[] amounts = pro[5].Split('$');
                for (int j = 0; j < stores.Length; j++)
                {
                    if (storeName.Equals(stores[j]) && pro[0].Equals(product.pname))
                        oldamount.Content = amounts[j];
                    //productToView.Add(new productView() { name = pro[0], price = prices[j], amount = "0", storeName = stores[j], amounttoAdd = "0", cat = pro[1], manu = pro[2], feedback = controler.getAllFeedbacksSearch(stores[j], pro[0]) });
                }

            }
        }
    }
}

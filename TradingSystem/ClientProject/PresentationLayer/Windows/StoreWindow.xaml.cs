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

namespace ClientProject.PresentationLayer.Windows
{
    /// <summary>
    /// Interaction logic for StoreWindow.xaml
    /// </summary>
    public partial class StoreWindow : Window
    {
        string msgFailed = "";
        static Controller controller = Controller.GetController();
        private string storeName;

        public StoreWindow(string storeName)
        {
            InitializeComponent();
            this.StoreName.Content = storeName;
            this.storeName = storeName;
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.ProductName == null)
                return;
            string productName = this.ProductName.Text;
            // check for optional inputs
            double minPrice = this.MinPrice == null ? -1 : Double.Parse(this.MinPrice.Text),
                maxPrice = this.MaxPrice == null ? -1 : Double.Parse(this.MaxPrice.Text);
            string manufacturer = this.ProductManufacturer.Text,
                category = this.ProductCategory.Text; // may be empty - meaning any categ/man

            // send a request for searching product in the store via the controller
            ICollection<SLproduct> products = controller.searchProduct(this.storeName, productName, category, manufacturer, minPrice, MaxPrice);

            // view the results


        }
    }
}

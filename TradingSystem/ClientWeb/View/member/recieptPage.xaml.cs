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

namespace ClientWeb.View.member
{
    /// <summary>
    /// Interaction logic for recieptPage.xaml
    /// </summary>
    public partial class recieptPage : Page
    {

        static Controller controler = Controller.GetController();
        List<productView> productToView = new List<productView>();


        public recieptPage()
        {
            InitializeComponent();

            productToView.Add(new productView() { name = "Pro1", price = "15", amount = "12", storeName = "Castro", amounttoAdd = "0" });
            productToView.Add(new productView() { name = "Pro2", price = "15", amount = "12", storeName = "Castro", amounttoAdd = "0" });
            productToView.Add(new productView() { name = "Pro3", price = "15", amount = "12", storeName = "Castro", amounttoAdd = "0" });



            dgProducts.ItemsSource = productToView;

        }

        public void addFeedback(object sender, RoutedEventArgs e)
        {
            productView p = (productView)dgProducts.SelectedItem;
            MessageBox.Show("implement Add feedback to product");

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

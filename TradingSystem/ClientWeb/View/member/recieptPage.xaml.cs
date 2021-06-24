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


        public recieptPage(string reciept_id)
        {


            //// { bool, receipt, receipt }. receipt ->
            ///user$store$price$date$id$products. products -> pro1&pro2&pro3 -> proInfo^feedback -> feedback_feedback -> user#comment
            ///
            string [] ans =  controler.GetAllMyReceiptHistory(PageController.username);
            
            //all recpepits extract the last one and ....



            InitializeComponent();
            userLabel.Content = "reciept for" + PageController.username;
            paymentLabel.Content = "Credit";
            TotalLabel.Content = "price from reciept";


            productToView.Add(new productView() { name = "Pro1", price = "15", amount = "12", storeName = "Castro", amounttoAdd = "0" });
            productToView.Add(new productView() { name = "Pro2", price = "15", amount = "12", storeName = "Castro", amounttoAdd = "0" });
            productToView.Add(new productView() { name = "Pro3", price = "15", amount = "12", storeName = "Castro", amounttoAdd = "0" });



            dgProducts.ItemsSource = productToView;

        }

        public void addFeedback(object sender, RoutedEventArgs e)
        {

            string p = dgProducts.SelectedItem.ToString();
            //MessageBox.Show("implement Add feedback to product");

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

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
    /// Interaction logic for myReceipts.xaml
    /// </summary>
    public partial class myReceipts : Page
    {
        recieptView receipt = new recieptView();
        static Controller controler = Controller.GetController();
        List<recieptView> msgToView = new List<recieptView>();
        public myReceipts()
        {
            InitializeComponent();
            this.DataContext = receipt;

            var receipts = controler.GetAllMyReceiptHistory(PageController.username);
            //receipt -> user$store$price$date$id$products. products -> pro1&pro2&pro3 -> proInfo^feedback -> feedback_feedback -> user#comment
            // proInfo -> productName^price^manu^category^amount^feedback
            for (int i = 0; i < receipts.Length; i++)
            {
                string[] receiptInfo = receipts[i].Split('$');
                string[] products = receiptInfo[5].Split('&');
                string proFinal = "";
                for (int j = 0; j < products.Length; j++)
                {
                    string[] proInfo = products[j].Split('^');
                    proFinal += proInfo[0] + ": " + proInfo[1] + "( x" + proInfo[2] + " ), ";
                }
                msgToView.Add(new recieptView() { id = receiptInfo[4], products = proFinal, price = receiptInfo[2], storeName = receiptInfo[1] });

            }

            dgMasage.ItemsSource = msgToView;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Page page = new Page1();
            NavigationService.Navigate(page);
        }
    }
}

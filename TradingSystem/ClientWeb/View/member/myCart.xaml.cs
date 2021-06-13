using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for myCart.xaml
    /// </summary>
    /// 
    public class productView
    {

        public string Name { get; set; }
        public string Price { get; set; }
        public string Amount { get; set; }
        public string StoreName { get; set; }

    }
    public partial class myCart : Page
    {
        UserData user = new UserData();
        static Controller controler = Controller.GetController();
        public myCart(string username)
        {
            
            user.username = username;
            InitializeComponent();

      
            string[] a = controler.GetCart(username);

            // {basket, basket}. basket -> username&storename&pros. pros -> -> pro$pro -> proInfo^feedback -> feedback_feedback -> user#comment

            /*
           DataTable Data = new DataTable();

            // create "fixed" columns
            Data.Columns.Add("Name");
            Data.Columns.Add("Amount");
            Data.Columns.Add("Store");
            Data.Columns.Add("Man");
          

            // add one row as an object array
            Data.Rows.Add(new object[] { "pro", "12", "Castro", "A" });
            Data.Rows.Add(new object[] { "pro", "12", "Castro", "A" });
            Data.Rows.Add(new object[] { "pro", "12", "Castro", "A" });
            Data.Rows.Add(new object[] { "pro", "12", "Castro", "A" });


            this.DataContext = Data;
            */


            List<productView> productToView = new List<productView>();

            productToView.Add(new productView() {Name = "Pro1", Price = "15" ,Amount = "12" ,StoreName ="Castro" } );
            productToView.Add(new productView() { Name = "Pro2", Price = "15", Amount = "12", StoreName = "Castro" });
            productToView.Add(new productView() { Name = "Pro3", Price = "15", Amount = "12", StoreName = "Castro" });

            dgProducts.ItemsSource = productToView;

            string b = a[0].Split('&')[2].Split('$')[1];
            string storename = a[0].Split('&')[1];
            MessageBox.Show(b);
            MessageBox.Show(storename);



        
        }



        void removeProduct(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    var temp = dgProducts.SelectedItem;
                    
                    MessageBox.Show(row.ToString());

                }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            paymentPage m = new paymentPage();
            NavigationService.Navigate(m);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

    

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

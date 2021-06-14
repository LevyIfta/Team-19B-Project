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
    class productView : ANotifyPropChange
    {

        private String Name;

        public String name
        {
            get { return Name; }
            set
            {
                Name = value;
                OnPropertyChanged();
            }
        }

        private String Price;

        public String price
        {
            get { return Price; }
            set
            {
                Price = value;
                OnPropertyChanged();
            }
        }



        private String Amount;

        public String amount
        {
            get { return Amount; }
            set
            {
                Amount = value;
                OnPropertyChanged();
            }
        }
        private String AmounttoAdd;

        public String amounttoAdd
        {
            get { return AmounttoAdd; }
            set
            {
                AmounttoAdd = value;
                OnPropertyChanged();
            }
        }
        private String StoreName;

        public String storeName
        {
            get { return StoreName; }
            set
            {
                StoreName = value;
                OnPropertyChanged();
            }
        }



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
            string[,] a1 = convertToViewObj.Product(a);

            // {basket, basket}. basket -> username&storename&pros. pros -> -> pro$pro -> proInfo^feedback -> feedback_feedback -> user#comment

 
            List<productView> productToView = new List<productView>();
         for( int i=0; i<a1.Length; i++)
            {
                productToView.Add(new productView() { name = a1[i,0] , price = a1[i, 1], amount = a1[i, 2], storeName = a1[i,3]});
            }
         /*
            productToView.Add(new productView() {name = "Pro1", price = "15" ,amount = "12" ,storeName ="Castro", amounttoAdd = "0" } );
            productToView.Add(new productView() { name = "Pro2", price = "15", amount = "12", storeName = "Castro", amounttoAdd = "0" });
            productToView.Add(new productView() { name = "Pro3", price = "15", amount = "12", storeName = "Castro", amounttoAdd = "0" });
         */
            dgProducts.ItemsSource = productToView;

            string b = a[0].Split('&')[2].Split('$')[1];
            string storename = a[0].Split('&')[1];
            MessageBox.Show(b);
            MessageBox.Show(storename);



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

        private void dgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

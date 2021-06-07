using ClientWeb.Objects;
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

namespace ClientWeb.Windows
{
    /// <summary>
    /// Interaction logic for BrowseItems.xaml
    /// </summary>
    public partial class BrowseItems : Page
    {
        private static Controller controller = Controller.GetController();
        public BrowseItems()
        {
            InitializeComponent();
            string username = controller.getUserName();
            this.userName.Content = username;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = (string)this.userName.Content;
            string product = this.TextBoxName.Text;
            string manufacturer = this.TextBxMan.Text;
             Dictionary<string, SLproduct> products =  controller.BrowseProducts(username, product, manufacturer);
            foreach (KeyValuePair<string, SLproduct> item in products)
            {
                Label lab = new Label();
                lab.Content = "store: " + item.Key + "   name: " + item.Value.productName + "   manufactorer: " + item.Value.manufacturer;

                this.ListBox1.Items.Add(lab);

            }


        }
    }
}

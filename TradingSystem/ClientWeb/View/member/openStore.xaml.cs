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
    /// Interaction logic for openStore.xaml
    /// </summary>
    public partial class openStore : Page
    {

        StoreData store = new StoreData();

        static Controller controler = Controller.GetController();
        public openStore()
        {
            InitializeComponent();

            this.DataContext = store;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            controler.OpenStore("almog", store.storename);
        }
    }
}

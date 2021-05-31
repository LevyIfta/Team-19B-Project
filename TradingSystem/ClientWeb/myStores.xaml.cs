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
    /// Interaction logic for myStores.xaml
    /// </summary>
    public partial class myStores : Page
    {
        public myStores()
        {
            InitializeComponent();
            Button b1 = new Button();
            b1.Click += button1_Click;
            b1.Content = "name1";
            this.StackList.Children.Add(b1);
            Button b2 = new Button();
            b2.Click += button1_Click;
            b2.Content = "name2";
            this.StackList.Children.Add(b2);
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            login l = new login();
            NavigationService.Navigate(l);
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            register l = new register();
            NavigationService.Navigate(l);
        }
    }
}

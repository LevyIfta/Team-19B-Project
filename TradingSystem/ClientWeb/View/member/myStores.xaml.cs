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

        UserData user = new UserData();

        public myStores(ICollection<string> storesNames, string username)
        {
            InitializeComponent();

            user.username = username;
            MessageBox.Show(username);
            dataGrid1.DataContext = storesNames;
            dataGrid1.ItemsSource = storesNames;
            /*  foreach (string storeName in storesNames)
                  addButton(storeName);*/
        }

        /*
        private void addButton(string storeName)
        {
            Button storeButton = new Button();
            storeButton.Content = storeName;
            storeButton.Click += (r, e) => openStoreWindow(storeName);
            var window = new Window();
            var stackPanel = new StackPanel { Orientation = Orientation.Vertical };
            //this.StackList.Children.Add(storeButton);

            stackPanel.Children.Add(storeButton);
            //stackPanel.Children.Add(new Button { Content = "Button" });
            window.Content = stackPanel;
            
        }

        private void openStoreWindow(string storeName)
        {
            Store storeWindow = new Store(storeName, user.username);
            NavigationService.Navigate(storeWindow);
        }
        */

        private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string selected = dataGrid1.SelectedItem.ToString();
            Store storePage = new Store(selected, user.username);
            NavigationService.Navigate(storePage);
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

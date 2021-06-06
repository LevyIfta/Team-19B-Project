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
        private string username;
        public myStores(ICollection<string> storesNames, string username)
        {
            InitializeComponent();

            this.username = username;

            foreach (string storeName in storesNames)
                addButton(storeName);
        }

        private void addButton(string storeName)
        {
            Button storeButton = new Button();
            storeButton.Content = storeName;
            storeButton.Click += (r, e) => openStoreWindow(storeName);
            this.StackList.Children.Add(storeButton);
        }

        private void openStoreWindow(string storeName)
        {
            Store storeWindow = new Store(storeName, username);
            NavigationService.Navigate(storeWindow);
        }
    }
}

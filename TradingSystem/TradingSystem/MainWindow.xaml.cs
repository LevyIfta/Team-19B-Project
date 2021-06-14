using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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


namespace TradingSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
          
            InitializeComponent();
            Application.Current.Exit += new ExitEventHandler(disconnect);
           
          

        }



        private void disconnect(object sender, ExitEventArgs args)
        {
            ServiceLayer.ServerConnectionManager.disconnect();
        }

        private void loadDB(object sender, RoutedEventArgs e)
        {
            ServiceLayer.Controllers.SystemController.Build();
        }

        private void tearDown(object sender, RoutedEventArgs e)
        {
            ServiceLayer.Controllers.SystemController.tearDown();
        }

        private void startServer(object sender, RoutedEventArgs e)
        {
            new Thread(new ThreadStart(ServiceLayer.ServerConnectionManager.init)).Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          object ans =  TradingSystem.ServiceLayer.UserController.register("grim", "Oirlee1");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           object ans = TradingSystem.ServiceLayer.UserController.login("grim", "Oirlee1");
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
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
        private static string configPath = "C:/Users/Ali/לימודים/סמסטר ו/סדנא/v3/configurationFile.txt";
        public MainWindow()
        {
          
            InitializeComponent();
            Application.Current.Exit += new ExitEventHandler(disconnect);
            initOutsideSystems();
        }

        private void initOutsideSystems()
        {
            initPaymentSystem();
            initSupplySystem();
        }

        private void initSupplySystem()
        {
            string supplyURL = getSupplyURL();
        }

        private void initPaymentSystem()
        {
            string paymentURL = getPaymentURL();
            
            if (!paymentURL.Equals(""))
                PaymentSystem.VerificationSystem.setReal(paymentURL);
        }

        private static string getSupplyURL()
        {
            // check if there is a valid url for the supply system, if so return it, else return ""
            string paymentURL = "";
            string[] configLines = File.ReadAllLines(configPath);

            foreach (string line in configLines)
                if (line.Length >= 13 && line.Substring(0, 13).Equals("Supply_system"))
                    paymentURL = line.Substring(15);

            return paymentURL;
        }

        private static string getPaymentURL()
        {
            // check if there is a valid url for the payment system, if so return it, else return ""
            string paymentURL = "";
            string[] configLines = File.ReadAllLines(configPath);

            foreach (string line in configLines)
                if (line.Length >= 14 && line.Substring(0, 14).Equals("Payment_system"))
                    paymentURL = line.Substring(16);

            return paymentURL;
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

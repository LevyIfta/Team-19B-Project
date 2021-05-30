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
            
            new Thread(new ThreadStart( ServiceLayer.ServerConnectionManager.init)).Start();
            Application.Current.Exit += new ExitEventHandler(disconnect);

        }



        public void dothedo(TradingSystem.ServiceLayer.DecodedMessge msg)
        {
            
            this.label2.Content = "" + msg.type + "  " + msg.name + "  " + msg.param_list.ToString();   
        }
        private void disconnect(object sender, ExitEventArgs args)
        {
            ServiceLayer.ServerConnectionManager.disconnect();
        }
    }
}

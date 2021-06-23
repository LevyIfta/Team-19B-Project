using ClientWeb.View.StoreManagement;
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

namespace ClientWeb.View.member
{
    /// <summary>
    /// Interaction logic for MangeManagers.xaml
    /// </summary>
    public partial class MangeManagers : Page
    {
        MessageData message = new MessageData();
        static Controller controler = Controller.GetController();
        List<MessageData> msgToView = new List<MessageData>();
        public MangeManagers()
        {
            InitializeComponent();
            this.DataContext = message;

            var msgArr = controler.GetStoreMessages(PageController.username, PageController.storeForManager);
            for (int i = 0; i < msgArr.Length; i++)
            {
                string[] info = msgArr[i].Split('$');
                msgToView.Add(new MessageData() { tosend = info[0], messagerecive = info[3], isnew = info[4]});

            }



            dgMasage.ItemsSource = msgToView;
        }


        public void addToBasket(object sender, RoutedEventArgs e)
        {
            MessageData p = (MessageData)dgMasage.SelectedItem;

            //add item to cart ( basket) 
            bool ans = controler.SendMessage(PageController.username, p.tosend, "", p.messagesend);
            if (ans)
            {
                p.messagesend = "";
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}

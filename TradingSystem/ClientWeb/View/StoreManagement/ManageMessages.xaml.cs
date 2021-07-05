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

namespace ClientWeb.View.StoreManagement
{
    /// <summary>
    /// Interaction logic for ManageMessages.xaml
    /// </summary>
    public partial class ManageMessages : Page
    {
        MessageData message = new MessageData();
        static Controller controler = Controller.GetController();
        List<MessageData> msgToView = new List<MessageData>();
        public ManageMessages()
        {
            InitializeComponent();
            this.DataContext = message;

            var msgArr = controler.GetStoreMessages(PageController.username, PageController.storeForManager);
            for (int i = 0; i < msgArr.Length; i++)
            {
                string[] info = msgArr[i].Split('$');
                msgToView.Add(new MessageData() { tosend = info[0], messagerecive = info[3], isnew = info[4] });

            }



            dgMasage.ItemsSource = msgToView;
        }
        public void addToBasket(object sender, RoutedEventArgs e)
        {
            MessageData p = (MessageData)dgMasage.SelectedItem;

            //add item to cart ( basket) 
            bool ans = controler.SendMessage(PageController.username, p.tosend, "", p.messagesend, PageController.storeForManager);
            if (ans)
            {
                p.messagesend = "";
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Store page = new Store(PageController.storeForManager);
            NavigationService.Navigate(page);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var ans = controler.SendMessage(PageController.username, this.usernew1.Text, "", this.messagenew1.Text, PageController.storeForManager);
            if (ans)
                this.msgnew.Content = "message is send";
            else
                this.msgnew.Content = "somting went worng";
        }
    }
}

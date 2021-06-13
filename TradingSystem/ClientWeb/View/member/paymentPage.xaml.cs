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
    /// Interaction logic for paymentPage.xaml
    /// </summary>
    public partial class paymentPage : Page
    {
        static Controller controler = Controller.GetController();
        PurchData data = new PurchData();
        UserData user = new UserData();
        List<string> errors;
        public paymentPage()
        {
            InitializeComponent();
            this.DataContext = data;
            data.total = "the total price is: " + controler.CheckPrice(user.username);
        }
        private void ButtomCheck() // textbox valid check 
        {
            if (data.cradit == null)
                errors.Add("cradit number empty");
            if (data.validity == null)
                errors.Add("validity empty");
            if (data.cvv == null)
                errors.Add("cvv number empty");
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (errors.Count > 0)
            {
                foreach (string ms in errors)
                    data.msg += ms + ", ";
            }
            else
            {
                string[] ans = controler.Purchase(user.username, data.cradit, data.validity, (string)data.cvv);
                if (ans != null)
                {
                    if (ans[0].Equals("false"))
                    {
                        for (int i = 1; i < ans.Length; i++)
                            data.msg += ans[i] + ", ";
                    }
                    else
                    {
                        data.msg = "the purchase have succsess!!!!";
                        // show reciept
                    }
                }
                else
                {
                    data.msg = "something went worng";
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}

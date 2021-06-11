
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
    /// Interaction logic for register.xaml
    /// </summary>
    public partial class register : Page
    {
        UserData user = new UserData();
        List<string> errors;
        static Controller controler = Controller.GetController();
        public register()
        {
            InitializeComponent();
            this.DataContext = user;
        }
        private void ButtomCheck() // textbox valid check 
        {
            String pass = "c";
            String passC = "t";
            if (user.username == null)
                errors.Add("User name empty");
            if (user.password == null)
                errors.Add("Password empty");
            else
                pass = user.password;
            if (user.passwordC == null)
                errors.Add("PasswordC empty");
            else
                passC = user.passwordC;
            if (!(pass.Equals(passC)) & (pass != "c") & (passC != "t"))
                errors.Add("Password isn't confirm");
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {


            string[] ans = controler.Register(user.username, user.password);
            //string[] ans = mockData.register();
            if (ans != null && ans[0].Equals("true"))
            {
                user.usermsg = "Registretion Su";
                MessageBox.Show("Success!");
            }
            else
            {
                MessageBox.Show("Err");
                user.usermsg = "Registretion Faild\n" + ans[1];
            }
            /*
            bool insertValid = true;
            if (errors.Count > 0)
            {
                insertValid = false;
                if (errors.Contains("User name empty"))
                    user.usermsg = "User name empty";
                if (errors.Contains("Password empty"))
                    user.usermsg += " Password empty";
                if (errors.Contains("PasswordC empty"))
                    user.usermsg += " Password confirm empty";
                if (errors.Contains("Password isn't confirm"))
                    user.usermsg += " Passwords must match";
            }
            if (insertValid)
            {
                string[] ans = controler.Register(this.textBox1.Text, this.textBox2.Text);
                if (ans != null && ans[0].Equals("true"))
                {
                    user.usermsg = "Registretion Su";
                }
                else
                {
                    user.usermsg = "Registretion Faild\n" + ans[1];
                }
            }
            */
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            login page = new login();
            NavigationService.Navigate(page);
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

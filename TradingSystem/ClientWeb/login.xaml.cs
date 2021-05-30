﻿using System;
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
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Page
    {
        UserData user = new UserData();
        List<string> errors;
        static Controller controler = Controller.GetController();
        public login()
        {
            InitializeComponent();
            this.DataContext = user;
        }
        private void CleanButton()
        {
            errors = new List<string>();
            user.usermsg = "";
        }
        private void ButtomCheck() // textbox valid check 
        {
            if (user.username == null)
                errors.Add("User name empty");
            if (user.password == null)
                errors.Add("Password empty");
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            CleanButton();
            bool insertValid = true;
            if (errors.Count > 0)
            {
                insertValid = false;
                if (errors.Contains("User name empty"))
                    user.usermsg = "User name empty";
            }
            if (insertValid)
            {
                string[] ans = controler.Login(user.username, user.password);
                if (ans != null && ans[0].Equals("true"))
                {
                    PageController.username = user.username;
                    Page1 page1 = new Page1();
                    this.Content = page1;
                }
                else
                {
                    user.loginname = "";
                    user.password = "";
                    user.usermsg = ans[1];
                }
            }
        }
    }
}
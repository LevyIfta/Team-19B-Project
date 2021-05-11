﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_Trial2.PresentationLayer.DataContext;
using ClientProject;

namespace WPF_Trial2.PresentationLayer.Windows
{
    class UserDataName : ANotifyPropChange
    {
        private String Username;

        public String userhello
        {
            get { return Username; }
            set
            {
                Username = value;
                OnPropertyChanged();
            }
        }
    }
    public partial class MainWindow : Window
    {
        static Controller controler = Controller.GetController();
        UserDataName user = new UserDataName();
        private class searchString : INotifyPropertyChanged
        {
            private String search;
            public String Search
            {
                get
                {
                    return search;
                }
                set
                {
                    search = value;
                    OnPropertyChanged(search);
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string search)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(search));
                }
            }
        }

        

        //private UserDataContext userDataContext;
        private List<StoreDataContext> storesDataContexts;
        private searchString searchStr;

        public MainWindow()
        {
            //this.userDataContext = new UserDataContext();
            this.DataContext = user;
            user.userhello = "guest";
            string ans = controler.getUserName();
            if (ans != null)
                user.userhello = ans;
            //this.UserNameHello = ans;
            this.storesDataContexts = new List<StoreDataContext>();
            this.searchStr = new searchString();
            InitializeComponent();
            this.byStore.IsChecked = true;


            //testing purpose
            this.storesDataContexts.Add(new StoreDataContext("a"));
            this.storesDataContexts.Add(new StoreDataContext("b"));
            this.storesDataContexts.Add(new StoreDataContext("c"));
        }

        //---search bar grayed text---
        private void byStoreChecked(object sender, RoutedEventArgs e)
        {
            this.searchBarText.Text = " Store name:";
        }
        private void byProductChecked(object sender, RoutedEventArgs e)
        {
            this.searchBarText.Text = " Product name:";
        }
        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            this.searchBarText.Visibility = Visibility.Hidden;
        }
        private void TextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (this.searchBarBox.Text.Length < 1)
            {
                this.searchBarText.Visibility = Visibility.Visible;
            }
            
        }

       
        //used by login window
        public void setLoggedInUser(UserDataContext udc)
        {
            //this.userDataContext = udc;
        }


        private void Search(object sender, RoutedEventArgs e)
        {
            this.button1.Content = this.searchStr.Search;
        }

        private void OpenStore(object sender, RoutedEventArgs e)
        {
            bool ans = controler.OpenStore("1", "2");
        }

        private void login_register_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            App.Current.MainWindow = loginWindow;
            this.Close();
            //loginWindow.ShowDialog();
        }
        private void logout(object sender, RoutedEventArgs e)
        {
            bool ans = controler.Logout();
            if (!ans)
            {
                user.userhello = "Failed logout";
            }
            else
            {
                user.userhello = "guest";
            }
            //loginWindow.ShowDialog();
        }
    }
}

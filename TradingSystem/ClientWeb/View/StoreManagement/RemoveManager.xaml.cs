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
    /// Interaction logic for RemoveManager.xaml
    /// </summary>
    public partial class RemoveManager : Page
    {
        employeeStore employee = new employeeStore();
        static Controller controler = Controller.GetController();
        public RemoveManager()
        {
            InitializeComponent();
            this.DataContext = employee;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (employee.employeename == null)
            {
                employee.msg = "user name empty";
            }
            else
            {
                bool ans = controler.RemoveManager(PageController.username, PageController.storeForManager, employee.employeename);

                if (ans)
                {
                    employee.employeename = "";
                    employee.msg = "user fire!";
                }
                else
                {
                    employee.msg = "user not fire...";
                }
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Store page = new Store(PageController.storeForManager);
            NavigationService.Navigate(page);
        }
    }
}

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
    /// Interaction logic for EditOwner.xaml
    /// </summary>
    public partial class EditOwner : Page
    {
        employeeStore employee = new employeeStore();
        static Controller controler = Controller.GetController();
        public EditOwner()
        {
            InitializeComponent();
            this.DataContext = employee;
            employee.permenu = PageController.permissionsMenu;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (employee.employeename == null)
            {
                employee.msg = "user name empty";
            }
            if (employee.permissions == null)
            {
                employee.msg = "permissions empty";
            }
            else
            {
                bool ans = controler.EditManagerPermissions(PageController.username, PageController.storeForManager, employee.employeename, convertToViewObj.PermmisionsInsert(employee.permissions));

                if (ans)
                {
                    employee.employeename = "";
                    employee.msg = "changes save!";
                }
                else
                {
                    employee.msg = "no...";
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

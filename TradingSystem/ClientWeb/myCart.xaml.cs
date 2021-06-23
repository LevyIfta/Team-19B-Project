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
    /// Interaction logic for AddManager.xaml
    /// </summary>
    public partial class AddManager : Page
    {
        employeeStore employee = new employeeStore();
        List<string> errors;
        static Controller controler = Controller.GetController();
        public AddManager()
        {
            InitializeComponent();
            this.DataContext = employee;
            employee.permenu = PageController.permissionsMenu;
        }
        private void CleanButton()
        {
            errors = new List<string>();
            employee.msg = "";
        }
        private void ButtomCheck() // textbox valid check 
        {
            if (employee.employeename == null)
                errors.Add("User name empty");
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //bool valid = true;
            if (errors.Count > 0)
            {
                //valid = false;
                if (errors.Contains("User name empty"))
                    employee.msg = "User name empty";
            }
            else
            {
                bool ans = false;

                if (employee.permissions == null || employee.permissions.Length == 0)
                { // manager
                    ans = controler.HireNewStoreManager(PageController.username, PageController.storeForManager, employee.employeename);
                    
                }
                else
                {
                    ans = controler.HireNewStoreOwner(PageController.username, PageController.storeForManager, employee.employeename, convertToViewObj.PermmisionsInsert(employee.permissions));
                }

                if (ans)
                {
                    employee.employeename = "";
                    employee.msg = "user hire!";
                }
                else
                {
                    employee.msg = "user not hire...";
                }
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}

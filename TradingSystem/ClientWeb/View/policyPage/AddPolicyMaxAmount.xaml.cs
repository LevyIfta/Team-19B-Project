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

namespace ClientWeb.View.policyPage
{
    /// <summary>
    /// Interaction logic for AddPolicyMaxAmount.xaml
    /// </summary>
    public partial class AddPolicyMaxAmount : Page
    {
        PolicyToAdd policy = new PolicyToAdd();
        static Controller controler = Controller.GetController();

        public AddPolicyMaxAmount()
        {
            InitializeComponent();
            this.DataContext = policy;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool ans = true; // controller.Add maxpolicy  policy.Pname , policy.maxAmount
            if (ans)
            {

                //MessageBox.Show("Product Added succesfully");
                policy.msg = "Policy Added succesfully";
            }
            else
                policy.msg = "Policy not Added";
        }
    }
    }


using ClientWeb.View.components;
using ClientWeb.View.member;
using ClientWeb.View.StoreManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for Store.xaml
    /// </summary>
    public partial class Store : Page
    {
        private static Controller controller = Controller.GetController();
        List<productView> productToView = new List<productView>();
        List<employeeView> employee = new List<employeeView>();
        private string storeName;
        private string username;
        public Store(string storeName)
        {
            InitializeComponent();

            this.storeName = storeName;
            PageController.storeForManager = storeName;
            this.username = PageController.username;
            this.storeNameLabel.Content = storeName;
            //initActionsStack();
            initEmployees();
            initProduct();
        }

        private void initActionsStack()
        {
            /*
            ICollection<string> permissions = controller.GetPermissions(username, storeName);
            if (permissions == null)
                return;

            foreach (string permission in permissions)
                addPermissionButton(permission);
            // we assume, for the mean time, that there is no permission for adding a new policy
            addPermissionButton("AddPolicy");
            */
        }
        private void initEmployees()
        {
            string storename = PageController.storeForManager;
            var emp = controller.GetInfoEmployees(PageController.username, storename);

            for (int i = 0; i < emp.Length; i++)
            {
                string[] info = emp[i].Split('$');
                string[] perm = info[1].Split('&');
                string permFinal = "";
                for (int j = 0; j < perm.Length; j++)
                {
                    string[] permInfo = perm[j].Split('^');
                    if (permInfo[0].Equals(storename))
                    {
                        string[] permList = permInfo[1].Split('#');
                        for (int h = 0; h < permList.Length; h++)
                        {
                            permFinal += permList[h] + ", ";
                        }

                    }
                    if (permFinal.Length > 0)
                    {
                        permFinal = permFinal.Substring(0, permFinal.Length - 2);
                        employee.Add(new employeeView() { employeename = info[0], permissions = permFinal });
                    }

                }

            }

            dgEmployees.ItemsSource = employee;
        }
        private void initProduct()
        {
            var productArr = controller.GetAllProducts();
            for (int i = 0; i < productArr.Length; i++)
            {
                string[] pro = productArr[i].Split('&');
                string[] stores = pro[3].Split('$');
                string[] prices = pro[4].Split('$');
                string[] amounts = pro[5].Split('$');
                for (int j = 0; j < stores.Length; j++)
                {
                    if(stores[j].Equals(storeName))
                        productToView.Add(new productView() { name = pro[0], price = prices[j], amount = amounts[j], cat = pro[1], manu = pro[2], feedback = controller.getAllFeedbacksSearch(stores[j], pro[0]) });
                }

            }

            dgProducts.ItemsSource = productToView;
        }
        private void addPermissionButton(string permission)
        {
            if (!permission.Equals("EditProduct"))
            {
                Button permissionButton = new Button();
                permissionButton.Content = permission;
                permissionButton.Click += (r, e) => act(permission);
                this.actionsStack.Children.Add(permissionButton);
            }
            else
            {
                Button permissionButtonEditPrice = new Button();
                permissionButtonEditPrice.Content = "EditPrice";
                permissionButtonEditPrice.Click += (r, e) => act("EditPrice");
                this.actionsStack.Children.Add(permissionButtonEditPrice);

                Button permissionButtonSupply = new Button();
                permissionButtonSupply.Content = "Supply";
                permissionButtonSupply.Click += (r, e) => act("Supply");
                this.actionsStack.Children.Add(permissionButtonSupply);
            }
        } // hire_Click_1

        private void act(string action)
        {
            switch (action)
            {
                case "AddPolicy":
                    addPolicy();
                    break;

                case "AddProduct":
                    addProduct();
                    break;

                case "EditManagerPermissions":
                    EditManagerPermissions();
                    break;

                case "EditPrice":
                    EditPrice();
                    break;

                case "Supply":
                    supply();
                    break;

                case "GetInfoEmployees":
                    GetInfoEmployees();
                    break;

                case "GetPurchaseHistory":
                    GetPurchaseHistory();
                    break;

                case "HireNewStoreManager":
                    HireNewStoreManager();
                    break;

                case "HireNewStoreOwner":
                    HireNewStoreOwner();
                    break;

                case "RemoveManager":
                    RemoveManager();
                    break;

                case "RemoveProduct":
                    RemoveProduct();
                    break;

                case "RemoveOwner":
                    RemoveOwner();
                    break;

                default:
                    break;
            }
        }

        private void addPolicy()
        {
            throw new NotImplementedException();
        }

        private void RemoveOwner()
        {
            mainGrid.Children.Clear();
            // /xaml objexts to add to grid - to be deleteds
            Label usernameLabel = new Label(), permissionsLabel = new Label();
            TextBox usernameTextBox = new TextBox();
            Button actButton = new Button();

            usernameLabel.Content = "Owner to remove:";

            actButton.Content = "Remove owner";
            actButton.Click += (r, e) =>
            {
                //controller.removeOwner(username, storeName, usernameTextBox.Text);
            };

            addToMainGrid(new UIElement[] { usernameLabel, permissionsLabel, usernameTextBox, actButton });
        }

        private void RemoveProduct()
        {
            mainGrid.Children.Clear();
            // init UI elements
            Label productNameLabel = new Label(),
                manLabel = new Label();

            TextBox productNameTextBox = new TextBox(),
                manTextBox = new TextBox();

            Button addProduct = new Button();

            addProduct.Content = "Remove product";

            // init label names
            productNameLabel.Content = "Product name:";
            manLabel.Content = "Manufacturer:";

            addToMainGrid(new UIElement[] { productNameLabel, productNameTextBox, manLabel, manTextBox });

            addProduct.Click += (r, e) =>
            {
                controller.RemoveProductFromStore(username, storeName, productNameTextBox.Text, manTextBox.Text);
            };
        }

        private void RemoveManager()
        {
            mainGrid.Children.Clear();
            // /xaml objexts to add to grid - to be deleteds
            Label usernameLabel = new Label();
            TextBox usernameTextBox = new TextBox();
            Button actButton = new Button();

            usernameLabel.Content = "Manager to remove:";

            actButton.Content = "Remove manager";
            actButton.Click += (r, e) =>
            {
                controller.RemoveManager(username, storeName, usernameTextBox.Text);
            };

            addToMainGrid(new UIElement[] { usernameLabel, usernameTextBox, actButton });
        }

        private void HireNewStoreOwner()
        {
            mainGrid.Children.Clear();
            // /xaml objexts to add to grid - to be deleteds
            Label usernameLabel = new Label(), permissionsLabel = new Label();
            TextBox usernameTextBox = new TextBox(), permissionsTextBox = new TextBox();
            Button actButton = new Button();

            usernameLabel.Content = "User to hire:";
            permissionsLabel.Content = "Permissions:";

            actButton.Content = "Add owner";
            actButton.Click += (r, e) =>
            {
                controller.HireNewStoreOwner(username, storeName, usernameTextBox.Text, permissionsTextBox.Text);
            };

            addToMainGrid(new UIElement[] { usernameLabel, permissionsLabel, usernameTextBox, permissionsTextBox, actButton });

        }

        private void HireNewStoreManager()
        {
            mainGrid.Children.Clear();
            // /xaml objexts to add to grid - to be deleteds
            Label usernameLabel = new Label();
            TextBox usernameTextBox = new TextBox();
            Button actButton = new Button();

            usernameLabel.Content = "User to hire:";

            actButton.Content = "Add manager";
            actButton.Click += (r, e) =>
            {
                controller.HireNewStoreManager(username, storeName, usernameTextBox.Text);
            };

            addToMainGrid(new UIElement[] { usernameLabel, usernameTextBox, actButton });
        }

        private void GetPurchaseHistory()
        {
            mainGrid.Children.Clear();
            string[] receipts = controller.GetReceiptsHistory(username, storeName);
            UIElement[] receiptsUIE = convertReceiptsToElements(receipts);

            addToMainGrid(receiptsUIE);
        }

        private UIElement[] convertReceiptsToElements(string[] receipts)
        {
            UIElement[] receiptsElements = new UIElement[receipts.Length];

            for (int i = 0; i < receipts.Length; i++)
            {
                Label tmp = new Label();
                tmp.Content = receipts[i];
                receiptsElements[i] = tmp;
            }

            return receiptsElements;
        }

        private void GetInfoEmployees()
        {
            mainGrid.Children.Clear();
            string[] infoEmp = controller.GetInfoEmployees(username, storeName);
            UIElement[] infoEmpUIE = new UIElement[infoEmp.Length];

            for (int i = 0; i < infoEmp.Length; i++)
            {
                Label infoLabel = new Label();
                infoLabel.Content = infoEmp[i];
                infoEmpUIE[i] = infoLabel;
            }

            addToMainGrid(infoEmpUIE);
        }

        private void EditPrice()
        {
            mainGrid.Children.Clear();
            // init UI elements
            Label productNameLabel = new Label(),
                manLabel = new Label(),
                priceLabel = new Label();

            TextBox productNameTextBox = new TextBox(),
                manTextBox = new TextBox(),
                priceTextBox = new TextBox();

            Button editPrice = new Button();

            // init label names
            productNameLabel.Content = "Product name:";
            manLabel.Content = "Manufacturer:";
            priceLabel.Content = "New price:";

            editPrice.Content = "Edit price";

            addToMainGrid(new UIElement[] { productNameLabel, productNameTextBox, manLabel, manTextBox, priceLabel, priceTextBox });

            editPrice.Click += (r, e) =>
            {
                controller.EditPrice(username, storeName, productNameTextBox.Text, priceTextBox.Text, manTextBox.Text);
            };
        }

        private void supply()
        {
            mainGrid.Children.Clear();
            // init UI elements
            Label productNameLabel = new Label(),
                manLabel = new Label(),
                supplyLabel = new Label();

            TextBox productNameTextBox = new TextBox(),
                manTextBox = new TextBox(),
                supplyTextBox = new TextBox();

            Button supply = new Button();

            // init label names
            productNameLabel.Content = "Product name:";
            manLabel.Content = "Manufacturer:";
            supplyLabel.Content = "Amount to supply:";

            supply.Content = "Supply";

            addToMainGrid(new UIElement[] { productNameLabel, productNameTextBox, manLabel, manTextBox, supplyLabel, supplyTextBox });

            supply.Click += (r, e) =>
            {
                controller.supply(username, storeName, productNameTextBox.Text, supplyTextBox.Text, manTextBox.Text);
            };
        }

        private void EditManagerPermissions()
        {
            mainGrid.Children.Clear();
            // /xaml objexts to add to grid - to be deleteds
            Label usernameLabel = new Label(), permissionsLabel = new Label();
            TextBox usernameTextBox = new TextBox(), permissionsTextBox = new TextBox();
            Button actButton = new Button();

            usernameLabel.Content = "User to edit:";
            permissionsLabel.Content = "Permissions";

            actButton.Content = "Edit permissions";

            actButton.Click += (r, e) =>
            {
                controller.EditManagerPermissions(username, storeName, usernameTextBox.Text, permissionsTextBox.Text);
            };

            addToMainGrid(new UIElement[] { usernameLabel, usernameTextBox, actButton });
        }

        private void addProduct()
        {
            mainGrid.Children.Clear();
            // init UI elements
            Label productNameLabel = new Label(),
                categoryLabel = new Label(),
                manLabel = new Label(),
                amountLabel = new Label(),
                priceLabel = new Label();

            TextBox productNameTextBox = new TextBox(),
                categoryTextBox = new TextBox(),
                manTextBox = new TextBox(),
                amountTextBox = new TextBox(),
                priceTextBox = new TextBox();

            Button addProduct = new Button();

            // init label names
            productNameLabel.Content = "Product name:";
            categoryLabel.Content = "Category:";
            manLabel.Content = "Manufacturer:";
            amountLabel.Content = "Amount:";
            priceLabel.Content = "Price:";

            addProduct.Content = "Add product";

            addToMainGrid(new UIElement[] { productNameLabel, productNameTextBox, categoryLabel, categoryTextBox, manLabel, manTextBox, amountLabel, amountTextBox, priceLabel, priceTextBox });

            addProduct.Click += (r, e) =>
            {
                controller.AddNewProduct(username, storeName, productNameTextBox.Text, priceTextBox.Text, amountTextBox.Text, categoryTextBox.Text, manTextBox.Text);
            };
        }

        private void addToMainGrid(UIElement[] uIElement)
        {
            foreach (UIElement element in uIElement)
                this.mainGrid.Children.Add(element);
        }

        private void removeFromMainGrid(UIElement[] uIElement)
        {
            foreach (UIElement element in uIElement)
                this.mainGrid.Children.Remove(element);
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            var ans = controller.CloseStore(PageController.username, PageController.storeForManager);
            if (ans)
                msgStore.Content = "the store is close";
            else
                msgStore.Content = "somting went worng";
        }

        private void removeP_Click_1(object sender, RoutedEventArgs e)
        {
            Page p = new RemoveProduct();
            NavigationService.Navigate(p);
        }
        private void editP_Click_1(object sender, RoutedEventArgs e)
        {
            Page p = new EditProduct();
            NavigationService.Navigate(p);
        }

        private void infoemployees_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void addproduct_Click_1(object sender, RoutedEventArgs e)
        {
            Page p = new AddProduct();
            NavigationService.Navigate(p);
        }
        private void hire_Click_1(object sender, RoutedEventArgs e)
        {
            AddManager p = new AddManager();
            NavigationService.Navigate(p);
        }
        private void editempl_Click(object sender, RoutedEventArgs e)
        {
            Page p = new EditOwner();
            NavigationService.Navigate(p);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Page1 page1 = new Page1();
            NavigationService.Navigate(page1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Page p = new ManageDiscountPolicies(username, storeName);
            NavigationService.Navigate(p);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Page p = new ManagePurchasePolicies(username, storeName);
            NavigationService.Navigate(p);
        }

        private void feedback_Click(object sender, RoutedEventArgs e)
        {
            Page p = new ManageFeedBacks(username, storeName);
            NavigationService.Navigate(p);
        }

        private void receipts_Click(object sender, RoutedEventArgs e)
        {
            Page p = new StoreReciepts(username, storeName);
            NavigationService.Navigate(p);
        }

        private void fireemplyee_Click(object sender, RoutedEventArgs e)
        {
            RemoveManager p = new RemoveManager();
            NavigationService.Navigate(p);
        }
        private void dgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

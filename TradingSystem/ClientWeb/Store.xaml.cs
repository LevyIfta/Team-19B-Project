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
        private string storeName;
        private string username;
        public Store(string storeName, string username)
        {
            InitializeComponent();

            this.storeName = storeName;
            this.username = username;
            this.storeNameLabel.Content = storeName;

            initActionsStack();
        }

        private void initActionsStack()
        {
            ICollection<string> permissions = controller.GetPermissions(username, storeName);
            if (permissions == null)
                return;

            foreach (string permission in permissions)
                addPermissionButton(permission);
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
                permissionButtonEditPrice.Content = "Edit price";
                permissionButtonEditPrice.Click += (r, e) => act("EditPrice");
                this.actionsStack.Children.Add(permissionButtonEditPrice);

                Button permissionButtonSupply = new Button();
                permissionButtonSupply.Content = "Supply";
                permissionButtonSupply.Click += (r, e) => act("Supply");
                this.actionsStack.Children.Add(permissionButtonSupply);
            }
        }

        private void act(string action)
        {
            switch (action)
            {
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

        private void RemoveOwner()
        {
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

            addToMainGrid(new UIElement[] { productNameLabel, productNameTextBox, manLabel, manTextBox});

            addProduct.Click += (r, e) =>
            {
                controller.RemoveProductFromStore(username, storeName, productNameTextBox.Text, manTextBox.Text);
            };
        }

        private void RemoveManager()
        {
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
            throw new NotImplementedException();
        }

        private void EditPrice()
        {
            
        }

        private void supply()
        {

        }
        

        private void EditManagerPermissions()
        {
            throw new NotImplementedException();
        }

        private void addProduct()
        {
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
    }
}

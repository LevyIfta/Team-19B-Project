using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ClientWeb;

namespace ClientWeb
{
    abstract class ANotifyPropChange : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    class StoreData : ANotifyPropChange
    {
        private String Storename;

        public String storename
        {
            get { return Storename; }
            set
            {
                Storename = value;
                OnPropertyChanged();
            }
        }

        private String Username;

        public String username
        {
            get { return Username; }
            set
            {
                Username = value;
                OnPropertyChanged();
            }
        }
    }

    class ProductDataToAdd : ANotifyPropChange
    {
        private String Pname;

        public String pname
        {
            get { return Pname; }
            set
            {
                Pname = value;
                OnPropertyChanged();
            }
        }

        private String Price;

        public String price
        {
            get { return Price; }
            set
            {
                Price = value;
                OnPropertyChanged();
            }
        }


        private String Amount;

        public String amount
        {
            get { return Amount; }
            set
            {
                Amount = value;
                OnPropertyChanged();
            }
        }


        private String Cat;

        public String cat
        {
            get { return Cat; }
            set
            {
                Cat = value;
                OnPropertyChanged();
            }
        }

        private String Man;

        public String man
        {
            get { return Man; }
            set
            {
                Man = value;
                OnPropertyChanged();
            }
        }
    }

    


    class UserData : ANotifyPropChange
    {
        private String Username;

        public String username
        {
            get { return Username; }
            set
            {
                Username = value;
                OnPropertyChanged();
            }
        }

        private String Password;

        public String password
        {
            get { return Password; }
            set
            {
                Password = value;
                OnPropertyChanged();
            }
        }
        private String Usermsg;

        public String usermsg
        {
            get { return Usermsg; }
            set
            {
                Usermsg = value;
                OnPropertyChanged();
            }
        }


        private String PasswordC;

        public String passwordC
        {
            get { return PasswordC; }
            set
            {
                PasswordC = value;
                OnPropertyChanged();
            }
        }
        private String Age;

        public String age
        {
            get { return Age; }
            set
            {
                Age = value;
                OnPropertyChanged();
            }
        }
        private String Gender;

        public String gender
        {
            get { return Gender; }
            set
            {
                Gender = value;
                OnPropertyChanged();
            }
        }
        private String Address;

        public String address
        {
            get { return Address; }
            set
            {
                Address = value;
                OnPropertyChanged();
            }
        }

        private String Noti;

        public String noti
        {
            get { return Noti; }
            set
            {
                Noti = value;
                OnPropertyChanged();
            }
        }
    }
}

public class PageController
{

    public static string username = "guest";
    public static string storeForManager = "";
    public static string permissionsMenu = "for permission enter the hash code,\ninsert you answer like this: 1#2#3\n1 - AddProduct\n2 - EditManagerPermissions\n3 - EditProduct\n4 - GetInfoEmployees\n5 - GetPurchaseHistory\n6 - HireNewStoreManager\n7 - HireNewStoreOwner\n8 - RemoveManager\n9 - RemoveProduct\n10 - RemoveOwner";
    
    

}

class PurchData : ANotifyPropChange
{
    private String Cradit;

    public String cradit
    {
        get { return Cradit; }
        set
        {
            Cradit = value;
            OnPropertyChanged();
        }
    }
    private String Validity;

    public String validity
    {
        get { return Validity; }
        set
        {
            Validity = value;
            OnPropertyChanged();
        }
    }
    private String Cvv;

    public String cvv
    {
        get { return Cvv; }
        set
        {
            Cvv = value;
            OnPropertyChanged();
        }
    }
    private String Total;

    public String total
    {
        get { return Total; }
        set
        {
            Total = value;
            OnPropertyChanged();
        }
    }
    private String Msg;

    public String msg
    {
        get { return Msg; }
        set
        {
            Msg = value;
            OnPropertyChanged();
        }
    }
}
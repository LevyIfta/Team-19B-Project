using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWeb
{
    class productView : ANotifyPropChange
    {

        private String Name;

        public String name
        {
            get { return Name; }
            set
            {
                Name = value;
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
        private String AmounttoAdd;

        public String amounttoAdd
        {
            get { return AmounttoAdd; }
            set
            {
                AmounttoAdd = value;
                OnPropertyChanged();
            }
        }
        private String StoreName;

        public String storeName
        {
            get { return StoreName; }
            set
            {
                StoreName = value;
                OnPropertyChanged();
            }
        }
        private String Feedback;

        public String feedback
        {
            get { return Feedback; }
            set
            {
                Feedback = value;
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

    }



    class recieptView : ANotifyPropChange
    {

        //// { bool, receipt, receipt }. receipt ->user$store$price$date$id$products. products -> pro1&pro2&pro3 -> proInfo^feedback -> feedback_feedback -> user#comment


        private String ID;

        public String id
        {
            get { return ID; }
            set
            {
                ID = value;
                OnPropertyChanged();
            }
        }


        private String User;

        public String user
        {
            get { return User; }
            set
            {
                User = value;
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

        private String StoreName;

        public String storeName
        {
            get { return StoreName; }
            set
            {
                StoreName = value;
                OnPropertyChanged();
            }
        }
    }

    class employeeView : ANotifyPropChange
    {
        private String Employeename;

        public String employeename
        {
            get { return Employeename; }
            set
            {
                Employeename = value;
                OnPropertyChanged();
            }
        }

        private String Permissions;

        public String permissions
        {
            get { return Permissions; }
            set
            {
                Permissions = value;
                OnPropertyChanged();
            }
        }

    }

    class employeeStore : ANotifyPropChange
    {
        private String Employeename;

        public String employeename
        {
            get { return Employeename; }
            set
            {
                Employeename = value;
                OnPropertyChanged();
            }
        }

        private String Permissions;

        public String permissions
        {
            get { return Permissions; }
            set
            {
                Permissions = value;
                OnPropertyChanged();
            }
        }
        private String Permenu;

        public String permenu
        {
            get { return Permenu; }
            set
            {
                Permenu = value;
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
    class MessageData : ANotifyPropChange
    {
        private String Tosend;

        public String tosend
        {
            get { return Tosend; }
            set
            {
                Tosend = value;
                OnPropertyChanged();
            }
        }

        private String Messagerecive;

        public String messagerecive
        {
            get { return Messagerecive; }
            set
            {
                Messagerecive = value;
                OnPropertyChanged();
            }
        }
        private String Messagesend;

        public String messagesend
        {
            get { return Messagesend; }
            set
            {
                Messagesend = value;
                OnPropertyChanged();
            }
        }
        private String Isnew;

        public String isnew
        {
            get { return Isnew; }
            set
            {
                Isnew = value;
                OnPropertyChanged();
            }
        }


    }


    class ViewModels
    {
    }
}

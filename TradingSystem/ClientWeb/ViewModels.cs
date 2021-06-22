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





    class ViewModels
    {
    }
}

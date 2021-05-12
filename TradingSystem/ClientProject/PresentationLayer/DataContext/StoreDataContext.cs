using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Trial2.PresentationLayer.DataContext
{
    class StoreDataContext : INotifyPropertyChanged
    {
        public string name  { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public StoreDataContext(string name)
        {
            this.name = name;
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}

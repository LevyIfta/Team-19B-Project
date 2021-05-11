using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace WPF_Trial2.PresentationLayer.Windows
{
    abstract class ANotifyPropChange : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
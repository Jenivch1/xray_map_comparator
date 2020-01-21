using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MapComparer.viewmodel
{
    /// <summary>
    /// INotifyPropertyChanged implementation.
    /// Base class for all models.
    /// </summary>
    public abstract class BindableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace EyeSave.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected bool Set<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field,value))
                return false;

            field = value;
            OnProppertyChanged(propertyName);
            return true;
        }

        private void OnProppertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, e: new PropertyChangedEventArgs(propertyName));
        }
    }
}

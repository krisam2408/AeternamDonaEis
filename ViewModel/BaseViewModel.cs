using AeternamDonaEis.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AeternamDonaEis.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName]string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T refField, T value, [CallerMemberName] string propertyName = "")
        {
            if(EqualityComparer<T>.Default.Equals(refField, value))
            {
                return;
            }
            refField = value;
            NotifyPropertyChanged(propertyName);
        }
    }
}

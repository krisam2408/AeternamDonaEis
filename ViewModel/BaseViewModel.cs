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

        #region SetValue
        protected void SetValue(ref GenerateType target, GenerateType value)
        {
            if (target != value) target = value;
            NotifyPropertyChanged("");
        }
        protected void SetValue(ref TextOutput target, TextOutput value)
        {
            if (target != value) target = value;
            NotifyPropertyChanged("");
        }
        protected void SetValue(ref TitleOptions target, TitleOptions value)
        {
            if (target != value) target = value;
            NotifyPropertyChanged("");
        }
        protected void SetValue(ref int target, int value)
        {
            if (target != value) target = value;
            NotifyPropertyChanged("");
        }
        protected void SetValue(ref bool target, bool value)
        {
            if (target != value) target = value;
            NotifyPropertyChanged("");
        }
        protected void SetValue(ref string target, string value)
        {
            if (target != value) target = value;
            NotifyPropertyChanged("");
        }
        protected void SetValue(ref List<GenerateType> target, List<GenerateType> value)
        {
            if (target != value) target = value;
            NotifyPropertyChanged("");
        }
        protected void SetValue(ref List<TextOutput> target, List<TextOutput> value)
        {
            if (target != value) target = value;
            NotifyPropertyChanged("");
        }
        protected void SetValue(ref List<TitleOptions> target, List<TitleOptions> value)
        {
            if (target != value) target = value;
            NotifyPropertyChanged("");
        }
        protected void SetValue(ref Windows.Storage.StorageFile target, Windows.Storage.StorageFile value)
        {
            if (target != value) target = value;
            NotifyPropertyChanged("");
        }
        protected void SetValue(ref ObservableCollection<Windows.Storage.StorageFile> target, ObservableCollection<Windows.Storage.StorageFile> value)
        {
            if (target != value) target = value;
            NotifyPropertyChanged("");
        }
        protected void SetValue(ref Windows.Storage.StorageFolder target, Windows.Storage.StorageFolder value)
        {
            if (target != value) target = value;
            NotifyPropertyChanged("");
        }

        #endregion
    }
}

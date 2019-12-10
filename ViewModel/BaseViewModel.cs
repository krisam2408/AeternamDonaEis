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
            target = value;
            NotifyPropertyChanged("");
        }
        protected void SetValue(ref TextOutput target, TextOutput value)
        {
            target = value;
            NotifyPropertyChanged("");
        }
        protected void SetValue(ref TitleOptions target, TitleOptions value)
        {
            target = value;
            NotifyPropertyChanged("");
        }
        protected void SetValue(ref int target, int value)
        {
            target = value;
            NotifyPropertyChanged("");
        }
        protected void SetValue(ref bool target, bool value)
        {
            target = value;
            NotifyPropertyChanged("");
        }
        protected void SetValue(ref string target, string value)
        {
            target = value;
            NotifyPropertyChanged("");
        }
        protected void SetValue(ref List<GenerateType> target, List<GenerateType> value)
        {
            target = value;
            NotifyPropertyChanged("");
        }
        protected void SetValue(ref List<TextOutput> target, List<TextOutput> value)
        {
            target = value;
            NotifyPropertyChanged("");
        }
        protected void SetValue(ref List<TitleOptions> target, List<TitleOptions> value)
        {
            target = value;
            NotifyPropertyChanged("");
        }
        protected void SetValue(ref Windows.Storage.StorageFile target, Windows.Storage.StorageFile value)
        {
            target = value;
            NotifyPropertyChanged("");
        }
        protected void SetValue(ref ObservableCollection<Windows.Storage.StorageFile> target, ObservableCollection<Windows.Storage.StorageFile> value)
        {
            target = value;
            NotifyPropertyChanged("");
        }

        #endregion
    }
}

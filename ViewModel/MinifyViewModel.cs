using AeternamDonaEis.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace AeternamDonaEis.ViewModel
{
    public class MinifyViewModel:BaseViewModel
    {
        private ObservableCollection<StorageFile> lstFiles;
        public ObservableCollection<StorageFile> LstFiles { get { return lstFiles; } set { SetValue(ref lstFiles, value); } }

        private bool minifyEnabled;
        public bool MinifyEnabled { get { return minifyEnabled; } set { SetValue(ref minifyEnabled, value); } }

        public ICommand PickFileCommand { get { return new RelayCommand(PickFile); } }
        public ICommand MinifyCommand { get { return new RelayCommand(Minify); } }


        public MinifyViewModel()
        {

        }


        private async void PickFile()
        {
            var picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.List,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            picker.FileTypeFilter.Clear();
            picker.FileTypeFilter.Add(".js");
            picker.FileTypeFilter.Add(".css");

            var files = await picker.PickMultipleFilesAsync();

            ObservableCollection<StorageFile> tempList = new ObservableCollection<StorageFile>();

            foreach (StorageFile stf in files) tempList.Add(stf);

            LstFiles = tempList;
        }

        private async void Minify()
        {

        }
    }
}

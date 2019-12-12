using AeternamDonaEis.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Storage.Pickers;

namespace AeternamDonaEis.ViewModel
{
    public class MinifyViewModel:BaseViewModel
    {
        private ObservableCollection<StorageFile> lstFiles;
        public ObservableCollection<StorageFile> LstFiles 
        {
            get
            {
                return lstFiles; 
            } 
            set 
            {
                if(lstFiles != value)
                {
                    SetValue(ref lstFiles, value);
                    MinifyEnabled = true;
                }
            } 
        }
        private StorageFolder folder;
        public StorageFolder Folder
        {
            get
            {
                return folder;
            }
            set
            {
                if(folder != value)
                {
                    SetValue(ref folder, value);
                    MinifyEnabled = true;
                    Path = value != null ? value.Path:string.Empty;
                }
            }
        }

        private string path;
        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                if(path != value)
                {
                    if(path != value) SetValue(ref path, value);
                }
            }
        }

        private bool minifyEnabled;
        public bool MinifyEnabled 
        {
            get
            { 
                return minifyEnabled; 
            }
            set
            {
                bool finalValue = true;
                if (LstFiles == null || LstFiles.Count < 1) finalValue = false;
                if (Folder == null) finalValue = false;
                if(minifyEnabled != finalValue) SetValue(ref minifyEnabled, finalValue); 
            }
        }

        public ICommand PickFileCommand { get { return new RelayCommand(PickFile); } }
        public ICommand PickFolderCommand { get { return new RelayCommand(PickFolder); } }
        public ICommand MinifyCommand { get { return new RelayCommand(MinifyFiles); } }


        public MinifyViewModel()
        {
            MinifyEnabled = true;
        }

        public void RemoveFile(int index)
        {
            if(index > -1 && index < LstFiles.Count)
            {
                LstFiles.RemoveAt(index);
            }
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

        private async void PickFolder()
        {
            var folderPicker = new FolderPicker
            {
                ViewMode = PickerViewMode.List,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            folderPicker.FileTypeFilter.Clear();
            folderPicker.FileTypeFilter.Add("*");

            var folder = await folderPicker.PickSingleFolderAsync();

            Folder = folder;

        }


        private async void MinifyFiles()
        {
            foreach(StorageFile stg in LstFiles)
            {
                string fileName = $"{stg.DisplayName}.min{stg.FileType}";
                // Agregar .min.

                var stream = await stg.OpenAsync(FileAccessMode.Read);
                ulong fileSize = stream.Size;

                string fileText;

                using (var inputStream = stream.GetInputStreamAt(0))
                using (var dataReader = new DataReader(inputStream))
                {
                    uint numBytes = await dataReader.LoadAsync((uint)fileSize);
                    string text = dataReader.ReadString(numBytes);
                    fileText = text;
                }

                fileText = Minify(fileText);

                StorageFile minFile = await Folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(minFile, fileText);

            }
        }

        private string Minify(string fileText)
        {
            string[] targets = new string[] { "\r", "\n", "} ", "{ ", "[ ", "] ", ": ", "  ", "\" ", ", {", ", [", ", \"", "> " };
            for(int i = 0; i < targets.Length; i++)
            {
                while (fileText.Contains(targets[i]))
                {
                    switch (i)
                    {
                        case 0:
                        case 1:
                            fileText = fileText.Replace(targets[i], "");
                            break;
                        default:
                            fileText = fileText.Replace(targets[i], targets[i].Length == 2 ? targets[i][0].ToString() : targets[i][0].ToString() + targets[i][2].ToString());
                            break;
                    }
                }
            }
            return fileText;
        }
    }
}

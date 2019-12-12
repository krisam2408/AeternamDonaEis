using AeternamDonaEis.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AeternamDonaEis.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MinifyPage : Page
    {

        public MinifyViewModel ViewModel { get; set; }

        public MinifyPage()
        {
            this.InitializeComponent();
            ViewModel = Locator.Instance.Minify;
        }

        private async void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            int index = listBox.SelectedIndex;
            var dialog = new MessageDialog("Are you sure you want to remove this file from the query?", "Are you sure?");
            dialog.Commands.Clear();
            dialog.Commands.Add(new UICommand("Yes"));
            dialog.Commands.Add(new UICommand("No"));

            dialog.DefaultCommandIndex = 0;

            var command = await dialog.ShowAsync();

            switch(command.Label)
            {
                case "Yes":
                    Locator.Instance.Minify.RemoveFile(index);
                    break;
                default:
                    break;
            }
        }
    }
}

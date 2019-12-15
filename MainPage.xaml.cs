using AeternamDonaEis.Classes;
using AeternamDonaEis.ViewModel;
using AeternamDonaEis.Views;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AeternamDonaEis
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Locator ViewModel { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = Locator.Instance;
            Navigate(0);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox list = (ListBox)sender;
            int index = list.SelectedIndex;
            Navigate(index);

        }

        private void Navigate(int index)
        {
            switch (index)
            {
                default:
                    WindowFrame.Navigate(typeof(TextGeneratorPage));
                    break;
            }
        }
    }
}

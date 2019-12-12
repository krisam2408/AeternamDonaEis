using AeternamDonaEis.Classes;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AeternamDonaEis.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TextGeneratorPage : Page
    {
        public TextGeneratorViewModel ViewModel { get; set; }

        public TextGeneratorPage()
        {
            this.InitializeComponent();
            ViewModel = Locator.Instance.TextGenerate;
        }


        private void LstTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Locator.Instance.TextGenerate.SelType = (GenerateType)((ListBox)sender).SelectedItem;
        }

        private void LstTitleOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            TitleOptions listValue = (TitleOptions)listBox.SelectedItem;
            Locator.Instance.TextGenerate.SelTitleOption = listValue;
            TitleOptions viewModelValue = Locator.Instance.TextGenerate.SelTitleOption;
            if(listValue != viewModelValue) listBox.SelectedItem = viewModelValue;
        }

        private void LstOutput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Locator.Instance.TextGenerate.SelOutput = (TextOutput)((ListBox)sender).SelectedItem;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (int.TryParse(textBox.Text, out int num)) Locator.Instance.TextGenerate.Quantity = num;
        }
    }
}

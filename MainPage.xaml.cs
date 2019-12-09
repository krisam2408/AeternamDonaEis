﻿using AeternamDonaEis.ViewModel;
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
        public MainViewModel ViewModel { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            MainViewModel.Instance.Generate = new GenerateViewModel();
            this.ViewModel = MainViewModel.Instance;
        }

        private void Increase_Click(object sender, RoutedEventArgs e)
        {
            //MainViewModel.Instance.Generate.IncreaseQuantity();
        }
        private void Decrease_Click(object sender, RoutedEventArgs e)
        {
            ///MainViewModel.Instance.Generate.DecreaseQuantity();
        }

    }
}

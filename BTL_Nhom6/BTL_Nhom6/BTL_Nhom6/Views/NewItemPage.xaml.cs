using net04_btl_nhom1.Models;
using net04_btl_nhom1.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace net04_btl_nhom1.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Chapters Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}
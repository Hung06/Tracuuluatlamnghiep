using BTL_Nhom6.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BTL_Nhom6.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        HomeViewModel _viewModel;
        public HomePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new HomeViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
            ItemsListView.IsVisible = false;
            
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = e.NewTextValue;
            if (!string.IsNullOrEmpty(keyword))
            {
               _viewModel.SearchData(keyword);
                ItemsListView.IsVisible = true;
               
            }
            else
            {
                ItemsListView.IsVisible = false;
                
            }
        }
    }
}
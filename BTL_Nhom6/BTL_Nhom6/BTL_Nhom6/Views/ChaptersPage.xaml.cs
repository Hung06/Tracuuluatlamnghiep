using BTL_Nhom6.Models;
using BTL_Nhom6.ViewModels;
using BTL_Nhom6.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BTL_Nhom6.Views
{
    public partial class ChaptersPage : ContentPage
    {
        ChaptersViewModel _viewModel;

        public ChaptersPage(int user)
        {
            InitializeComponent();

            BindingContext = _viewModel = new ChaptersViewModel();
            if (_viewModel != null)
            {
                _viewModel.UserId = user;
            }
        }

        public ChaptersPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ChaptersViewModel();
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
using BTL_Nhom6.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace BTL_Nhom6.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccountDetailPage : ContentPage
	{
		AccountDetailViewModel _viewModel;
		public AccountDetailPage ()
		{
			InitializeComponent ();
			BindingContext = _viewModel = new AccountDetailViewModel ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
            
        }
    }
}
using BTL_Nhom6.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BTL_Nhom6.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccountPage : ContentPage
	{
		AccountViewModel _viewModel;
		public AccountPage ()
		{
			InitializeComponent ();
			BindingContext = _viewModel = new AccountViewModel ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
			if (Preferences.Get("UserId", 0) == 0)
			{
                ItemsListView.IsVisible = false;
				Buttons.IsVisible = false;

				// 3 cái này là khi người dùng có id = 0
				AccountName.IsVisible = true;
				AccountDescription.IsVisible = true;
				LoginButton.IsVisible = true;
			}

			if (Preferences.Get("UserId", 0) != 0)
			{
                ItemsListView.IsVisible = true;
                Buttons.IsVisible = true;

				//
                AccountName.IsVisible = false;
                AccountDescription.IsVisible = false;
                LoginButton.IsVisible = false;
            }
        }
    }
}
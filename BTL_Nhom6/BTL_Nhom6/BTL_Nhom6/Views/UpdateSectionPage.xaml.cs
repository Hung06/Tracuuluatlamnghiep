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
	public partial class UpdateSectionPage : ContentPage
	{
		UpdateSectionViewModel _viewModel;
		public UpdateSectionPage ()
		{
			InitializeComponent ();
			BindingContext = _viewModel = new UpdateSectionViewModel ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
			_viewModel.OnAppearing ();

			if (Preferences.Get("UserId",0) == 0)
			{
				DeleteButton.IsVisible = false;
				Buttons.IsVisible = false;
				ContentLabel.Text = "Nội dung";
			}

            if (Preferences.Get("UserId", 0) != 0)
            {
                DeleteButton.IsVisible = true;
                Buttons.IsVisible = true;
            }
        }
    }
}
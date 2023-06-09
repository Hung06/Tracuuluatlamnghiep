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
	public partial class ArticlesDetailPage : ContentPage
	{
		ArticlesDetailViewModel _viewModel;
		public ArticlesDetailPage ()
		{
			InitializeComponent ();
			BindingContext = _viewModel = new ArticlesDetailViewModel ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
            if (Preferences.Get("UserId", 0) == 0) //kiểm tra xem người dùng admin đã đăng nhập hay chưa. 
            {
                Editing.IsVisible = false;
                DeleteFrame.IsVisible = false;
            }
            if (Preferences.Get("UserId", 0) != 0)
            {
                Editing.IsVisible = true;
                DeleteFrame.IsVisible = true;
            }
        }
    }
}

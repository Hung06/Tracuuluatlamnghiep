using BTL_Nhom6.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BTL_Nhom6.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UpdateChapterPage : ContentPage
	{
		UpdateChapterViewModel _viewModel;
		public UpdateChapterPage ()
		{
			InitializeComponent ();
			BindingContext = _viewModel = new UpdateChapterViewModel ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
			_viewModel.OnAppearing ();
        }
    }

}
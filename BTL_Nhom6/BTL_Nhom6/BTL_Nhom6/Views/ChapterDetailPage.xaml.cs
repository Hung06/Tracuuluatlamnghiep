using BTL_Nhom6.ViewModels;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BTL_Nhom6.Views
{
    public partial class ChapterDetailPage : ContentPage
    {
        ChapterDetailViewModel _viewModel;
        public ChapterDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ChapterDetailViewModel(); //BindingContext là một thuộc tính của trang (page) đặt ngữ cảnh gắn kết (binding context) 
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
            if (Preferences.Get("UserId", 0) == 0)
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

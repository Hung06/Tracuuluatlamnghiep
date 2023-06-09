using BTL_Nhom6.ViewModels;
using BTL_Nhom6.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BTL_Nhom6
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(ChapterDetailPage), typeof(ChapterDetailPage));
            Routing.RegisterRoute(nameof(ArticlesDetailPage), typeof(ArticlesDetailPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(ChaptersPage), typeof(ChaptersPage));
            Routing.RegisterRoute(nameof(ArticlesPage), typeof(ArticlesPage));
            Routing.RegisterRoute(nameof(SectionsPage), typeof(SectionsPage));
            Routing.RegisterRoute(nameof(UpdateChapterPage), typeof(UpdateChapterPage));
            Routing.RegisterRoute(nameof(UpdateArticlePage), typeof(UpdateArticlePage));
            Routing.RegisterRoute(nameof(UpdateSectionPage), typeof(UpdateSectionPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}

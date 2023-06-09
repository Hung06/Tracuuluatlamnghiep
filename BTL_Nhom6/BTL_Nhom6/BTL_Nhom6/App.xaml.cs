using BTL_Nhom6.Database;
using BTL_Nhom6.Services;
using BTL_Nhom6.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BTL_Nhom6
{
    public partial class App : Application
    {

        private static DatabaseService db;

        public static DatabaseService DB
        {
            get {
                if (db == null)
                {
                    db = new DatabaseService();
                }
                return db;
            }
            
        }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockChapters>();
            DependencyService.Register<MockArticles>();
            DependencyService.Register<MockUsers>();
            DependencyService.Register<MockSections>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

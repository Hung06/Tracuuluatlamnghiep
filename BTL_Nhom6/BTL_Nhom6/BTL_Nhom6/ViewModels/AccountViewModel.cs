using BTL_Nhom6.Models;
using BTL_Nhom6.Services;
using BTL_Nhom6.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BTL_Nhom6.ViewModels
{
    public class AccountViewModel : BaseViewModel
    {
        private Users _selectedItem;
        public int _itemId;
        public string title;
        public ObservableCollection<Users> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command EditItemCommand { get; }
        public Command LogoutCommand { get; }
        public Command<Users> ItemTapped { get; }

        public Command LoginCommand { get; }
        public Command ChangePassword { get; }
        private bool isItemsLoaded;

        public AccountViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Users>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Users>(OnItemSelected);
            ChangePassword = new Command(OnChangePassword);
            LogoutCommand = new Command(OnLogout);
            //EditItemCommand = new Command(OnEditItem);
            LoginCommand = new Command(OnLogin);
        }


        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                LoadItemId(Preferences.Get("UserId", 0));
            }
            finally
            {
                IsBusy = false;
            }
        }
        public async void LoadItemId(int itemId)
        {
            try
            {
                Items.Clear();
                var item = await UsersData.GetUserAsync(itemId);
                Items.Add(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Users SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        public async void OnLogin()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        //private async void OnEditItem(object obj)
        //{
        //    await Shell.Current.GoToAsync(nameof(LoginPage));
        //}

        async void OnItemSelected(Users item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(AccountDetailPage)}?{nameof(AccountDetailViewModel.ItemId)}={item.Id}");
        }

        public async void OnLogout()
        {
            bool logoutConfirm = await App.Current.MainPage.DisplayAlert("Đăng xuất", "Bạn có chắc chắn muốn đăng xuất?", "OK", "Hủy");
            if (logoutConfirm)
            {
                Preferences.Set("UserId", 0);
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
        }

        public async void OnChangePassword()
        {
            await Shell.Current.GoToAsync($"{nameof(ChangePasswordPage)}?{nameof(ChangePasswordViewModel.ItemId)}={Preferences.Get("UserId",0)}");
        }
    }
}


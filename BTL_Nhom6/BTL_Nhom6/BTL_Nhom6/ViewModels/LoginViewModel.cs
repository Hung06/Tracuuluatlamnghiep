using BTL_Nhom6.Database;
using BTL_Nhom6.Models;
using BTL_Nhom6.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BTL_Nhom6.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        
        public Command LoginCommand { get; }

        public Command BackCommand { get; }

        public Command LoginAsGuest { get; }

        public Command RegisterCommand { get; }

        public Users User { get; set; }
        private INavigation Navigation { get; set; }

        private string userName;

        private string passWord;
       
        public LoginViewModel()
        {
            
            User = new Users();
            LoginCommand = new Command(OnLoginClicked);
            BackCommand = new Command(OnBackClicked);
            LoginAsGuest = new Command(OnLoginAsGuest);
            RegisterCommand = new Command(OnRegister);
        }

        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public string PassWord
        {
            get => passWord;
            set => SetProperty(ref passWord, value);
        }

        public async void OnAppearing()
        {
            bool IsLoggedIn = false;
            Preferences.Set("IsLoggedIn", IsLoggedIn);
            if (IsLoggedIn)
            {
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            }
        }

        private async void OnBackClicked(object obj)
        {
            
        }

        public async void OnRegister()
        {
            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
        }

        private async void OnLoginAsGuest()
        {
            Preferences.Set("UserId", 0);
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
        private async void OnLoginClicked(object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(PassWord)) {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng điền đầy đủ thông tin!", "OK");
                } else
                {
                    var data = await UsersData.Login(UserName, PassWord);
                    if (data != null)
                    {
                        //Debug.WriteLine(data.ToString());
                        Preferences.Set("UserId", data.Id);
                        await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Thông báo", "Tên tài khoản hoặc mật khẩu không đúng", "OK");
                    }
                 }
                
            } catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Có lỗi hệ thống", "OK");
            }
        }
    }
}

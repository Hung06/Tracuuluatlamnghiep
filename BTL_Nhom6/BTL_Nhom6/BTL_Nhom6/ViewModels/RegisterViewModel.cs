using BTL_Nhom6.Models;
using BTL_Nhom6.Views;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BTL_Nhom6.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public Command RegisterCommand { get; }

        public Users User { get; set; }

        private string userName;

        private string passWord;

        private string rePassWord;

        private string email;

        private string mobile;

        public RegisterViewModel()
        {

            User = new Users();

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

        public string RePassWord
        {
            get => rePassWord;
            set => SetProperty(ref rePassWord, value);
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public string Mobile
        {
            get => mobile;
            set => SetProperty(ref mobile, value);
        }

        public async void OnAppearing()
        {

        }

        public async void OnRegister()
        {
            try
            {
                if (string.IsNullOrEmpty(UserName) || 
                    string.IsNullOrEmpty(PassWord) || 
                    string.IsNullOrEmpty(RePassWord) || 
                    string.IsNullOrEmpty(Email) || 
                    string.IsNullOrEmpty(Mobile))
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng điền đầy đủ thông tin!", "OK");
                    return;
                }
                if (!PassWord.Equals(RePassWord))
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Mật khẩu không trùng khớp", "OK");
                    return;
                }
                else
                {
                    Users data = new Users();
                    data.UserName = UserName;
                    data.PassWord = PassWord;
                    data.Email = Email;
                    data.Mobile = int.Parse(Mobile);
                    data.Role = "admin";
                    await UsersData.Register(data);
                    await App.Current.MainPage.DisplayAlert("Đăng ký thành công!", "Bạn đã đăng ký thành công tài khoản quản trị viên! Vui lòng đăng nhập để tiếp tục sử dụng ứng dụng", "OK");
                    await Shell.Current.GoToAsync("..");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Có lỗi hệ thống", "OK");
            }
        }

    }
}

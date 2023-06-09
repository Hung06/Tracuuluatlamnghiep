using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BTL_Nhom6.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ChangePasswordViewModel : BaseViewModel
    {
        private int itemId;
        private string password;
        private string oldPassword;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public ChangePasswordViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        public string OldPassword
        {
            get => oldPassword;
            set => SetProperty(ref oldPassword, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public int ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;

            }
        }

        public async void OnSave()
        {
            try
            {
                if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(OldPassword))
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng điền đầy đủ thông tin", "OK");
                    return;
                }

                if (OldPassword.Equals(Password)) {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Mật khẩu mới trùng với mật khẩu cũ!", "OK");
                    return;
                }
                else
                {
                    var item = await UsersData.GetUserAsync(ItemId);
                    if (OldPassword != item.PassWord)
                    {
                        await App.Current.MainPage.DisplayAlert("Thông báo", "Mật khẩu cũ không đúng!", "OK");
                        return;
                    }
                    bool onConfirm = await App.Current.MainPage.DisplayAlert("Thay đổi mật khẩu", "Bạn có chắc chắn muốn thay đổi mật khẩu?", "Lưu", "Tiếp tục chỉnh sửa");
                    if (onConfirm)
                    {
                        
                        if (item != null)
                        {     
                            item.PassWord = Password;
                            await UsersData.UpdateUserAsync(item);
                            await Shell.Current.GoToAsync("..");
                        }
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
        }

        public async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}

using BTL_Nhom6.Database;
using BTL_Nhom6.Models;
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
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class AccountDetailViewModel : BaseViewModel
    {
        private int itemId;
        private string userName;
        private string email;
        private string mobile;

        public ObservableCollection<Users> Items { get; }
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

       
        
        public AccountDetailViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Users>();

            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            
        }

        public void OnAppearing()
        {
            IsBusy = true;
            
            LoadItemId(ItemId);
        }

        public int Id { get; set; }

        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
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

        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await UsersData.GetUserAsync(itemId);
                UserName = item.UserName;
                Email = item.Email;
                Mobile = item.Mobile.ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async void OnSave()
        {
            try
            {
                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Mobile))
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng điền đầy đủ thông tin", "OK");
                }
                else
                {
                    bool onConfirm = await App.Current.MainPage.DisplayAlert("Thay đổi thông tin", "Bạn có chắc chắn muốn thay đổi thông tin?", "Lưu", "Tiếp tục chỉnh sửa");
                    if(onConfirm)
                    {
                        var item = await UsersData.GetUserAsync(ItemId);
                        if (item != null)
                        {
                            item.UserName = UserName;
                            item.Email = Email;
                            item.Mobile = int.Parse(Mobile);
                            await UsersData.UpdateUserAsync(item);
                            await Shell.Current.GoToAsync("..");
                        }
                    }
                }
            } catch (Exception ex) { Debug.WriteLine(ex);}
        }

        public async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}


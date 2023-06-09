using BTL_Nhom6.Database;
using BTL_Nhom6.Models;
using BTL_Nhom6.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BTL_Nhom6.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class UpdateChapterViewModel : BaseViewModel
    {
        private int itemId;
        private string title;
        private string content;
        private string createTime;
        private string updateTime;
        private int decree;

        private Chapters _selectedItem;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public ObservableCollection<Chapters> Items { get; }
        
        
        public UpdateChapterViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Chapters>();
            
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
        }

        
        public void OnAppearing()
        {
            IsBusy = true;
        }

        public int Id { get; set; }

        public string ChapterTitle
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }

        public string CreateTime
        {
            get => createTime;
            set => SetProperty(ref createTime, value);
        }

        public string UpdateTime
        {
            get => updateTime;
            set => SetProperty(ref updateTime, value);
        }

        public int Decree
        {
            get
            {
                return decree;
            }
            set
            {
                SetProperty(ref decree, value);
            }
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
                LoadItemId(value);
            }
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                Items.Clear();
                var item = await ChaptersData.GetItemAsync(itemId);
                Items.Add(item);
                ChapterTitle = item.Title;
                Content = item.Content;
                Decree = item.Decree;
                CreateTime = item.CreateTime;
                UpdateTime = item.UpdateTime;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async void OnSave()
        {
            if (string.IsNullOrEmpty(ChapterTitle) && 
                string.IsNullOrEmpty(Content) && 
                string.IsNullOrEmpty(CreateTime) && 
                string.IsNullOrEmpty(UpdateTime) && 
                string.IsNullOrEmpty(Decree.ToString())) {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng điền đầy đủ các mục", "OK");
                return;
            }

            var item = await ChaptersData.GetItemAsync(itemId);
            if (item != null)
            {
                item.Title = ChapterTitle;
                item.Content = Content;
                item.Decree = Decree;
                item.CreateTime = CreateTime;
                item.UpdateTime = UpdateTime;
                bool onConfirm = await App.Current.MainPage.DisplayAlert("Thay đổi thông tin", "Bạn có chắc chắn muốn thay đổi thông tin?", "Lưu", "Tiếp tục chỉnh sửa");
                if (onConfirm)
                {
                    await ChaptersData.UpdateItemAsync(item);
                    await Shell.Current.GoToAsync("..");
                }
            }
        }

        public async void OnCancel()
        {
            await Shell.Current.GoToAsync(".."); 
        }
    }
}

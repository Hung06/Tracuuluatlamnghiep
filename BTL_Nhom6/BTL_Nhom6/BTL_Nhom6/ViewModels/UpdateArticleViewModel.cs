using BTL_Nhom6.Models;
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
    public class UpdateArticleViewModel : BaseViewModel
    {
        private int itemId;
        private string title;
        private string content;
        private string createTime;
        private string updateTime;
        private int chapterId;

        private Articles _selectedItem;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public ObservableCollection<Articles> Items { get; }
        public Command LoadItemsCommand { get; }

        public UpdateArticleViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Articles>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {

            }

            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        public int Id { get; set; }

        public string ArticleTitle
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

        public int ChapterId
        {
            get
            {
                return chapterId;
            }
            set
            {
                SetProperty(ref chapterId, value);
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
                var item = await ArticlesData.GetItemAsync(itemId);
                Items.Add(item);
                ArticleTitle = item.Title;
                Content = item.Content;
                ChapterId = item.ChapterId;
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
            if (string.IsNullOrEmpty(ArticleTitle) && string.IsNullOrEmpty(Content) && string.IsNullOrEmpty(CreateTime) && string.IsNullOrEmpty(UpdateTime) && string.IsNullOrEmpty(ChapterId.ToString()))
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng điền đầy đủ các mục", "OK");
            }

            var item = await ArticlesData.GetItemAsync(itemId);
            if (item != null)
            {
                item.Title = ArticleTitle;
                item.Content = Content;
                item.ChapterId = ChapterId;
                item.CreateTime = CreateTime;
                item.UpdateTime = UpdateTime;
                bool onConfirm = await App.Current.MainPage.DisplayAlert("Thay đổi thông tin", "Bạn có chắc chắn muốn thay đổi thông tin?", "Lưu", "Tiếp tục chỉnh sửa");
                if (onConfirm)
                {
                    await ArticlesData.UpdateItemAsync(item);
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

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
    public class UpdateSectionViewModel : BaseViewModel
    {
        private int itemId;
        private string title;
        private string content;
        private string createTime;
        private string updateTime;
        private int articleId;
        private int decreeId;
        private string min;
        private string max;
        private string avg;

        private Sections _selectedItem;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command DeleteCommand { get; }
        public ObservableCollection<Sections> Items { get; }
        public Command LoadItemsCommand { get; }

        public UpdateSectionViewModel() 
        {
            Title = "Browse";
            Items = new ObservableCollection<Sections>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            DeleteCommand = new Command(OnDelete);
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

        public string SectionTitle
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

        public int DecreeId
        {
            get
            {
                return decreeId;
            }
            set
            {
                SetProperty(ref decreeId, value);
            }
        }

        public int ArticleId
        {
            get => articleId;
            set => SetProperty(ref articleId, value);
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

        public string Min
        {
            get => min;
            set => SetProperty(ref min, value);
        }

        public string Max
        {
            get => max;
            set => SetProperty(ref max, value);
        }

        public string Avg
        {
            get => avg;
            set => SetProperty(ref avg, value);
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                Items.Clear();
                var item = await SectionsData.GetItemAsync(itemId);
                Items.Add(item);
                
                Content = item.Content;
                DecreeId = item.DecreeId;
                ArticleId = item.ArticleId;
                CreateTime = item.CreateTime;
                UpdateTime = item.UpdateTime;
                Min = item.Min;
                Max = item.Max;
                Avg = item.Avg;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async void OnSave()
        {
            if (string.IsNullOrEmpty(Content) && 
                string.IsNullOrEmpty(CreateTime) && 
                string.IsNullOrEmpty(UpdateTime) && 
                string.IsNullOrEmpty(DecreeId.ToString()) &&
                string.IsNullOrEmpty(ArticleId.ToString()) &&
                string.IsNullOrEmpty(Min) && 
                string.IsNullOrEmpty(Max) && 
                string.IsNullOrEmpty(Avg))
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng điền đầy đủ các mục", "OK");
            }

            var item = await SectionsData.GetItemAsync(itemId);
            if (item != null)
            {
                item.Content = Content;
                item.DecreeId = DecreeId;
                item.ArticleId = ArticleId;
                item.CreateTime = CreateTime;
                item.UpdateTime = UpdateTime;
                item.Min = Min;
                item.Max = Max;
                item.Avg = Avg;
                bool onConfirm = await App.Current.MainPage.DisplayAlert("Thay đổi thông tin", "Bạn có chắc chắn muốn thay đổi thông tin?", "Lưu", "Tiếp tục chỉnh sửa");
                if (onConfirm)
                {
                    await SectionsData.UpdateItemAsync(item);
                    await Shell.Current.GoToAsync("..");
                }
            }
        }

        public async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        public async void OnDelete()
        {
            bool onConfirm = await App.Current.MainPage.DisplayAlert("Xóa khoản", "Bạn có chắc chắn muốn xóa khoản này?", "Có", "Không");
            if (onConfirm)
            {
                var item = await SectionsData.GetItemAsync(ItemId);
                if (item != null)
                {
                    await SectionsData.DeleteItemAsync(item);
                }
                await Shell.Current.GoToAsync("..");
            }
        }
    }

}

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
    public class ArticlesDetailViewModel : BaseViewModel
    {
        private int itemId;
        private string title;
        private string content;
        private string createTime;
        private string updateTime;
        private int decree;
        private Articles _selectedItem;
        private Users _user;
        private DatabaseService database;
        public ObservableCollection<Articles> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command EditItemCommand { get; }
        public Command DeleteCommand { get; }
        public Command<Articles> ItemTapped { get; }
        public ArticlesDetailViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Articles>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Articles>(OnItemSelected);

            EditItemCommand = new Command(OnEditItem);
            DeleteCommand = new Command(OnDeleteItem);
            database = new DatabaseService();
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                LoadItemId(ItemId);
            }

            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public int Id { get; set; }

        public string Text
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

            }
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                Items.Clear();
                var item = await ArticlesData.GetItemAsync(itemId);
                Items.Add(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public Articles SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnEditItem(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(UpdateArticlePage)}?{nameof(UpdateArticleViewModel.ItemId)}={itemId}");
        }

        async void OnItemSelected(Articles item)
        {
            if (item == null)
            {
                return;
            }
            //await Shell.Current.GoToAsync("..");
            await Shell.Current.GoToAsync($"{nameof(SectionsPage)}?{nameof(SectionsViewModel.ItemId)}={itemId}");
        }

        private async void OnDeleteItem()
        {
            bool onConfirm = await App.Current.MainPage.DisplayAlert("Xóa điều khoản", "Bạn có chắc chắn muốn xóa điều khoản?", "Có", "Không");
            if (onConfirm)
            {
                var item = await ArticlesData.GetItemAsync(ItemId);
                if (item != null)
                {
                    await ArticlesData.DeleteItemAsync(item);
                }

                await Shell.Current.GoToAsync("..");
            }
        }
    }
}

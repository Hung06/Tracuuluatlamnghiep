using BTL_Nhom6.Database;
using BTL_Nhom6.Models;
using BTL_Nhom6.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BTL_Nhom6.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ChapterDetailViewModel : BaseViewModel
    {
        private int itemId;
        private string title;
        private string content;
        private string createTime;
        private string updateTime;
        private int decree;
        private Chapters _selectedItem;
        private Users _user;
        private DatabaseService database;
        public ObservableCollection<Chapters> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command EditItemCommand { get; }
        public Command DeleteCommand { get; }
        public Command<Chapters> ItemTapped { get; }
        public ChapterDetailViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Chapters>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Chapters>(OnItemSelected);
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
            set => SetProperty(ref  updateTime, value);
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
                var item = await ChaptersData.GetItemAsync(itemId);
                Items.Add(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public Chapters SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnEditItem()
        {
            await Shell.Current.GoToAsync($"{nameof(UpdateChapterPage)}?{nameof(UpdateChapterViewModel.ItemId)}={itemId}");
        }

        async void OnItemSelected(Chapters item)
        {
            if (item == null)
            {
                return;
            }
            //await Shell.Current.GoToAsync("..");
            await Shell.Current.GoToAsync($"{nameof(ArticlesPage)}?{nameof(ArticlesViewModel.ItemId)}={itemId}");
        }

        private async void OnDeleteItem()
        {
            bool onConfirm = await App.Current.MainPage.DisplayAlert("Xóa chương mục", "Bạn có chắc chắn muốn xóa chương mục?", "Có", "Không");

            if (onConfirm)
            {
                var item = await ChaptersData.GetItemAsync(ItemId);
                if (item != null)
                {
                    await ChaptersData.DeleteItemAsync(item);

                }
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}


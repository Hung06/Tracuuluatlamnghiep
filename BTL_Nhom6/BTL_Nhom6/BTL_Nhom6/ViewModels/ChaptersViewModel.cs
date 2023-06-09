using BTL_Nhom6.Database;
using BTL_Nhom6.Models;
using BTL_Nhom6.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BTL_Nhom6.ViewModels
{
    [QueryProperty(nameof(UserId), nameof(UserId))]
    public class ChaptersViewModel : BaseViewModel
    {
        private Chapters _selectedItem;
        private int _user;
        private DatabaseService database;
        public ObservableCollection<Chapters> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command EditItemCommand { get; }
        public Command<Chapters> ItemTapped { get; }
        public ArticlesViewModel articles;

        private int _selectedIndex;

        public int UserId
        {
            get => _user;
            set
            {
                _user = value;                
            }
        }

        public ChaptersViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Chapters>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Chapters>(OnItemSelected);

            EditItemCommand = new Command(async() => await OnEditItem());
            database = new DatabaseService();
            articles = new ArticlesViewModel();
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await ChaptersData.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
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

        public Chapters SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }


        

        async Task OnEditItem()
        {
            try
            {
                
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            
        }

        async void OnItemSelected(Chapters item)
        {
            if (item == null)
                return;
            //Preferences.Set("Count", )
            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ChapterDetailPage)}?{nameof(ChapterDetailViewModel.ItemId)}={item.Id}");
        }
    }
}
using BTL_Nhom6.Models;
using BTL_Nhom6.Services;
using BTL_Nhom6.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static System.Collections.Specialized.BitVector32;

namespace BTL_Nhom6.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class SectionsViewModel : BaseViewModel
    {
        private Sections _selectedItem;
        public int _itemId;
        public string title;
        public ObservableCollection<Sections> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command EditItemCommand { get; }
        
        public Command<Sections> ItemTapped { get; }
        
        private bool isItemsLoaded;

        public SectionsViewModel() 
        {
            Title = "Browse";
            Items = new ObservableCollection<Sections>();
            
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Sections>(OnItemSelected);
            
            EditItemCommand = new Command(OnEditItem);
        }

        public int ItemId
        {
            get { return _itemId; }
            set
            {
                _itemId = value;

            }
        }

        public string Text
        {
            get => title;
            set => SetProperty(ref title, value);
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
        public async void LoadItemId(int itemId)
        {
            try
            {
                Items.Clear();
                var items = await SectionsData.GetItemsAsync(itemId, true);
                foreach (var item in items)
                {
                    Items.Add(item);
                    Text = item.Title;
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
            isItemsLoaded = false;
        }

        
        public Sections SelectedItem
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
            await Shell.Current.GoToAsync($"{nameof(UpdateSectionPage)}?{nameof(UpdateSectionViewModel.ItemId)}={ItemId}");
        }

        async void OnItemSelected(Sections item)
        {
            if (Preferences.Get("UserId", 0) == 0)
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Bạn phải là quản trị viên để có quyền chỉnh sửa", "OK");
            }
            if (Preferences.Get("UserId", 0) != 0)
            {
                if (item == null)
                    return;
                await Shell.Current.GoToAsync($"{nameof(UpdateSectionPage)}?{nameof(UpdateSectionViewModel.ItemId)}={item.Id}");
            }
            
        }
    }
}

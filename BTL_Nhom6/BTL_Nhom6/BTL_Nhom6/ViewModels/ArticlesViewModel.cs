using BTL_Nhom6.Models;
using BTL_Nhom6.Services;
using BTL_Nhom6.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace BTL_Nhom6.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ArticlesViewModel : BaseViewModel
    {
        private Articles _selectedItem;
        public int _itemId;
        public ObservableCollection<Articles> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command EditItemCommand { get; }
        public Command<Articles> ItemTapped { get; }

        public ArticlesViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Articles>();
           
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
           
        

            ItemTapped = new Command<Articles>(OnItemSelected);

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
                var items = await ArticlesData.GetItemsAsync(itemId, true);
                foreach (var item in items)
                {
                    Items.Add(item);
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
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }

    async void OnItemSelected(Articles item)
    {
        if (item == null)
            return;
        
        await Shell.Current.GoToAsync($"{nameof(ArticlesDetailPage)}?{nameof(ArticlesDetailViewModel.ItemId)}={item.Id}");
    }
    }
}

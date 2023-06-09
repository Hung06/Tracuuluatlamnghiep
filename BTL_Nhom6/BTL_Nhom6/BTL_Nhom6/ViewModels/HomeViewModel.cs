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

namespace BTL_Nhom6.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private Sections _selectedItem;
        public int _itemId;
        public string title;
        public string userName;
        public string sectionTitle;
        public string sectionContent;
        public string min;
        public string max;
        public string avg;
        public ObservableCollection<Sections> Items { get; }
        public ObservableCollection<Sections> RecommendItems { get; }
        public Command EditItemCommand { get; }

        public Command<Sections> ItemTapped { get; }

        private bool isItemsLoaded;

        public HomeViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Sections>();
            ItemTapped = new Command<Sections>(OnItemSelected);
            RecommendItems = new ObservableCollection<Sections>();
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

        public string SectionTitle
        {
            get => sectionTitle;
            set => SetProperty(ref sectionTitle, value);
        }

        public string SectionContent
        {
            get => sectionContent;
            set => SetProperty(ref sectionContent, value);
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

        public string Text
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
            isItemsLoaded = false;
            LoadUserName();
            LoadSectionsTitle();
        }

        public async void LoadSectionsTitle()
        {
            var item = await SectionsData.GetItemAsync(1);
            if (item != null)
            {
                SectionTitle = item.Title;
                SectionContent = item.Content;
                Min = item.Min;
                Max = item.Max;
                Avg = item.Avg;
            }
        }

        public async void LoadUserName()
        {
            var item = await UsersData.GetUserAsync(Preferences.Get("UserId", 0));
            if (item != null)
            {
                UserName = item.UserName;
            }
            else
            {
                UserName = "Khách";
            }
            
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
            if (item == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(UpdateSectionPage)}?{nameof(UpdateSectionViewModel.ItemId)}={item.Id}");
        }
        public async void SearchData(string keyword)
        {
            try
            {
                Items.Clear ();
                var items = await SectionsData.SearchItemsAsync(keyword);
                foreach (var item in items)
                {
                    Items.Add(item);
                    Text = item.Title;
                }
            }
            catch(Exception ex) { 
                Debug.WriteLine(ex.ToString());
            }
        }

        public async void LoadRecommendItems()
        {
            try
            {
                Items.Clear ();
                var items = await SectionsData.GetItemsAsync(3);
                foreach(var item in items)
                {
                    Items.Add(item);
                }
                
            } catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }
    } 
}

using BTL_Nhom6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace BTL_Nhom6.Services
{
    public class MockSections : ISections<Sections> 
    {
        readonly List<Sections> items;

        public MockSections()
        {
            items = new List<Sections>();
        }

        public async Task<bool> AddItemAsync(Sections item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<int> UpdateItemAsync(Sections item)
        {
            return await App.DB.UpdateSectionAsync(item);
        }

        public async Task<int> DeleteItemAsync(Sections item)
        {
            return await App.DB.DeleteSectionAsync(item);
        }

        public async Task<Sections> GetItemAsync(int id)
        {
            return await App.DB.GetSectionAsync(id);
        }

        public async Task<IEnumerable<Sections>> GetItemsAsync(int itemId, bool forceRefresh = false)
        {
            return await App.DB.GetSectionsAsync(itemId);
        }
        
        public async Task<IEnumerable<Sections>> SearchItemsAsync(string keyword)
        {
            return await App.DB.SearchSectionsAsync(keyword);
        }
    }
}


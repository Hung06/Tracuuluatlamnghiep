using BTL_Nhom6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTL_Nhom6.Services
{
    public class MockChapters : IChapters<Chapters>
    {
        readonly List<Chapters> items;

        public MockChapters()
        {
            items = new List<Chapters>();
        }

        public async Task<bool> AddItemAsync(Chapters item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<int> UpdateItemAsync(Chapters item)
        {
            return await App.DB.UpdateChapterAsync(item);
        }

        public async Task<int> DeleteItemAsync(Chapters item)
        {
            return await App.DB.DeleteChapterAsync(item);
        }

        public async Task<Chapters> GetItemAsync(int id)
        {
            return await App.DB.GetChapterAsync(id);
        }

        public async Task<IEnumerable<Chapters>> GetItemsAsync(bool forceRefresh = false)
        {
            return await App.DB.GetChaptersAsync();
        }

        public async Task<IEnumerable<Chapters>> SearchChapterAsync(string keyword)
        {
            return await App.DB.SearchChapterAsync(keyword);
        }
    }
}
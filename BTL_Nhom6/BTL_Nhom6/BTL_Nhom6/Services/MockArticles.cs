using BTL_Nhom6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_Nhom6.Services
{
    public class MockArticles : IArticles<Articles>
    {
        readonly List<Articles> items;

        public MockArticles()
        {
            items = new List<Articles>();
        }

        public async Task<bool> AddItemAsync(Articles item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<int> UpdateItemAsync(Articles item)
        {
            return await App.DB.UpdateArticleAsync(item);
        }

        public async Task<int> DeleteItemAsync(Articles item)
        {
            return await App.DB.DeleteArticleAsync(item);
        }

        public async Task<Articles> GetItemAsync(int id)
        {
            return await App.DB.GetArticleAsync(id);
        }

        public async Task<IEnumerable<Articles>> GetItemsAsync(int itemId, bool forceRefresh = false)
        {
            return await App.DB.GetArticlesAsync(itemId);
        }
    }
}

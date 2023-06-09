using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BTL_Nhom6.Services
{
    public interface IChapters<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<int> UpdateItemAsync(T item);
        Task<int> DeleteItemAsync(T item);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> SearchChapterAsync(string keyword);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BTL_Nhom6.Services
{
    public interface ISections<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<int> UpdateItemAsync(T item);
        Task<int> DeleteItemAsync(T item);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> SearchItemsAsync(string keyword);
        Task<IEnumerable<T>> GetItemsAsync(int itemId, bool forceRefresh = false);
    }
}

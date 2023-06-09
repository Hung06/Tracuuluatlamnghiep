using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BTL_Nhom6.Services
{
    public interface IUsers<T>
    {
        Task<bool> AddUserAsync(T item);
        Task<int> UpdateUserAsync(T item);
        Task<bool> DeleteUserAsync(int id);
        Task<T> GetUserAsync(int id);
        Task<IEnumerable<T>> GetUsersAsync(bool forceRefresh = false);
        Task<T> Login(string userName, string passWord);
        Task<int> Register(T item);
    }
}

using BTL_Nhom6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTL_Nhom6.Services
{
    public class MockUsers : IUsers<Users>
    {
        readonly List<Users> users;

        public MockUsers()
        {
            users = new List<Users>();
        }

        public async Task<bool> AddUserAsync(Users user)
        {
            users.Add(user);
            return await Task.FromResult(true);
        }

        public async Task<int> UpdateUserAsync(Users user)
        {
            return await App.DB.UpdateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var oldUser = users.Where((Users arg) => arg.Id == id).FirstOrDefault();
            users.Remove(oldUser);

            return await Task.FromResult(true);
        }

        public async Task<Users> GetUserAsync(int id)
        {
            return await App.DB.GetUserAsync(id);
        }


        public async Task<IEnumerable<Users>> GetUsersAsync(bool forceRefesh = false)
        {
            return await App.DB.GetUsersAsync();
        }

        public async Task<Users> Login(string userName, string passWord)
        {
            return await App.DB.Login(userName, passWord);
        }

        public async Task<int> Register(Users user)
        {
            return await App.DB.RegisterAccount(user);
        }
    }
}

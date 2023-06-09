using BTL_Nhom6.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BTL_Nhom6.Database
{
    public class DatabaseService
    {
        static SQLiteAsyncConnection database;

        public DatabaseService()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "net03.db");
            Assembly assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            Stream embeddedDbStream = assembly.GetManifestResourceStream("BTL_Nhom6.net03.db");
            
            if (!File.Exists(dbPath))
            {
                FileStream fileStreamToWrite = File.Create(dbPath);
                embeddedDbStream.Seek(0, SeekOrigin.Begin);
                embeddedDbStream.CopyTo(fileStreamToWrite);
                fileStreamToWrite.Close();
            }

            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Chapters>().Wait();
            database.CreateTableAsync<Users>().Wait();
            database.CreateTableAsync<Articles>().Wait();
            database.CreateTableAsync<Sections>().Wait();
        }

        // ---

        public Task<List<Chapters>> SearchChapterAsync(string searchString)
        {
            return database.Table<Chapters>().Where(item => item.Title.Contains(searchString)).ToListAsync();
        }

        public Task<int> UpdateChapterAsync(Chapters chapter)
        {
            return database.UpdateAsync(chapter);
        }

        public Task<int> DeleteChapterAsync(Chapters chapter)
        {
            return database.DeleteAsync(chapter);
        }

        public Task<List<Chapters>> GetChaptersAsync()
        {
            return database.Table<Chapters>().ToListAsync();
        }

        public Task<Chapters> GetChapterAsync(int id)
        {
            return database.Table<Chapters>()
        .Where(chapter => chapter.Id == id)
        .FirstOrDefaultAsync();
        }

        // --- 

        public Task<List<Users>> GetUsersAsync()
        {
            return database.Table<Users>().ToListAsync();
        }

        public Task<Users> GetUserAsync(int itemId)
        {
            return database.Table<Users>().Where(user => user.Id == itemId).FirstOrDefaultAsync();
        }

        public Task<int> UpdateUserAsync(Users user)
        {
            return database.UpdateAsync(user);
        }

        public Task<Users> Login(string userName, string passWord)
        {
            return database.Table<Users>().Where(user => user.UserName.Equals(userName) && user.PassWord.Equals(passWord)).FirstOrDefaultAsync();
        }

        public async Task<int> RegisterAccount(Users user)
        {
            return await database.InsertAsync(user);
        }

        // --- 

        public Task<int> UpdateArticleAsync(Articles articles)
        {
            return database.UpdateAsync(articles);
        }

        public Task<int> DeleteArticleAsync(Articles articles)
        {
            return database.DeleteAsync(articles);
        }

        public Task<List<Articles>> GetArticlesAsync(int itemId)
        {
            return database.Table<Articles>().Where(article => article.ChapterId == itemId).ToListAsync();
        }

        public Task<Articles> GetArticleAsync(int id)
        {
            return database.Table<Articles>()
        .Where(articles => articles.Id == id)
        .FirstOrDefaultAsync();
        }

        // ---

        public Task<int> UpdateSectionAsync(Sections sections)
        {
            return database.UpdateAsync(sections);
        }

        public Task<int> DeleteSectionAsync(Sections sections)
        {
            return database.DeleteAsync(sections);
        }

        public Task<List<Sections>> GetSectionsAsync(int itemId) 
        { 
            return database.Table<Sections>().Where(sections => sections.ArticleId == itemId).ToListAsync();
        }

        public Task<Sections> GetSectionAsync(int id)
        {
            return database.Table<Sections>()
        .Where(sections => sections.Id == id)
        .FirstOrDefaultAsync();
        }

        public Task<List<Sections>> SearchSectionsAsync(string searchString)
        {
            return database.Table<Sections>().Where(item => item.Title.Contains(searchString) || item.Content.Contains(searchString)).ToListAsync();
        }
    }
}

using SQLite;
using System;

namespace BTL_Nhom6.Models
{
    [Table("Users")]
    public class Users
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public int Mobile { get; set; }
        public string Role { get; set; }
    }
}

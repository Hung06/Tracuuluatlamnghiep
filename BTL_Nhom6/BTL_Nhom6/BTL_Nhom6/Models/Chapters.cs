using System;
using SQLite;

namespace BTL_Nhom6.Models
{
    [Table("Chapters")]
    public class Chapters
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CreateTime { get; set; }
        public string UpdateTime { get; set; }
        public int Decree { get; set; }
    }
}
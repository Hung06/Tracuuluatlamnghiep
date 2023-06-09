using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BTL_Nhom6.Models
{
    [Table("Sections")]
    public class Sections
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public string Avg { get; set; }
        public string CreateTime { get; set; }
        public string UpdateTime { get; set; }
        public int ArticleId { get; set; }
        public int DecreeId { get; set; }
    }
}

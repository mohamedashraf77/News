using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Models
{
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<News> news { get; set; }
        public Category()
        {
            news = new List<News>();
        }
    }
}

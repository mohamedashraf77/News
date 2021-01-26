using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Models
{
    public class News
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime date { get; set; }
        public string topic { get; set; }
        public Category category { get; set; }
       
        [ForiegnKey("category")]
        public int categoryID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Topic { get; set; }
        public Category Category { get; set; }
       
        [ForiegnKey("category")]
        public int CategoryId { get; set; }
    }
}

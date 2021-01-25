using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Models
{
    public class News
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Topic { get; set; }
        public Category Category { get; set; }
       
        [ForiegnKey("category")]
        public int CategoryID { get; set; }
    }
}

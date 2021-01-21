using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Models
{
    public class news
    {
        public int ID { get; set; }
        public string title { get; set; }
        public DateTime date { get; set; }
        public string topic { get; set; }
        public category Category { get; set; }
       
        [ForiegnKey("category")]
        public int CategoryID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Models
{
    public class category
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<news> news { get; set; }
        public category()
        {
            news = new List<news>();
        }
    }
}

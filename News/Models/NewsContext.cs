using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace News.Models
{
    public class NewsContext : DbContext
    {
        public NewsContext(DbContextOptions<NewsContext> options)
            : base(options)
        {

        }
        public DbSet<Category> categories { get; set; }
        public DbSet<ContactUs> contactUs { get; set; }
        public DbSet<News> news { get; set; }
        public DbSet<Admin> admin { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using News.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace News.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        NewsContext DataBaseContext;

        public HomeController(ILogger<HomeController> logger, NewsContext context)
        {
            _logger = logger;
            DataBaseContext = context;
        }
        
        public IActionResult Index()
        { 
            return View(DataBaseContext.categories.ToList());
        }
        
        //Action to handle Contuct us view 
        public IActionResult ContuctUS()
        {
            return View();
        }

        //Action to recieve message from contuc us form
        [HttpPost]
        public IActionResult SaveContuct(ContactUs model)
        {
            DataBaseContext.ContactUs.Add(model);
            DataBaseContext.SaveChanges();
            return RedirectToAction("ContuctUs");
        }
        //Action to handle Admin view
        public IActionResult Admin()
        {
            return View();
        }

        //Action to handle News view
        public IActionResult News(int id)
        {
            var result = DataBaseContext.news.Where(n => n.CategoryID == id).ToList();
            return View(result);
        }

        //Action to handle Contuct us view
        public IActionResult login()
        { 
            return View();
        }

        //Action to check if user name and password are correct
        public IActionResult CheckLogin(Admin model)
        {
            var member = DataBaseContext.Admin.Where(t => t.mail == model.mail).ToList();
            if (member.Count == 0)
                return RedirectToAction("LoginError");
            else if (member[0].password == model.password)
                return RedirectToAction("Admin");
            else
                return RedirectToAction("LoginError");
        }
        //Action to response by error if mail or password is wrong
        public IActionResult LoginError()
        { 
            return View();
        }
        //Action to response by view that let admin to insert new news
        public IActionResult Add()
        {
            return View();
        }
        //Action to insert new news
        public IActionResult AddNews(news model)
        {
            model.date = DateTime.Now;
            var category = DataBaseContext.categories.Where(c => c.name == model.Category.name).ToList();
            model.CategoryID = category[0].ID;
            model.Category = null;
            DataBaseContext.Add(model);
            DataBaseContext.SaveChanges();
            return RedirectToAction("Admin");
        }

        //Action to response by view that let admin to delete an news
        public IActionResult Delete()
        {
            return View(DataBaseContext.news.ToList());
        }
        [HttpPost]
        //Action to delet an news
        public IActionResult remove(int id)
        {
            var removed_news = DataBaseContext.news.Where(n => n.ID==id).FirstOrDefault();
            DataBaseContext.news.Remove(removed_news);
            DataBaseContext.SaveChanges();
            return RedirectToAction("Delete");
        }
        
        //Action to response by view that let admin to update new news
        public IActionResult update()
        {
            return View(DataBaseContext.news.ToList());
        }
        [HttpPost]
        public IActionResult edite(int id)
        {
            var updated_news = DataBaseContext.news.Where(n => n.ID == id).FirstOrDefault();
            var cat = DataBaseContext.categories.Where(c => c.ID == updated_news.CategoryID).FirstOrDefault();
            updated_news.Category = cat;
            return View(updated_news);
        }
        public IActionResult EditeNews(int id, string title, string topic, string category)
        {
            var updated_news = DataBaseContext.news.Where(n => n.ID == id).FirstOrDefault();
            if(title != null)
                updated_news.title = title;
            if(topic != null)
                updated_news.topic = topic;
            if(category != null)
            {
                updated_news.CategoryID = DataBaseContext.categories.Where(c => c.name == category).FirstOrDefault().ID;
                updated_news.Category = null;
            }
            DataBaseContext.news.Update(updated_news);
            DataBaseContext.SaveChanges();
            return RedirectToAction("update");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

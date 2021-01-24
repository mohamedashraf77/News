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
        NewsContext dataBaseContext;

        public HomeController(ILogger<HomeController> logger, NewsContext context)
        {
            _logger = logger;
            dataBaseContext = context;
        }
        
        public IActionResult Index()
        { 
            return View(dataBaseContext.Categories.ToList());
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
            dataBaseContext.ContactUs.Add(model);
            dataBaseContext.SaveChanges();
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
            var result = dataBaseContext.News.Where(n => n.CategoryID == id).ToList();
            return View(result);
        }

        //Action to handle Contuct us view
        public IActionResult Login()
        { 
            return View();
        }

        //Action to check if user name and password are correct
        public IActionResult CheckLogin(Admin model)
        {
            var member = dataBaseContext.Admin.Where(t => t.Mail == model.Mail).ToList();
            if (member.Count == 0)
                return RedirectToAction("LoginError");
            else if (member[0].Password == model.Password)
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
        public IActionResult AddNews(Models.News model)
        {
            model.Date = DateTime.Now;
            var category = dataBaseContext.Categories.Where(c => c.Name == model.Category.Name).ToList();
            model.CategoryID = category[0].ID;
            model.Category = null;
            dataBaseContext.Add(model);
            dataBaseContext.SaveChanges();
            return RedirectToAction("Admin");
        }

        //Action to response by view that let admin to delete an news
        public IActionResult Delete()
        {
            return View(dataBaseContext.News.ToList());
        }
        [HttpPost]
        //Action to delet an news
        public IActionResult Remove(int id)
        {
            var removed_news = dataBaseContext.News.Where(n => n.ID==id).FirstOrDefault();
            dataBaseContext.News.Remove(removed_news);
            dataBaseContext.SaveChanges();
            return RedirectToAction("Delete");
        }
        
        //Action to response by view that let admin to update new news
        public IActionResult Update()
        {
            return View(dataBaseContext.News.ToList());
        }
        [HttpPost]
        public IActionResult Edite(int id)
        {
            var updated_news = dataBaseContext.News.Where(n => n.ID == id).FirstOrDefault();
            var cat = dataBaseContext.Categories.Where(c => c.ID == updated_news.CategoryID).FirstOrDefault();
            updated_news.Category = cat;
            return View(updated_news);
        }
        public IActionResult EditeNews(int id, string title, string topic, string category)
        {
            var updated_news = dataBaseContext.News.Where(n => n.ID == id).FirstOrDefault();
            if(title != null)
                updated_news.Title = title;
            if(topic != null)
                updated_news.Topic = topic;
            if(category != null)
            {
                updated_news.CategoryID = dataBaseContext.Categories.Where(c => c.Name == category).FirstOrDefault().ID;
                updated_news.Category = null;
            }
            dataBaseContext.News.Update(updated_news);
            dataBaseContext.SaveChanges();
            return RedirectToAction("update");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

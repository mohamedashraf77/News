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
        NewsContext _dataBaseContext;

        public HomeController(ILogger<HomeController> logger, NewsContext context)
        {
            _logger = logger;
            _dataBaseContext = context;
        }
        
        public IActionResult Index()
        { 
            return View(_dataBaseContext.Categories.ToList());
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
            _dataBaseContext.ContactUs.Add(model);
            _dataBaseContext.SaveChanges();
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
            var result = _dataBaseContext.News.Where(n => n.CategoryId == id).ToList();
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
            var member = _dataBaseContext.Admin.Where(t => t.Mail == model.Mail).ToList();
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
            var category = _dataBaseContext.Categories.Where(c => c.Name == model.Category.Name).ToList();
            model.CategoryId = category[0].Id;
            model.Category = null;
            _dataBaseContext.Add(model);
            _dataBaseContext.SaveChanges();
            return RedirectToAction("Admin");
        }

        //Action to response by view that let admin to delete an news
        public IActionResult Delete()
        {
            return View(_dataBaseContext.News.ToList());
        }
        [HttpPost]
        //Action to delet an news
        public IActionResult Remove(int id)
        {
            var removed_news = _dataBaseContext.News.Where(n => n.Id==id).FirstOrDefault();
            _dataBaseContext.News.Remove(removed_news);
            _dataBaseContext.SaveChanges();
            return RedirectToAction("Delete");
        }
        
        //Action to response by view that let admin to update new news
        public IActionResult Update()
        {
            return View(_dataBaseContext.News.ToList());
        }
        [HttpPost]
        public IActionResult Edite(int id)
        {
            var updated_news = _dataBaseContext.News.Where(n => n.Id == id).FirstOrDefault();
            var cat = _dataBaseContext.Categories.Where(c => c.Id == updated_news.CategoryId).FirstOrDefault();
            updated_news.Category = cat;
            return View(updated_news);
        }
        public IActionResult EditeNews(int id, string title, string topic, string category)
        {
            var updated_news = _dataBaseContext.News.Where(n => n.Id == id).FirstOrDefault();
            if(title != null)
                updated_news.Title = title;
            if(topic != null)
                updated_news.Topic = topic;
            if(category != null)
            {
                updated_news.CategoryId = _dataBaseContext.Categories.Where(c => c.Name == category).FirstOrDefault().Id;
                updated_news.Category = null;
            }
            _dataBaseContext.News.Update(updated_news);
            _dataBaseContext.SaveChanges();
            return RedirectToAction("update");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

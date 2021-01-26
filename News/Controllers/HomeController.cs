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
            return View(dataBaseContext.categories.ToList());
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
            dataBaseContext.contactUs.Add(model);
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
            var result = dataBaseContext.news.Where(n => n.categoryID == id).ToList();
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
            var member = dataBaseContext.admin.Where(t => t.mail == model.mail).ToList();
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
        public IActionResult AddNews(Models.News model)
        {
            model.date = DateTime.Now;
            var category = dataBaseContext.categories.Where(c => c.name == model.category.name).ToList();
            model.categoryID = category[0].id;
            model.category = null;
            dataBaseContext.Add(model);
            dataBaseContext.SaveChanges();
            return RedirectToAction("Admin");
        }

        //Action to response by view that let admin to delete an news
        public IActionResult Delete()
        {
            return View(dataBaseContext.news.ToList());
        }
        [HttpPost]
        //Action to delet an news
        public IActionResult Remove(int id)
        {
            var removed_news = dataBaseContext.news.Where(n => n.id==id).FirstOrDefault();
            dataBaseContext.news.Remove(removed_news);
            dataBaseContext.SaveChanges();
            return RedirectToAction("Delete");
        }
        
        //Action to response by view that let admin to update new news
        public IActionResult Update()
        {
            return View(dataBaseContext.news.ToList());
        }
        [HttpPost]
        public IActionResult Edite(int id)
        {
            var updated_news = dataBaseContext.news.Where(n => n.id == id).FirstOrDefault();
            var cat = dataBaseContext.categories.Where(c => c.id == updated_news.categoryID).FirstOrDefault();
            updated_news.category = cat;
            return View(updated_news);
        }
        public IActionResult EditeNews(int id, string title, string topic, string category)
        {
            var updated_news = dataBaseContext.news.Where(n => n.id == id).FirstOrDefault();
            if(title != null)
                updated_news.title = title;
            if(topic != null)
                updated_news.topic = topic;
            if(category != null)
            {
                updated_news.categoryID = dataBaseContext.categories.Where(c => c.name == category).FirstOrDefault().id;
                updated_news.category = null;
            }
            dataBaseContext.news.Update(updated_news);
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

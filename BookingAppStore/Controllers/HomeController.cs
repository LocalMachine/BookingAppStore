using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookingAppStore.Models;

namespace BookingAppStore.Controllers
{
    public class HomeController : Controller
    {
        BookContext db = new BookContext();

        public ActionResult Index()
        {
            var books = db.Books;
            ViewBag.Books = books;

            return View();
        }
        
        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.Books = id;

            return View();
        }

        [HttpPost] // необходимо чтобы компилятор понял какой из двух запросов Buy  ему выполнять 
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            //Добавляе информацию о покупке в бд
            db.Purchases.Add(purchase);
            //Сохраняем все изменения в бд
            db.SaveChanges();

            return "Спасибо " + purchase.Person + " за покупку";
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
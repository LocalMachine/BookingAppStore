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

            ViewData["Head"] = "Hello Rachik jan, vonces brats?)"; // обычный обьект типа словаря (ключ=> значение)
            ViewBag.Head2 = "Lavem, inch ka chka?"; // динамический обьект, можем определить любое содержимое. Аналогичный вариант как и ViewData. Если указать одинаковые названия то будет выводить послед вариант
                                                    //Разница в том что, Нет необходимости в приведении типов для получения данных. 

            ViewBag.Answer = new List<string>
            {
                "lavem", "shat lavem", "gehecik"
            };

            return View(); // по умолчанию будет выводиться представление Views/Home/Index (т.к. автоматом выполняется представление исходя из названия метода). 
                           //Чтобы переопределить, достаточно будет передать названиме представления в результат метода View например: return View("About")
                           // так же можем указать конкретный путь к представлению пример: return View("~/Views/Some/Index.cshtml")
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

        public RedirectResult GetVoid()
        {
            return Redirect("/Home/Contact"); // постоянная переадресация  (RedirectPermanent - временная)
            //return RedirectToRoute(new { conteoller = "Home", action = "Contact" });
            //return RedirectToAction("Square","Home", new {a=10,h=5});  если метод находится в одном и том же контроллере то можно написать так RedirectToAction("Contact");
        }

        public ActionResult GetVoid2()
        {          
            return new HttpStatusCodeResult(404); // или  HttpNotFound() // или когда не авторизован HttpUnauthorizedResult
        }
    }

}
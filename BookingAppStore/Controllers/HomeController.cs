using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BookingAppStore.Models;
using System.Data.Entity;

namespace BookingAppStore.Controllers
{
    public class HomeController : Controller
    {
        BookContext db = new BookContext(); // определяем переменную контекста данных


        public ActionResult Index()
        {
            using (BookContext db2 = new BookContext()) //Анологичный вариант BookContext db = new BookContext(); -- единственное, необходимо будет во втором случае, уничтожать переменную с помощью метода Dispose()
            {
                var boooks = db2.Books; 
            }

                HttpContext.Response.Cookies["id"].Value = "rgag"; // установили куку
            Session["name"] = "Rach"; //установил сессию. Чтобы удалить сессию: Session["name"] = null

            var books = db.Books;
            ViewBag.Books = books;

            ViewData["Head"] = "Hello Rachik jan, vonces brats?)"; // обычный обьект типа словаря (ключ=> значение)
            ViewBag.Head2 = "Lavem, inch ka chka?"; // динамический обьект, можем определить любое содержимое. Аналогичный вариант как и ViewData. Если указать одинаковые названия то будет выводить послед вариант
                                                    //Разница в том что, Нет необходимости в приведении типов для получения данных. 

            ViewBag.Answer = new List<string>
            {
                "lavem", "shat lavem", "gehecik"
            };

            ViewBag.Message = "message";

            IEnumerable<Book> i_books = db.Books.ToList(); //синхронный метод = если каждый запрос будет долгий(Нпр к бд), то сервис будет заморожен до тех пор пока не получит результат
            ViewBag.i_Books = i_books;



            SelectList authors = new SelectList(db.Books, "Author", "Name"); // (обьекты book , свойство значения, свойство отображения)
            ViewBag.Authors = authors;



            return View(books); // по умолчанию будет выводиться представление Views/Home/Index (т.к. автоматом выполняется представление исходя из названия метода). 
                           //Чтобы переопределить, достаточно будет передать названиме представления в результат метода View например: return View("About")
                           // так же можем указать конкретный путь к представлению пример: return View("~/Views/Some/Index.cshtml")
        }



        public ActionResult BookIndex()
        {
            var books = db.Books;
            ViewBag.Books = books;


            IEnumerable<Book> i_books = db.Books.ToList(); //синхронный метод = если каждый запрос будет долгий(Нпр к бд), то сервис будет заморожен до тех пор пока не получит результат
            ViewBag.i_Books = i_books;

            return View(books); // по умолчанию будет выводиться представление Views/Home/Index (т.к. автоматом выполняется представление исходя из названия метода). 
                                //Чтобы переопределить, достаточно будет передать названиме представления в результат метода View например: return View("About")
                                // так же можем указать конкретный путь к представлению пример: return View("~/Views/Some/Index.cshtml")
        }





        // асинхронный метод 
        public async Task<ActionResult> BookList()
        {
            // необхордимо подклютить простр имен using System.Data.Entity;
            IEnumerable<Book> i_books = await db.Books.ToListAsync(); //асинхронный метод = если каждый запрос будет долгий (Нпр к бд), то поток не ждет пока данные будут получены  а переключается на обработку других запросов
            ViewBag.i_Books = i_books;

            return View("Index");
        }

        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.Books = id;
            Purchase purchase = new Purchase { BookId = id, Person="unknown" };

            return View(purchase);
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

        public FilePathResult GetFile() // отправление файла из файловой системы
        {
            // путь к файлу
            string file_path = Server.MapPath("~/Files/Пример.pdf");
            // тип файла content-type
            string file_type = "application/pdf";  // можно использовать универсанльныйтип файла (txt,docx,zip и тд) = "applicaton/octet-stream"
            // имя файла - необязательно  
            string file_name = "Пример.pdf";

            return File(file_path, file_type, file_name);
        }

        public FileContentResult GetBytes() // отправление файла через массив байтов
        {
            // путь к файлу
            string file_path = Server.MapPath("~/Files/Пример.pdf");
            byte[] mas = System.IO.File.ReadAllBytes(file_path);
            // тип файла content-type
            string file_type = "application/pdf";
            // имя файла - необязательно  
            string file_name = "Пример.pdf";

            return File(mas, file_type, file_name);
        }

        public FileStreamResult GetStream() // отправление файла через поток
        {
            // путь к файлу
            string file_path = Server.MapPath("~/Files/Пример.pdf");
            //
            FileStream fs = new FileStream(file_path, FileMode.Open);
            // тип файла content-type
            string file_type = "application/pdf";
            // имя файла - необязательно  
            string file_name = "Пример.pdf";

            return File(fs, file_type, file_name);
        }

        public string GetContext()
        {
            // HttpContext.Response.Write("Hello");с помощью свойства респонс
            string browser = HttpContext.Request.Browser.Browser;
            string user_agent = HttpContext.Request.UserAgent;
            string url = HttpContext.Request.RawUrl;
            string ip = HttpContext.Request.UserHostAddress;
            string referrer = HttpContext.Request.UrlReferrer == null ? "" : HttpContext.Request.UrlReferrer.AbsoluteUri;

            return "<p>Browser: " + browser + "</p>" +
                    "<p>User-agent: " + user_agent + "</p>" +
                    "<p>Url zaprosa: " + url + "</p>" +
                    "<p>IP: " + ip + "</p>" +
                    "<p>Referre: " + referrer + "</p>";
        }

        public string GetData() // получить сессию и куки
        {
            string id = HttpContext.Request.Cookies["id"].Value;
            var session = Session["name"];

            return id + "   " + session; 
        }

        public ActionResult GetList()
        {
            string[] states = new string[] { "Russia", "Armenia", "Egypt", "Denmark" };

            return PartialView(states);
        }

        [HttpPost]
        public string GetForm(string[] countries) //text - если выбрать текст ареа, зависит от имени тега
        {
            string result = "";
            foreach (string c in countries)
            {
                result += c;
                result += ";";
            }
            return "Вы выбрали " + result;
        }



    }

}
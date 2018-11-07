using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookingAppStore.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetBook()
        {
            return View();
        }


        [HttpPost]
        public string GetBook(string title, string author) // либо можно будет написать как метод Square, только в форме контроллера надо будет обьвить  action="GetBook" 
        {
            return title + " " + author;
        }


        public string Square() // способ передачи не обязательных параметров
        {
            int a = Int32.Parse(Request.Params["a"]);
            int b = Int32.Parse(Request.Params["b"]);
            double s = a * b / 2;

            return "<h2>Result = " + s + "</h2>";
        }
    }
}
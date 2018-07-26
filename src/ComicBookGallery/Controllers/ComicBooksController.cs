using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComicBookGallery.Controllers
{
    public class ComicBooksController : Controller
    {
        // GET: ComicBooks
        public ActionResult Index()
        {

            if (DateTime.Today.DayOfWeek != DayOfWeek.Monday)
            {
                return Content("Merdas");
              //  return Redirect("/");
            }

            else return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComicBookGallery.Data;


namespace ComicBookGallery.Controllers
{
    public class ComicBooksController : Controller
    {
        private ComicBookRepository rp = new ComicBookRepository();



        // GET: ComicBooks
        public ActionResult Index()
        {
            var comicBook = rp.getComicBooks();

            return View(comicBook);
        }

        public ActionResult Detail(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }
            var comicBook = rp.getComicBook((int)id);
            return View(comicBook);
        }


    }
}
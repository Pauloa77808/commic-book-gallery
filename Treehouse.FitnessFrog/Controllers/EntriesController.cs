using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Treehouse.FitnessFrog.Data;
using Treehouse.FitnessFrog.Models;

namespace Treehouse.FitnessFrog.Controllers
{
    public class EntriesController : Controller
    {
        private EntriesRepository _entriesRepository = null;

        public EntriesController()
        {
            _entriesRepository = new EntriesRepository();
        }

        public ActionResult Index()
        {
            List<Entry> entries = _entriesRepository.GetEntries();

            // Calculate the total activity.
            double totalActivity = entries
                .Where(e => e.Exclude == false)
                .Sum(e => e.Duration);

            // Determine the number of days that have entries.
            int numberOfActiveDays = entries
                .Select(e => e.Date)
                .Distinct()
                .Count();

            ViewBag.TotalActivity = totalActivity;
            ViewBag.AverageDailyActivity = (totalActivity / (double)numberOfActiveDays);

            return View(entries);
        }

        public ActionResult Add()
        {
            var entry = new Entry()
            {
                Date = DateTime.Today
            };


            ViewBag.ListDropDown = new SelectList(Data.Data.Activities, "Id", "Name");

            return View(entry);
        }
   

        [HttpPost]
        public ActionResult Add(Entry entry)
        {
            
            if(ModelState.IsValidField("Duration") && entry.Duration <=0) {

                ModelState.AddModelError("Duration","The Duration Filed must be number greater than 0");
            }

            if (ModelState.IsValid) {
                _entriesRepository.AddEntry(entry);

                TempData["Message"] = "Your entry was added with sucess!";

                return RedirectToAction("Index");
            }

            ViewBag.ListDropDown = new SelectList(Data.Data.Activities, "Id", "Name");

            return View();
        }


        public ActionResult Edit(int? id) {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Entry entryAux = _entriesRepository.GetEntry((int)id);

            ViewBag.ListDropDown = new SelectList(Data.Data.Activities, "Id", "Name");

            return View(entryAux);
        }


        [HttpPost]
        public ActionResult Edit(Entry entry)
        {
            if (entry == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValidField("Duration") && entry.Duration <= 0)
            {
                ModelState.AddModelError("Duration", "The Duration Filed must be number greater than 0");
            }

            if (ModelState.IsValid)
            {
                _entriesRepository.UpdateEntry(entry);

                TempData["Message"] = "The entry with Id " + entry.Id + " was edit with sucess!";

                return RedirectToAction("Index");
            }

            ViewBag.ListDropDown = new SelectList(Data.Data.Activities, "Id", "Name");

            return View();

        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            _entriesRepository.DeleteEntry((int)id);

            TempData["Message"] = "The Entry was deleted with sucess!";

            return RedirectToAction("Index");

        }
    }
}
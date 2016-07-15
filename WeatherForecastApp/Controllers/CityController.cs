using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WeatherForecastApp.Models;

namespace WeatherForecastApp.Controllers
{
    public class CityController : Controller
    {
        private ApplicationDbContext _context;
        public CityController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: City
        public ActionResult Index()
        {
            return View("List", _context.CityByDefaults.ToList());
        }

        // GET: City/Add
        public ActionResult Add()
        {
            return View();
        }

        //POST: City/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CityByDefault city)
        {
            if (!ModelState.IsValid)
            {
                return View(city);
            }

            _context.CityByDefaults.Add(city);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: City/Edit/1
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var city = _context.CityByDefaults.SingleOrDefault(c => c.Id == id);

            if (city == null)
            {
                return HttpNotFound();
            }

            return View(city);
        }

        // POST: City/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CityByDefault city)
        {
            if (!ModelState.IsValid)
            {
                return View(city);
            }

            var cityInDb = _context.CityByDefaults.SingleOrDefault(c => c.Id == city.Id);

            if (cityInDb == null)
            {
                return HttpNotFound();
            }

            cityInDb.Name = city.Name;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: City/Delete/1
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cityInDb = _context.CityByDefaults.SingleOrDefault(c => c.Id == id);

            if (cityInDb == null)
            {
                return HttpNotFound();
            }

            return View(cityInDb);
        }

        // POST: City/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var cityInDb = _context.CityByDefaults.SingleOrDefault(c => c.Id == id);

            _context.CityByDefaults.Remove(cityInDb);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

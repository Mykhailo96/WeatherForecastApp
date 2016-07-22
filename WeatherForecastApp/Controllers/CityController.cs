using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WeatherForecastApp.Models;
using WeatherForecastApp.Services;

namespace WeatherForecastApp.Controllers
{
    public class CityController : Controller
    {
        private ICityService _service;

        public CityController(ICityService service)
        {
            _service = service;
        }

        // GET: City
        public ActionResult Index()
        {
            return View("List", _service.GetCityByDefaultList());
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

            _service.AddcityByDefault(city);
            _service.SaveAllChanges();

            return RedirectToAction("Index");
        }

        // GET: City/Edit/1
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var city = _service.GetCityByDefaultById(id);

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

            var cityInDb = _service.GetCityByDefaultById(city.Id);

            if (cityInDb == null)
            {
                return HttpNotFound();
            }

            cityInDb.Name = city.Name;
            _service.SaveAllChanges();

            return RedirectToAction("Index");
        }

        // GET: City/Delete/1
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cityInDb = _service.GetCityByDefaultById(id);

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
            var cityInDb = _service.GetCityByDefaultById(id);

            _service.RemoveCityByDefault(cityInDb);
            _service.SaveAllChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _service.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Index()
        {
            return View("List", await _service.GetCityByDefaultListAsync());
        }

        // GET: City/Add
        public ActionResult Add()
        {
            return View();
        }

        //POST: City/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(CityByDefault city)
        {
            if (!ModelState.IsValid)
            {
                return View(city);
            }

            _service.AddcityByDefault(city);
            await _service.SaveAllChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: City/Edit/1
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var city = await _service.GetCityByDefaultByIdAsync(id);

            if (city == null)
            {
                return HttpNotFound();
            }

            return View(city);
        }

        // POST: City/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CityByDefault city)
        {
            if (!ModelState.IsValid)
            {
                return View(city);
            }

            var cityInDb = await _service.GetCityByDefaultByIdAsync(city.Id);

            if (cityInDb == null)
            {
                return HttpNotFound();
            }

            cityInDb.Name = city.Name;
            await _service.SaveAllChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: City/Delete/1
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cityInDb = await _service.GetCityByDefaultByIdAsync(id);

            if (cityInDb == null)
            {
                return HttpNotFound();
            }

            return View(cityInDb);
        }

        // POST: City/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var cityInDb = await _service.GetCityByDefaultByIdAsync(id);

            _service.RemoveCityByDefault(cityInDb);
            await _service.SaveAllChangesAsync();

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

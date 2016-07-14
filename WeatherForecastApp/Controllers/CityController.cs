using System;
using System.Collections.Generic;
using System.Linq;
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
            var cities = _context.CityByDefaults.ToList();

            return View("List", cities);
        }
    }
}
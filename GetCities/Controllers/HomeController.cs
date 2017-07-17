using GetCities.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GetCities.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetJsonData(string url)
        {
            CitiesHandler dataSource = new CitiesHandler();
            List<GetCities.Classes.CityInfo> cities = dataSource.getCities(url);

            return Json(cities, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Cities(string url)
        {
            return View(url as object);
        }
    }
}
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


        public JsonResult GetJsonData()
        {
            CitiesHandler dataSource = new CitiesHandler();
            List<GetCities.Classes.CityInfo> cities = dataSource.getCities();

            return Json(cities, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Cities()
        {
            return View();
        }
    }
}
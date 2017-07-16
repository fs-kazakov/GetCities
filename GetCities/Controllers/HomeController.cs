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

        public ActionResult Cities()
        {
            CitiesHandler dataSource = new CitiesHandler();

            List<GetCities.Classes.CityInfo> viewmodel = dataSource.getCities();
            return View(viewmodel as object);
        }
    }
}
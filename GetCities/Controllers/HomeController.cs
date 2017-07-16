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

            string testResult = dataSource.getCities();
            return View(testResult as object);
        }
    }
}
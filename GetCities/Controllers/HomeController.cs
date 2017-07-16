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

            string htmlCode;

            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                htmlCode = client.DownloadString("http://www.gismeteo.ua/catalog/");
            }

            object htmlCodeObj = htmlCode as object;
            return View(htmlCodeObj);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Real_Time_Map_TechBrij.Hubs;
namespace Real_Time_Map_TechBrij.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //MapsHub obj = new MapsHub();
         
            return View();
        }

    }
}

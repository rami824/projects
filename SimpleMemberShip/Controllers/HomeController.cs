using SimpleMemberShip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleMemberShip.Controllers
{
    public class HomeController : Controller
    {
        private Model1Container db = new Model1Container();
        public ActionResult Index()
        {
            return View();
        }

       // [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       
    }
}
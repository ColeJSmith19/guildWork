using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spark.Data.Factory;

namespace Spark.UI.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            var model = CardInfoRepositoryFactory.GetRepository().GetMostExpensive();
            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
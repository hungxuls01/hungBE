using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace Service.Controllers
{
    public class HomeController : Controller
    {
        private static Logger _logger;
        public HomeController()
        {
            _logger = LogManager.GetCurrentClassLogger();



        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

    }
}

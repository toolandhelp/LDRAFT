using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lm.WebMVC.Views
{
    public class AboutUsController : Controller
    {
        // GET: AboutUs
        public ActionResult Index()
        {
            return View();
        }
        //GET:企业介绍
        public ActionResult EntInt()
        {
            return View();
        }
        //GET:专业团队
        public ActionResult MajTeam()
        {
            return View();
        }
    }
}
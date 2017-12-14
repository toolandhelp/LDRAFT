
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.CommonLib;

namespace Lm.WebMVC.Controllers
{
    public class NewConentController : Controller
    {
        // GET: NewConent
        public ActionResult Index()
        {
            return View();
        }


        // /NewConent/Industry  行业动态
        public ActionResult Industry()
        {
            return View();
        }

        // /NewConent/New  新闻
        public ActionResult New()
        {
            return View();
        }

        // 详情
        public ActionResult Default()
        {
            var Id =  RequestParameters.Pint("id");
            ViewBag.ID = Id;
            return View();
        }
    }
}
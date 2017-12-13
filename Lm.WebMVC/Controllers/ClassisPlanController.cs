
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.CommonLib;

namespace Lm.WebMVC.Controllers
{
    public class ClassisPlanController : Controller
    {
        // GET: ClassisPlan
        public ActionResult Index()
        {
            return View();
        }

        //详情内容
        public ActionResult Default()
        {
            string id = RequestParameters.Pstring("id");
            ViewBag.ID = id;
            return View();
        }
        //超高层
        public ActionResult cgc()
        {
            return View();
        }

        public ActionResult jbg()
        {
            return View();
        }
    }
}
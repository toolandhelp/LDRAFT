using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lm.WebMVC.Controllers
{
    public class AboutUsController : Controller
    {
        // GET: AboutUs
        public ActionResult Index()
        {
            return View();
        }

        //企业介绍
        public ActionResult EntInt()
        {
            return View();
        }

        //专业团队
        public ActionResult MajTeam()
        {
            return View();
        }

        //企业理念
        public ActionResult EntPhi()
        {
            return View();
        }

        //组织框架
        public ActionResult OrgFrames()
        {
            return View();
        }

        //我们的客户
        public ActionResult OurClient()
        {
            return View();
        }

    }
}
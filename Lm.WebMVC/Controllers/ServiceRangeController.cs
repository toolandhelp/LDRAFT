using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lm.WebMVC.Controllers
{
    public class ServiceRangeController : Controller
    {
        // GET: ServiceRange
        public ActionResult Index()
        {
            return View();
        }

        // 设计结构
        public ActionResult Structure()
        {
            return View();
        }

        // 建筑方案
        public ActionResult ArchPlan()
        {
            return View();
        }

        // 设备专业
        public ActionResult EquMag()
        {
            return View();
        }

        // 幕墙钢结构
        public ActionResult mqg()
        {
            return View();
        }

        // 地下室停车
        public ActionResult dxs()
        {
            return View();
        }

        // 超限结构
        public ActionResult cxjg()
        {
            return View();
        }
    }
}
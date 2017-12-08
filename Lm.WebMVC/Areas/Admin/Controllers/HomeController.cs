using Lm.BLL;
using Lm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lm.WebMVC.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Utits.IsLogin)
            {
                return RedirectPermanent("/Home/Index?_r=0." + Guid.NewGuid().ToString().Replace("-", ""));

            }
            return View();
        }

        public ActionResult Home()
        {
            if (Utits.IsLogin)
            {
                return RedirectPermanent("/Home/Index?_r=0." + Guid.NewGuid().ToString().Replace("-", ""));

            }
            return View();
        }

        public JsonResult LeftAuthMenuNav()
        {
            var cBll = new BLL_Menu();
            var list = cBll.GetMenu_A_ListByAll().OrderBy(o=>o.menu_Sort).ToList();

            if (list != null)
            {
                return Json(list.Select(itemNode => new TreeModel
                {
                    // Id=itemNode.menu_Id,
                    Id = itemNode.menu_Code,
                    Pid = itemNode.menu_FatherId,
                    Title = itemNode.menu_Name,
                    Target = itemNode.menu_Image,
                    Url = itemNode.menu_Link

                }));
            }
            return Json("[]");
        }
    }
}
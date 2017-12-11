using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.BLL;
using Lm.CommonLib;
using Lm.Model;

namespace Lm.WebMVC.Controllers
{
    public class PublicController : Controller
    {
        // GET: Public
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 菜单导航
        /// </summary>
        /// <returns></returns>
        public JsonResult TopNav()
        {

            var cBll = new BLL_Menu();
            var list = cBll.GetMenu_B_ListByAll().Where(o => o.menu_Stage == (int)StageMode.Normal);

            if (list != null)
            {
                return Json(list.Select(itemNode => new TreeModel
                {
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
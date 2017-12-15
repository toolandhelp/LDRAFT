
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.CommonLib;
using Lm.BLL;
using Lm.Model;
using System.Text;

namespace Lm.WebMVC.Controllers
{
    public class ClassisPlanController : Controller
    {

        #region 视图

        // GET: ClassisPlan
        public ActionResult Index()
        {

            string FatherCode = "004"; //根据数据库来的

            StringBuilder rsbCaseContent = new StringBuilder();

            var cBLL = new BLL_Project();
            var cMBLL = new BLL_Menu();

            var Mlist = cMBLL.GetMenu_B_ListByAll().Where(o=>o.menu_FatherId== FatherCode);

            //int iPageIndex = RequestParameters.Pint("Pid");//接收页面传值

            //int iTotalRecord = 0;

            //int iPageSize = Config.PageSize;

            //var list = cBLL.GetPageList(1, iPageSize, ref iTotalRecord); //分页不做了
            var list = cBLL.GetListByAll();

            ViewBag.SelectBtn = SelectBtn(Mlist.ToList());

            if (list.Count != 0) //没有的话就放回
            {
                this.GetCaseContent(ref rsbCaseContent, list);
            }

            ViewBag.CaseContent = rsbCaseContent;


            return View();
        }

        //详情内容
        public ActionResult Default()
        {
            //数据库007=联系我们
            string ContactUsCode = "007";
            int Couts = 8;
            StringBuilder sbSimilar = new StringBuilder();

            int id = RequestParameters.Pint("id");

            var cBLL = new BLL_Project();

            var model = cBLL.GetObjectById(id);
            if (model != null) //判断没有只有要提示
            {
                ViewBag.ProjectTitle = model.Project_Name.Trim();
                ViewBag.Year = model.Project_Start.ToString("yyyy");
                ViewBag.Client = model.Project_Location;
                ViewBag.Category = this.ProType(model.Project_Type);
                ViewBag.Contribution = model.Project_People;//设计者
                ViewBag.CompletionTime = this.ComTime(model.Project_Start, model.Project_End) + "天";//完成时间
                ViewBag.Description = model.Project_Description;

                ViewBag.ContactUsUrl = this.Contact(true, ContactUsCode);
                ViewBag.ContactUs = this.Contact(false, ContactUsCode);

                this.GetSimilar(ref sbSimilar, model.Project_Type, Couts);

                ViewBag.ThisProImgs = ThisProImgsHtml(model.Id);

                ViewBag.SimilarPro = sbSimilar; //类似项目
            }

            return View();
        }

        #endregion

        #region 方法

        #region 详情ActionResult 
        /// <summary>
        /// 根据项目Code得到项目对应项目类型
        /// </summary>
        /// <param name="ProTypeID">项目Code</param>
        /// <returns></returns>
        private string ProType(string ProTypeID)
        {
            var cBLL = new BLL_Menu();
            return cBLL.GetObjectByMenuCode(ProTypeID).menu_Name.Trim();
        }


        /// <summary>
        /// 计算完成时间
        /// </summary>
        /// <returns></returns>
        /// <param name="dSTime">开始时间</param>
        /// <param name="dETime">结束时间</param>
        private int ComTime(DateTime dSTime, DateTime dETime)
        {
            var ts = dETime.Subtract(dSTime);
            if (ts.Days <= 0)
            {
                return 0;
            }
            else
            {
                return ts.Days;
            }
        }

        /// <summary>
        /// 判断bIsUrl获取URL或者Name
        /// </summary>
        /// <param name="bIsUrl">是否</param>
        /// <param name="sMenuCode">code</param>
        /// <returns></returns>
        private string Contact(bool bIsUrl, string sMenuCode)
        {
            string rString = "";
            var cBLL = new BLL_Menu();
            var tempMode = cBLL.GetObjectByMenuCode(sMenuCode);
            if (bIsUrl)
            {
                rString = tempMode.menu_Link;
            }
            else
            {
                rString = tempMode.menu_Name;
            }
            return rString;
        }

        /// <summary>
        /// 获取类似项目
        /// </summary>
        /// <param name="rsbSimilar">输出的html</param>
        /// <param name="ProCode">项目类型</param>
        /// <param name="iCounts">项目数</param>
        /// <returns></returns>
        private void GetSimilar(ref StringBuilder rsbSimilar, string ProCode, int iCounts)
        {
            var cBLL = new BLL_Project();
            var list = cBLL.GetListByAll();

            Random ran = new Random();

            int NameLenth = 20;

            var similarList = list.Where(o => o.Project_Type == ProCode).ToList();
            var tempCount = similarList.Count;
            if (tempCount >= iCounts)
            {
                similarList = similarList.Take(iCounts).ToList();
            }
            else
            {
                var otherList = list.Where(o => o.Project_Type != ProCode).OrderByDescending(o => o.Create_date).ToList();

                var otherCount = iCounts - tempCount;

                otherList = otherList.Take(otherCount).ToList();

                similarList = similarList.Union(otherList).ToList();
            }
            for (int i = 0; i < similarList.Count; i++)
            {
                rsbSimilar.Append("<li>");

                rsbSimilar.Append("<div class=\"featured-projects\">");

                rsbSimilar.Append("<div class=\"featured-projects-image\">");

                rsbSimilar.Append("<a href=\"/ClassisPlan/Default?_r=" + ran.NextDouble() + "&id=" + similarList[i].Id + "\" target=\"main\"><img src=\"/img/demo/300x200.png\" class=\"imgOpa\" alt=\"\"></a>");

                rsbSimilar.Append("</div>");

                rsbSimilar.Append("<div class=\"featured-projects-content\">");

                var pname = similarList[i].Project_Name.Length >= NameLenth ? similarList[i].Project_Name.Substring(0, NameLenth) + "..." : similarList[i].Project_Name;

                rsbSimilar.Append("<h1><a href=\"/ClassisPlan/Default?_r=" + ran.NextDouble() + "&id=" + similarList[i].Id + "\" target=\"main\">" + pname + "</a></h1>");

                rsbSimilar.Append("<p>" + similarList[i].Project_Location + "</p>");

                rsbSimilar.Append("</div>");

                rsbSimilar.Append("</div>");

                rsbSimilar.Append("</li>");
            }

        }

        private string ThisProImgsHtml(int id)
        {
            var cBll = new BLL_UploadFile();

            var list = cBll.GetListByProId(id);

            string html = "";

            if (list.Count != 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    html += "<li>";
                    html += "<img src='" + list[i].Path + "' alt= '" + list[i].FileName + "' >";
                    html += "</li>";
                }
            }
            else
            {
                html += "<li>";
                html += "<img src=\"/img/demo/300x200.png\" alt=\"\" >";
                html += "</li>";
            }

            return html;
        }
        #endregion

        #region 全部案例 ActionResult Index
        /// <summary>
        /// 项目
        /// </summary>
        /// <param name="rsbCaseContent"></param>
        /// <param name="list"></param>
        private void GetCaseContent(ref StringBuilder rsbCaseContent, IList<tb_Project> list)
        {
            Random ran = new Random();

            int NameLenth = 20;

            for (int i = 0; i < list.Count; i++)
            {
                rsbCaseContent.Append("<div class=\"boxportfolio3 item " + list[i].Project_Type.Trim() + "\">");

                rsbCaseContent.Append("<div class=\"boxcontainer\">");

                rsbCaseContent.Append("<img src=\"/img/temp/masonry/1.jpg\" alt=\"\">");

                rsbCaseContent.Append("<div class=\"roll\">");

                rsbCaseContent.Append("<div class=\"wrapcaption\">");

                rsbCaseContent.Append("<a href=\"/ClassisPlan/Default?_r=" + ran.NextDouble() + "&id=" + list[i].Id + "\" target=\"main\"><i class=\"icon-link captionicons\"></i></a>");

                //rsbCaseContent.Append("<a data-gal="prettyPhoto[gallery1]" href="~/ img / temp / masonry / 1.jpg" title="La Chaux De Fonds"><i class="icon - zoom -in captionicons"></i></a>");

                rsbCaseContent.Append("</div>");

                rsbCaseContent.Append("</div>");

                var pname = list[i].Project_Name.Length >= NameLenth ? list[i].Project_Name.Substring(0, NameLenth) + "..." : list[i].Project_Name;

                rsbCaseContent.Append("<h1><a href=\"/ClassisPlan/Default?_r=" + ran.NextDouble() + "&id=" + list[i].Id + "\" target=\"main\">" + pname + "</a></h1>");

                rsbCaseContent.Append("<p>" + list[i].Project_Location + "</p>");

                rsbCaseContent.Append("</div>");

                rsbCaseContent.Append("</div>");
            }

        }


        private string SelectBtn(IList<tb_Menu_B> list)
        {
            string html = "";
            if (list.Count != 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    html += "<li><a href=\"\" id=" + list[i].menu_Code.Trim() + " data-filter=\"." + list[i].menu_Code.Trim() + "\"><i class=\"icon icon-th-large\"></i>&nbsp; " + list[i].menu_Name.Trim() + "</a></li>";
                }
            }

            return html;
        }
        #endregion

        #region 公用方法

        #endregion

        #endregion
    }
}
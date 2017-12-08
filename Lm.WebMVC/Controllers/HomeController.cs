using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Model;
using Lm.BLL;
using System.Text;
using Lm.CommonLib;

namespace Lm.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            #region 项目
            StringBuilder sbRP = new StringBuilder();
            var cBll = new BLL_Project();
            var list = cBll.GetListByAll().Skip(4).Take(9).ToList();
            if (null != list)
            {
                GetTake5Work(ref sbRP, list);
            }

            ViewBag.RecentProjects = sbRP;
            #endregion

            #region 最新新闻
            StringBuilder sbNews = new StringBuilder();
            var NewsBll = new BLL_Articles();
            //数据库 17：新闻
            var Newslist = NewsBll.GetListByAll().Where(o => o.ArticlesType == 17).Take(4).ToList();
            GetTop4News(ref sbNews, Newslist);
            ViewBag.HotNews = sbNews;
            #endregion

            #region 业务范围(服务范围) 
            //Fid:8 都是服务范围 共5个取4个
            StringBuilder sbService = new StringBuilder();
            var ServiceBll = new BLL_Dictionary();
            var Servicelist = ServiceBll.GetListByAll().Where(o => o.FId == 8).Take(4).OrderByDescending(o => o.Sort).ToList();
            Get4Service(ref sbService, Servicelist);
            ViewBag.Service = sbService;
            #endregion

            #region 首页公司信息
            string sTel = Config.CsTel;
            string sAdder = Config.sAdder;
            string CInfoD_T = Config.CInfoD_T;
            string CInfoD = Config.CInfoD;
            string CompanyName = Config.CompanyName;

            var CInfoBll = new BLL_CompanyInfo();
            td_CompanyInfo CInfo = new td_CompanyInfo();
            CInfo = CInfoBll.GetTopCInfo();


            if (CInfo != null)
            {
                sTel = CInfo.Company_Call;
                sAdder = CInfo.Company_Address;
                CInfoD_T = CInfo.Company_Description_T;
                CInfoD = CInfo.Company_description;
            }
            ViewBag.Tel = sTel;
            ViewBag.Address = sAdder;
            ViewBag.CInfoD = CInfoD;
            ViewBag.CInfoD_T = CInfoD_T;
            ViewBag.CompanyName = CompanyName;

            #endregion
            return View();
        }

        public ActionResult Content()
        {
          

            return View();
        }

        /// <summary>
        /// 最新作品
        /// </summary>
        /// <returns></returns>
        public JsonResult LatestWork()
        {
            var sReturnModel = new ReturnListModel();

            var cBll = new BLL_Project();
            var list = cBll.GetListByAll().Take(4);

            sReturnModel.ErrorType = 1;
            sReturnModel.Data = from o in list
                                select new
                                {
                                    name = o.Project_Name,
                                    location = o.Project_Location,
                                    people = o.Project_People,
                                    start = string.Format("{0:D}", o.Project_Start),
                                    end = string.Format("{0:D}", o.Project_End),
                                    description = o.Project_Description,
                                };
            return Json(sReturnModel);
        }

        /// <summary>
        /// 最近项目，后期还需要修改
        /// </summary>
        /// <param name="sbRP"></param>
        /// <param name="listRp"></param>
        public void GetTake5Work(ref StringBuilder sbRP, IList<td_Project> listRp)
        {
            if (listRp.Count > 0)
            {
                for (int i = 0; i < listRp.Count; i++)
                {
                    sbRP.Append("<li>");
                    sbRP.Append("<p>");
                    sbRP.Append("<img src=\"/img/demo/300x200.png\" class=\"imgproject\" alt=\"\">");
                    sbRP.Append("<b>" + listRp[i].Project_Name + "</b>" + listRp[i].Project_Description + "<a href=\"#\">[...]</a>");
                    sbRP.Append("</p>");
                    sbRP.Append("<p>");
                    sbRP.Append("<a href=\"#fakelink\" class=\"btn btn-primary\"><i class=\"icon-share-alt\"></i>查看类似项目</a>");
                    sbRP.Append("</p>");
                    sbRP.Append("</li>");
                }
            } else
            {
                sbRP.Append("");
            }
        }

        /// <summary>
        /// 前四条新闻
        /// </summary>
        /// <param name="sbNews"></param>
        /// <param name="listNews"></param>
        public void GetTop4News(ref StringBuilder sbNews, IList<td_Articles> listNews)
        {
            if (listNews.Count > 0)
            {
                for (int i = 0; i < listNews.Count; i++)
                {

                    sbNews.Append("<div class=\"span3\">");

                    sbNews.Append("<article>");

                    sbNews.Append("<img src=\"/img/temp/masonry/helsinki-94309_640.jpg\" alt=\"\" class=\"imgOpa\">");

                    sbNews.Append("<div class=\"date\">");

                    sbNews.Append("<span class=\"day\">" + listNews[i].Creationtime.Day + "</span>");

                    var month = listNews[i].Creationtime.Month;

                    sbNews.Append("<span class=\"month\">" + ToStringMonth(month) + "</span>");

                    sbNews.Append("</div>");

                    sbNews.Append("<h4><a href = \"bloghome.html\" >" + listNews[i].ArticlesTitle + "</a></h4>");

                    if (listNews[i].ArticlesContent.Length > 100)
                    {
                        sbNews.Append("<p>" + listNews[i].ArticlesContent.Substring(0, 100) + "<a href = \"#\"class=\"read-more\">阅读更多<i class=\"icon-angle-right\"></i></a> </p>");
                    }
                    else
                    {
                        sbNews.Append("<p>" + listNews[i].ArticlesContent + "<a href = \"#\"class=\"read-more\">阅读更多<i class=\"icon-angle-right\"></i></a> </p>");
                    }


                    sbNews.Append(" </article>");

                    sbNews.Append("</div>");

                }
            }
            else
            {
                sbNews.Append("");
            }
        }

        /// <summary>
        /// 业务范围也就是服务范围
        /// </summary>
        /// <param name="sbService"></param>
        /// <param name="listService"></param>
        public void Get4Service(ref StringBuilder sbService, IList<td_Dictionary> listService)
        {
            if (listService.Count > 0)
            {
                for (int i = 0; i < listService.Count; i++)
                {
                    sbService.Append("<div class=\"span3\">");

                    sbService.Append("<div class=\"grey-box-icon\">");

                    sbService.Append("<div class=\"icon-box-top grey-box-icon-pos\">");

                    sbService.Append("<i class=\"fontawesome-icon medium circle-white center " + listService[i].IconImg + "\"></i>");

                    sbService.Append("</div>");

                    sbService.Append("<h4>"+listService[i].Name+"</h4>");

                    sbService.Append("<p>" + listService[i].Description + "</ p> ");

                    sbService.Append("<p><a href = \"#\" style = \"font-weight: bold;\" > 阅读更多 →</a > </p >");

                    sbService.Append("</div>");

                    sbService.Append(" </div>");

                }
            }
            else
            {
                sbService.Append("");
            }
        }

        /// <summary>
        /// 数字月份 转 英文字
        /// </summary>
        /// <param name="iMonth"></param>
        /// <returns></returns>
        public string ToStringMonth(int iMonth)
        {
            string sMonth = "";
            switch (iMonth)
            {
                case 1:
                    sMonth = "Jan";
                    break;
                case 2:
                    sMonth = "Feb";
                    break;
                case 3:
                    sMonth = "Mar";
                    break;
                case 4:
                    sMonth = "Apr";
                    break;
                case 5:
                    sMonth = "May";
                    break;
                case 6:
                    sMonth = "Jun";
                    break;
                case 7:
                    sMonth = "Jul";
                    break;
                case 8:
                    sMonth = "Aug";
                    break;
                case 9:
                    sMonth = "Sep";
                    break;
                case 10:
                    sMonth = "Oct";
                    break;
                case 11:
                    sMonth = "Nov";
                    break;
                case 12:
                    sMonth = "Dec";
                    break;
            }
            return sMonth;
        }

        /// <summary>
        /// 作品第4条到第9条
        /// </summary>
        /// <returns></returns>
        public JsonResult Take5Work()
        {
            var sReturnModel = new ReturnListModel();

            var cBll = new BLL_Project();
            var list = cBll.GetListByAll().Skip(4).Take(9);

            sReturnModel.ErrorType = 1;
            sReturnModel.Data = from o in list
                                select new
                                {
                                    id=o.Id,
                                    name = o.Project_Name,
                                    //location = o.Project_Location,
                                    //people = o.Project_People,
                                    //start = string.Format("{0:D}", o.Project_Start),
                                    //end = string.Format("{0:D}", o.Project_End),
                                    description = o.Project_Description,
                                };
            return Json(sReturnModel);
        }

        public JsonResult News()
        {
            var sReturnModel = new ReturnListModel();

            var cBll = new BLL_News();
            var list = cBll.GetListByAll().Take(3);

            sReturnModel.ErrorType = 1;
            sReturnModel.Data = from o in list
                                select new
                                {
                                    id = o.Id,
                                    title = o.News_Title,
                                    date = o.News_Date.ToString("yyyy/MM/dd"),
                                    people = o.News_People,
                                    content = o.News_Content,
                                };
            return Json(sReturnModel);
        }
    }
}
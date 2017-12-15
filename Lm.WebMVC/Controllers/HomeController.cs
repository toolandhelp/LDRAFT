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
            #region 首页公司信息
            string sTel = Config.CsTel;
            string sAdder = Config.Adder;
            string CInfoD = Config.CInfoD;
            string CompanyName = Config.CompanyName;
            string Copyright = Config.Copyright;

            var CInfoBll = new BLL_CompanyInfo();
            var  CInfo = new tb_CompanyInfo();
            CInfo = CInfoBll.GetListByAll().OrderByDescending(o=>o.Id).FirstOrDefault();


            if (CInfo != null)
            {
                sTel = CInfo.Company_Call;
                sAdder = CInfo.Company_Address;
                CInfoD = CInfo.Company_description;
                CompanyName = CInfo.Company_Name;
                Copyright = CInfo.Company_copyright;

            }
            ViewBag.Tel = sTel;
            ViewBag.Address = sAdder;
            ViewBag.CInfoD = CInfoD;
            ViewBag.CompanyName = CompanyName;
            ViewBag.Copyright = Copyright;
            #endregion
            return View();
        }

        public ActionResult Content()
        {

            #region 公司术语
            string CInfoD_T = Config.CInfoD_T;
            string CInfoD = Config.CInfoD;
            string CEmail = Config.Emails;

            var CInfoBll = new BLL_CompanyInfo();
            var CInfo = new tb_CompanyInfo();
            CInfo = CInfoBll.GetListByAll().OrderByDescending(o => o.Id).FirstOrDefault();


            if (CInfo != null)
            {
                CInfoD_T = CInfo.Company_Description_T;
                CInfoD = CInfo.Company_description;
                CEmail = CInfo.Company_Email;
            }
            ViewBag.CInfoD = CInfoD;
            ViewBag.CInfoD_T = CInfoD_T;
            ViewBag.CEmail = CEmail;
            #endregion

            #region 最近项目
            //修改需根据数据库来
            string CaseCode = "004";
            StringBuilder sbRP = new StringBuilder();
            var cBll = new BLL_Project();
            var list = cBll.GetListByAll().OrderByDescending(o=>o.Project_Start).Take(Config.RecentProjects).ToList();
            if (null != list)
            {
                GetTakeWork(ref sbRP, list,CaseCode);
            }

            ViewBag.RecentProjects = sbRP;
            #endregion

            #region 行业动态
            StringBuilder sbNews = new StringBuilder();
            var NewsBll = new BLL_NewsCenter();
            //数据库 行业动态
            var Newslist = NewsBll.GetListByAll().Where(o => o.ArticlesType == Config.IndustryDynamicsID).Take(Config.IndustryDynamicsCount).ToList();
            GetTopNews(ref sbNews, Newslist);
            ViewBag.HotNews = sbNews;
            #endregion

            #region 4种描述 
            //
            StringBuilder sbService = new StringBuilder();
            var ServiceBll = new BLL_Dictionary();
            var Servicelist = ServiceBll.GetListByAll().Take(4).OrderByDescending(o => o.Sort).ToList();
            GetService(ref sbService, Servicelist);
            ViewBag.Service = sbService;
            #endregion

            return View();
        }

        /// <summary>
        /// 页头动画切换《调用相机滑块》
        /// </summary>
        /// <returns></returns>
        public JsonResult LatestWork()
        {
            var sReturnModel = new ReturnListModel();

            var cBll = new BLL_Project();
            var list = cBll.GetListByAll().Where(o=>o.Project_IsShow==true);
            if (list.ToList().Count >= (int)Config.TopProjects)
            {
                list.Take(Config.TopProjects);
            }

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
        /// 最近项目
        /// </summary>
        /// <param name="sbRP"></param>
        /// <param name="listRp"></param>
        public void GetTakeWork(ref StringBuilder sbRP, IList<tb_Project> listRp, string sCaseCode)
        {
            int temp = 150;

            Random ran = new Random();

            var BLLMenu = new BLL_Menu();
            var tempMenu = BLLMenu.GetMenu_B_ListByAll().Where(o => o.menu_FatherId == sCaseCode);



            if (listRp.Count > 0)
            {
                for (int i = 0; i < listRp.Count; i++)
                {
                    // double n = ran.NextDouble();

                    sbRP.Append("<li>");
                    sbRP.Append("<p>");
                    sbRP.Append("<img src=\"/img/demo/300x200.png\" class=\"imgproject\" alt=\"\">");

                    var iLength = listRp[i].Project_Description.Length;
                    var sCentent = iLength >= temp ? listRp[i].Project_Description.Substring(0, temp) : listRp[i].Project_Description;

                    sbRP.Append("<b>" + listRp[i].Project_Name + "</b>" + sCentent + "<a href=\"/ClassisPlan/Default?_r=" + ran.NextDouble() + "&id=" + listRp[i].Id + "\" target=\"main\" >[...]</a>");
                    sbRP.Append("</p>");
                    sbRP.Append("<p>");

                    var tempmodel = tempMenu.Where(o => o.menu_Code == listRp[i].Project_Type).FirstOrDefault();

                    sbRP.Append("<a href=\"" + tempmodel.menu_Link + "?_r=" + ran.NextDouble() + "\" class=\"btn btn-primary\" target=" + tempmodel.menu_Target + "><i class=\"icon-share-alt\"></i>查看类似项目</a>");
                    sbRP.Append("</p>");
                    sbRP.Append("</li>");
                }
            }
            else
            {
                sbRP.Append("");
            }
        }

        /// <summary>
        /// 前四条
        /// </summary>
        /// <param name="sbNews"></param>
        /// <param name="listNews"></param>
        public void GetTopNews(ref StringBuilder sbNews, IList<tb_NewsCenter> listNews)
        {
            int temp = 100;
            Random ran = new Random();

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

                    sbNews.Append("<h4><a  href=\"/NewConent/Default?_r=" + ran.NextDouble() + "&id=" + listNews[i].ID + "\" target=\"main\">" + listNews[i].ArticlesTitle + "</a></h4>");


                    var iLength = listNews[i].ArticlesContent.Length;
                    var sCentent = iLength >= temp ? listNews[i].ArticlesContent.Substring(0, temp) + "..." : listNews[i].ArticlesContent;

                    sbNews.Append("<p>" + sCentent + "<a href=\"/NewConent/Default?_r=" + ran.NextDouble() + "&id=" + listNews[i].ID + "\" class=\"read-more\" target=\"main\">阅读更多<i class=\"icon-angle-right\"></i></a> </p>");

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
        public void GetService(ref StringBuilder sbService, IList<tb_Dictionary> listService)
        {
            int temp = 50;
            Random ran = new Random();

            if (listService.Count > 0)
            {
                for (int i = 0; i < listService.Count; i++)
                {
                    sbService.Append("<div class=\"span3\">");

                    sbService.Append("<div class=\"grey-box-icon\">");

                    sbService.Append("<div class=\"icon-box-top grey-box-icon-pos\">");

                    sbService.Append("<i class=\"fontawesome-icon medium circle-white center " + listService[i].IconImg + "\"></i>");

                    sbService.Append("</div>");

                    sbService.Append("<h4>" + listService[i].Name + "</h4>");

                    var iLength = listService[i].Description.Length;

                    var sCentent = iLength >= temp ? listService[i].Description.Substring(0, temp) : listService[i].Description;

                    sbService.Append("<p>" + sCentent + "</ p> ");

                    sbService.Append("<p><a href = \"" + listService[i].MenuLinkTemp + "?_r=" + ran.NextDouble() + "\" style = \"font-weight: bold;\" target=" + listService[i].Targets + " > 阅读更多 →</a > </p >");

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
        /// 最近项目 （暂时不用）
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

     

        [ValidateInput(false)]
        public JsonResult Question()
        {
            //0:失败；1:成功；2:数量限制
            var sReturnModel = new ReturnMessageModel();

            string name = RequestParameters.Pstring("name");
            string email = RequestParameters.Pstring("email");
            string message = RequestParameters.Pstring("comment");
            string txtValicode = RequestParameters.Pstring("qcode");

            #region 参数检查
            try
            {
                if (txtValicode != Session["Qcaptcha"].ToString())
                {
                    sReturnModel.ErrorType = 0;
                    sReturnModel.MessageContent = "验证码错误!刷新,再试一次.";
                    return Json(sReturnModel);
                }
            }
            catch (Exception)
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "验证码错误!刷新,再试一次.";
                return Json(sReturnModel);
            }

            if (name.Length < 1)
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "信息发送失败!姓名不能为空.";
                return Json(sReturnModel);
            }

            if (email.Length < 1)
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "信息发送失败!邮箱不能为空.";
                return Json(sReturnModel);
            }

            if (message.Length < 1)
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "信息发送失败!信息内容不能为空.";
                return Json(sReturnModel);
            }
            if (name.Length < 1)
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "信息发送失败!姓名不能为空.";
                return Json(sReturnModel);
            }

            if (email.Length < 1)
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "信息发送失败!邮箱不能为空.";
                return Json(sReturnModel);
            }

            if (message.Length < 1)
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "信息发送失败!信息内容不能为空.";
                return Json(sReturnModel);
            }
            #endregion  

            var cBll = new BLL_ClientMessage();
            var model = new tb_ClientMessage();
            model.Name = HttpUtility.HtmlEncode(name);
            model.Email = HttpUtility.HtmlEncode(email);
            model.Message = HttpUtility.HtmlEncode(message);
            model.MessType = true; //0:问题信息
            model.SubmitDate = System.DateTime.Now;

            //当前问题今天所发的记录数
            var list = cBll.GetListByAll().Where(o => o.Email == email &&
            o.SubmitDate.ToString("yyyy-MM-dd") == System.DateTime.Now.ToString("yyyy-MM-dd") &&
            o.MessType == true).Count();

            if (list >= 5)
            {
                sReturnModel.ErrorType = 2;
                sReturnModel.MessageContent = "信息发送失败!每天只允许发送<span style='color:red;'>5条</span>问题.";
                return Json(sReturnModel);
            }


            if (cBll.AddOrUpdate(model))
            {
                sReturnModel.ErrorType = 1;
                sReturnModel.MessageContent = "谢谢你!你的信息已经发送了.";

            }
            else
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "信息发送失败!请刷新页面重试.";
            }

            return Json(sReturnModel);
        }

        /// <summary>
        /// 验证码(发送信息)
        /// </summary>
        /// <returns></returns>
        public ActionResult CodeImg()
        {
            var itemValidateCode = new CommonLib.ValidateCode();
            string code = itemValidateCode.CreateValidateCode(4);
            Session["Qcaptcha"] = code;
            byte[] bytes = itemValidateCode.CreateValidateGraphic(code);

            return File(bytes, @"image/jpg");
        }

        /// <summary>
        /// 菜单导航
        /// </summary>
        /// <returns></returns>
        public JsonResult TopNav()
        {

            var cBll = new BLL_Menu();
            var list = cBll.GetMenu_B_ListByAll().Where(o => o.menu_Stage == (int)StageMode.Normal).ToList();

            if (list != null)
            {
                return Json(list.Select(itemNode => new TreeModel
                {
                    Id = itemNode.menu_Code,
                    Pid = itemNode.menu_FatherId,
                    Title = itemNode.menu_Name,
                    Target = itemNode.menu_Target,
                    Url = itemNode.menu_Link

                }));
            }
            return Json("[]");
        }

        public JsonResult BottomNav()
        {
            var cBll = new BLL_Menu();
            var list = cBll.GetMenu_B_ListByAll().Where(o => o.menu_Stage == (int)StageMode.Normal);
            list = list.Where(o => o.menu_Link_temp != null && o.menu_Link_temp != "").ToList();

            if (list != null)
            {
                return Json(list.Select(itemNode => new TreeModel
                {
                    Id = itemNode.menu_Code,
                    Pid = itemNode.menu_FatherId,
                    Title = itemNode.menu_Name,
                    Target = itemNode.menu_Target,
                    Url = itemNode.menu_Link_temp

                }));
            }
            return Json("[]");
        }
    }
}
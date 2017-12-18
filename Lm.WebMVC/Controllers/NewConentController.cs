
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
    public class NewConentController : Controller
    {

        #region 视图

        // GET: NewConent
        public ActionResult Index()
        {
           
            var cBLL = new BLL_NewsCenter();

            StringBuilder RevertMostHtml = new StringBuilder();
            //获取所有
            var list = cBLL.GetListByAll().ToList();
            //回复数
            var CommLIist = cBLL.GetNCommentListByAll().ToList();

            this.RevertMostHtml(ref RevertMostHtml, CommLIist, list);
            ViewBag.Revertmost = RevertMostHtml;
            //最新一条
            var model = list.OrderByDescending(o => o.ArticlesDate).FirstOrDefault();

            ViewBag.Latest = this.LatestRecord(model);
            return View();
        }

        public ActionResult List()
        {
            var typeId = RequestParameters.Pstring("mCode");
            ViewBag.TypeId = typeId;
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
            var Id =  RequestParameters.Pint("Nid");

            var cBLL = new BLL_NewsCenter();
            var model = cBLL.GetObjectById(Id);
            if (model != null)
            {
                ViewBag.Title = model.ArticlesTitle.Trim();
                ViewBag.Yeas = model.ArticlesDate.ToString("yyyy/MM/dd");
                ViewBag.Content = model.ArticlesContent;

                //标签

                ViewBag.Tags = this.Tags(model.tags);

                //是否开启评论
                if (model.IsComment == true)
                {
                    ViewBag.CommentCount = this.CommentCounts(model.ID);

                    ViewBag.CommentContent = this.CommentContents(model.ID);

                }
                else
                {
                    ViewBag.CommentCount = "<span class=\"pull-right\" id='CommentsShow'isShow='false'></span>";

                    ViewBag.CommentContent = "<div class=\"alert alert-info\"><strong> 抱歉!</strong> 此内容为开启评论功能! </div>";
                }
            }


            return View();
        }
        #endregion

        #region 方法

        /// <summary>
        /// 最新一条新闻或者动态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string LatestRecord(tb_NewsCenter model)
        {
            string html = "";
            int ContentLenth = 300;

            Random ran = new Random();

            if (model != null)  //判断没有只有要提示
            {

                html += "<h2>" + model.ArticlesTitle + "</h2>";

                html += "<p >";

                if (model.ImgPath != null && model.ImgPath != "")
                {
                    html += "<img src= \"" + model.ImgPath + "\" class=\"pull-left paddingright\" alt=\"\" style=\"margin-top:10px;\">";
                }
                else
                {
                    html += "<img src= \"/img/demo/news.jpg\" class=\"pull-left paddingright\" alt=\"\" style=\"margin-top:10px;\">";
                }

                html += model.ArticlesContent.Length > ContentLenth ? model.ArticlesContent.Substring(0, ContentLenth) + "..." : model.ArticlesContent;

                html += "</p>";

                html += "<p>";

                html += "<a class=\"btn btn-primary btn-large\" href=\"/NewConent/Default?_r="+ ran.NextDouble() + "&Nid="+model.ID+"\"  target=\"main\">阅读更多 »</a>";

                html += "</p>";
            }
            return html;
        }

        /// <summary>
        ///  得到评论 html
        /// </summary>
        /// <param name="CommentID">评论的ID =新闻或行业动态Id</param>
        /// <returns></returns>
        private string CommentCounts(int CommentID)
        {
            string html = "";
            int count = 0;
            var cBll = new BLL_CommentMessage();

            var ComCount = cBll.GetListByNewId(CommentID).Where(o => o.IsDelete == false && o.IsDisplay == true).ToList();
            if (ComCount.Count > 0) {
                count = ComCount.Count;
            }

            html += "<span class=\"pull-right\" id='CommentsShow'isShow='true'>";
            html += "<a href=\"#comments\">";
            html += "<span>"+ count + "</span> 条评论";
            html += "</a>";
            html += "</span>";

            return html;
        }

        private string CommentContents(int CommentID)
        {
            string html = "";
            var cBll = new BLL_CommentMessage();

            //最近的排最上面
            //必须是没有删除的并且是显示的
            var ComCount = cBll.GetListByNewId(CommentID).Where(o => o.IsDelete == false && o.IsDisplay == true).
                OrderByDescending(o => o.SubmitDate).ToList();
            if (ComCount.Count > 0)
            {
                for (int i = 0; i < ComCount.Count; i++)
                {
                    html += "<ul class=\"media\">";
                    html += "<li class=\"comment\">";

                    html += "<article class=\"comment\">";

                    html += " <footer>";

                    html += "<span class=\"comment-author vcard\">";

                    html += "<span class=\"pull-left\">";
                    html += "<img alt= \"\" src=\"/img/CUser/CommLoglo.jpg\" class=\"avatar avatar-54 photo\" height=\"54\" width=\"54\">";
                    html += "</span>";

                    html += "<cite class=\"fn\"><a href= \"javascript:;\" >" + ComCount[i].Name + "</a></cite>";
                    html += "<span class=\"says\">&nbsp;&nbsp;&nbsp;&nbsp;发表于：&nbsp;&nbsp;&nbsp;</span>";
                    html += "</span>";

                    html += "<span class=\"comment-meta commentmetadata\">";

                    html += "<a href= \"javascript:;\" >" + ComCount[i].SubmitDate.ToLocalTime() + "</a>";

                    html += " </span>";

                    html += "</footer>";

                    html += "<div class=\"comment-content\">";

                    html += " <p>" + ComCount[i].Message.Trim() + "</p>";

                    html += "</div>";

                    html += "<div class=\"reply\"></div>";

                    html += "</article>";


                    html += "</li>";

                    html += "</ul>";


                }


            }
            else
            {
                html = "<div class=\"alert alert-info\"><strong> 暂无评论!</strong> </div>";
            }

            return html;
        }

        /// <summary>
        /// 所属 标签
        /// </summary>
        /// <param name="Tags"></param>
        /// <returns></returns>
        private string Tags(string Tags)
        {
            string html = "";

            if (Tags != null && Tags != "")
            {
                var tempArry = Tags.Split(',');
                for (int i = 0; i < tempArry.Length; i++)
                {
                    html += "<a href=\"javascript:;\" rel=\"tag\" > " + tempArry[i] + " </a>、";
                }
                html = html.TrimEnd('、');
            }

            return html;
        }

        /// <summary>
        ///  各前两条的留言最多的记录
        /// </summary>
        /// <param name="ALLHtml">返回的HTML</param>
        /// <param name="list">v_NewsCenterComment list 已经查看，并且允许留言</param>
        /// <param name="NewsCenterList">tb_NewsCenter list</param>
        private void RevertMostHtml(ref StringBuilder ALLHtml, List<v_NewsCenterComment> list, List<tb_NewsCenter> NewsCenterList)
        {
            var cBLL = new BLL_Menu();
            Random ran = new Random();
            string InduCode = "005001", NewsCode = "005002";

            var Namelist = cBLL.GetMenu_B_ListByAll();
            //新闻动态
            var NewsList = list.Where(o => o.ArticlesType == NewsCode && o.IsDisplay == true).ToList();
            // 行业动态
            var IndustryList = list.Where(o => o.ArticlesType == InduCode && o.IsDisplay == true).ToList();

            //var temp = (from v in IndustryList select v.ID).Distinct().ToList();

            //分组统计行业新闻
            this.test(ref ALLHtml, NewsList, Namelist, NewsCenterList);

            //分组统计行业动态
            this.test(ref ALLHtml, IndustryList, Namelist, NewsCenterList);

        }


        private void test(ref StringBuilder ALLHtml,List<v_NewsCenterComment> AllContentsList,IList<tb_Menu_B> Namelist,List<tb_NewsCenter> NewsCenterList)
        {
            int contentLenth = 30;
            Random ran = new Random();

            //分组统计行业动态
            var q1 = from p in AllContentsList
                     group p by p.ID into g
                     select new Temp
                     {
                         Key= (int)g.Key,
                         Count = g.Count()
                     };

            var Intop2 = q1.OrderByDescending(o => o.Count).Take(2);

            if (Intop2.Count() < 2)
            {
                var tempq = from p in NewsCenterList
                            orderby p.ArticlesDate descending
                            select new Temp
                            {
                                Key = p.ID,
                                Count = 0
                            };
                var tempcount = 2 - Intop2.Count();
                tempq = tempq.Take(tempcount);
                Intop2 = Intop2.Union(tempq);
            }

            ALLHtml.Append("<div class=\"row-fluid\">");

            foreach (var item in Intop2)
            {
                var temp = NewsCenterList.Where(o => o.ID == item.Key).FirstOrDefault();
                if (temp != null)
                {
                    // ALLHtml.Append("<div class=\"row-fluid\">");
                    ALLHtml.Append("<div class=\"span6\">");
                    ALLHtml.Append("<div class=\"boxblog\">");

                    var tempTitle = temp.ArticlesTitle.Trim().Length > contentLenth ? temp.ArticlesTitle.Trim().Substring(0, contentLenth) + "..." : temp.ArticlesTitle.Trim();

                    ALLHtml.Append("<h5><a href=\"/NewConent/Default?_r=" + ran.NextDouble() + "&Nid=" + temp.ID + "\" target=\"main\">" + tempTitle + "</a></h5>");
                    ALLHtml.Append("<p class=\"small datepost\">");
                    ALLHtml.Append("发布于 : " + temp.ArticlesDate.ToString("yyyy-MM-dd") + "");
                    ALLHtml.Append("<span class=\"floatright\" title=\"" + item.Count + "条留言\"><a href=\"/NewConent/Default?_r=" + ran.NextDouble() + "&Nid=" + temp.ID + "#comments\" target=\"main\"><img src=\"/img/comments.png\" alt = \"\">" + item.Count + "条留言</a></span>");

                    var tempType = Namelist.Where(o => o.menu_Code == temp.ArticlesType).FirstOrDefault().menu_Name;

                    var tempTypeURl = Namelist.Where(o => o.menu_Code == temp.ArticlesType).FirstOrDefault().menu_Link;

                    ALLHtml.Append("<i class=\"floatright icon-flag\" style=\"float:right;\">: <a href=\"" + tempTypeURl + "?_r=" + ran.NextDouble() + "&mCode=" + temp.ArticlesType + "\" target=\"main\">" + tempType + "</a></i></p>");
                    ALLHtml.Append("<div class=\"innerblogboxtwo\"><p>");

                    var tempContent = temp.ArticlesContent.Trim().Length > contentLenth * 4 + 10 ? temp.ArticlesContent.Trim().Substring(0, contentLenth * 4 + 10) + "..." : temp.ArticlesContent.Trim();

                    if (temp.ImgPath != null && temp.ImgPath != "")
                    {
                        ALLHtml.Append("<img width=\"150\" height=\"150\" src=\"" + temp.ImgPath + "\" class=\"attachment-thumbnail\" alt=\"\">" + tempContent + "");
                    }
                    else
                    {
                        ALLHtml.Append("<img width=\"150\" height=\"150\" src=\"/img/demo/300x200.png\" class=\"attachment-thumbnail\" alt=\"\">" + tempContent + "");
                    }
                    ALLHtml.Append("</p></div>");

                    ALLHtml.Append("<p class=\"continueread readmorebox\"><a <a href=\"/NewConent/Default?_r=" + ran.NextDouble() + "&Nid=" + temp.ID + "\" target=\"main\"> 继续阅读 </a></p></div></div>");
                }
            }
            ALLHtml.Append("</div>");
        }

        #endregion
    }

    public class Temp
    {
        public int Key { get; set; }
        public int Count { get; set; }
    }
}
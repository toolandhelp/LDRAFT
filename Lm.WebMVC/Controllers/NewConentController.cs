
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.CommonLib;
using Lm.BLL;
using Lm.Model;

namespace Lm.WebMVC.Controllers
{
    public class NewConentController : Controller
    {

        #region 视图

        // GET: NewConent
        public ActionResult Index()
        {
            var cBLL = new BLL_NewsCenter();

            string InduCode = "005001", NewsCode = "005002";

            //获取所有
            var list = cBLL.GetListByAll();

            // 行业动态
            var IndustryList = list.Where(o => o.ArticlesType == InduCode).ToList();
            //新闻动态
            var NewsList = list.Where(o=>o.ArticlesType==NewsCode).ToList();

            //最新一条

            var model = list.OrderByDescending(o => o.ArticlesDate).FirstOrDefault();

            ViewBag.Latest = this.LatestRecord(model);
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

            html += "<span class=\"pull-right\" id='CommentsShow'isShow='true'";
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
        #endregion

    }
}
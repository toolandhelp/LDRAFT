using Lm.CommonLib;
using Lm.Model;
using Lm.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lm.WebMVC.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// 日志记录
        /// </summary>
        log4net.Ext.IExtLog log = log4net.Ext.ExtLogManager.GetLogger("dblog");
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult SysLogin()
        {
            var sReturnModel = new ReturnMessageModel();
            int isCookieUp = RequestParameters.Pint("Remember");
            string txtValicode = RequestParameters.Pstring("Code");
            string UserName = RequestParameters.Pstring("UserName");
            string Password = RequestParameters.Pstring("Password");
            bool Remember = isCookieUp == 1;//记住密码
            var bll = new BLL_User();
            if (txtValicode != Session["CheckCode"].ToString())
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "验证码错误.";
                log.Warn(Utils.GetIP(), UserName, Request.Url.ToString(), "Login", "系统登录，登录结果：" + sReturnModel.MessageContent);
                return Json(sReturnModel);
            }
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "用户名或密码不正确.";
                log.Warn(Utils.GetIP(), UserName, Request.Url.ToString(), "Login", "系统登录，登录结果：" + sReturnModel.MessageContent);
                return Json(sReturnModel); ;
            }

            Password = CommonLib.HashEncrypt.BgPassWord(Password);
            var model = bll.LoginUsers(UserName, Password);
            if (model == null)
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "用户名或密码不正确.";
                log.Warn(Utils.GetIP(), UserName, Request.Url.ToString(), "Login", "系统登录，登录结果：" + sReturnModel.MessageContent);
                return Json(sReturnModel); ;
            }
            if (model != null)
            {
                if (model.Status != true)
                {
                    sReturnModel.ErrorType = 0;
                    sReturnModel.MessageContent = "该帐号已停用.";
                    log.Error(Utils.GetIP(), UserName, Request.Url.ToString(), "Login", "系统登录，登录结果：" + sReturnModel.MessageContent);
                    return Json(sReturnModel); ;
                }
                Session["LDRAFT_USERID"] = HashEncrypt.EncryptQueryString(model.Id.ToString());

                if (Remember)
                {
                    #region Cookie
                        
                    HttpCookie cookies = Request.Cookies["LDRAFT_USERINFO"];
                    if (cookies != null)
                    {
                        cookies.Expires = DateTime.Now.AddDays(-30);
                        Response.AppendCookie(cookies);
                    }
                    HttpCookie cookie = new HttpCookie("LDRAFT_USERINFO");
                    cookie.Values.Add("LDRAFT_USERNAME", HashEncrypt.EncryptQueryString(UserName));
                    cookie.Values.Add("LDRAFT_PASSWORD", HashEncrypt.EncryptQueryString(Password));
                    cookie.Values.Add("LDRAFT_REALNAME", HashEncrypt.EncryptQueryString(model.Name));
                    cookie.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Add(cookie);

                    #endregion

                }
                sReturnModel.ErrorType = 1;
                sReturnModel.MessageContent = "登录成功.";
                log.Info(Utils.GetIP(), UserName, Request.Url.ToString(), "Login", "系统登录，登录结果：" + sReturnModel.MessageContent);
                return Json(sReturnModel);
            }
            return Json(sReturnModel);
        }


        /// <summary>
        /// 验证码（登录）
        /// </summary>
        /// <returns></returns>
        public ActionResult CodeImg()
        {
            var itemValidateCode = new CommonLib.ValidateCode();
            string code = itemValidateCode.CreateValidateCode(4);
            Session["CheckCode"] = code;
            byte[] bytes = itemValidateCode.CreateValidateGraphic(code);

            return File(bytes, @"image/jpg");
        }
    }
}
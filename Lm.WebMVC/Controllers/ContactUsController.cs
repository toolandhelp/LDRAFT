
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.CommonLib;
using Lm.Model;
using Lm.BLL;

namespace Lm.WebMVC.Controllers
{
    public class ContactUsController : Controller
    {
        // GET: ContactUs
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public JsonResult MassPost()
        {
            var sReturnModel = new ReturnMessageModel();

            string name = RequestParameters.Pstring("name");
            string email = RequestParameters.Pstring("email");
            string message = RequestParameters.Pstring("message");
            string txtValicode = RequestParameters.Pstring("code");

            string temp = Session["captcha"].ToString();

            if (txtValicode != Session["captcha"].ToString())
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "验证码错误!刷新,再试一次.";
                return Json(sReturnModel);
            }

            var cBll = new Bll_ClientMessage();
            var model = new tb_ClientMessage();
            model.Name = HttpUtility.HtmlEncode(name);
            model.Email = HttpUtility.HtmlEncode(email);
            model.Message = HttpUtility.HtmlEncode(message);
            model.SubmitDate = System.DateTime.Now;

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
            Session["captcha"] = code;
            byte[] bytes = itemValidateCode.CreateValidateGraphic(code);

            return File(bytes, @"image/jpg");
        }
    }
}
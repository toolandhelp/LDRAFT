
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

            #region 联系方式
            string sAdder = Config.Adder;
            string sWorkTime = Config.WorkTime;
            string sEmail = Config.Emails;

            var CInfoBll = new BLL_CompanyInfo();
            var  model = new tb_CompanyInfo();
            model = CInfoBll.GetListByAll().FirstOrDefault();
            if (model != null)
            {
                sAdder = model.Company_Address;
                sWorkTime = model.Company_WorkTime;
                sEmail = model.Company_Email;
            }

            ViewBag.Adderss = sAdder;
            ViewBag.WorkTime = sWorkTime;
            ViewBag.Email = sEmail;

            #endregion

            return View();
        }

        [ValidateInput(false)]
        public JsonResult MassPost()
        {
            //0:失败；1:成功；2:数量限制
            var sReturnModel = new ReturnMessageModel();

            string name = RequestParameters.Pstring("name");
            string email = RequestParameters.Pstring("email");
            string message = RequestParameters.Pstring("message");
            string txtValicode = RequestParameters.Pstring("code");

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
            model.MessType = false; //0:联系信息
            model.SubmitDate = System.DateTime.Now;

            //当前邮件今天所发的记录数
            var list = cBll.GetListByAll().Where(o => o.Email == email &&
            o.SubmitDate.ToString("yyyy-MM-dd") == System.DateTime.Now.ToString("yyyy-MM-dd") &&
            o.MessType == false).Count();

            if (list >= 5)
            {
                sReturnModel.ErrorType = 2;
                sReturnModel.MessageContent = "信息发送失败!每人每天只允许发送<span style='color:red;'>5条</span>信息.";
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
            Session["captcha"] = code;
            byte[] bytes = itemValidateCode.CreateValidateGraphic(code);

            return File(bytes, @"image/jpg");
        }
    }
}
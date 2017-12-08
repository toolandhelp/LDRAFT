using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using Lm.BLL;
using Lm.CommonLib;
using Lm.Model;

public class Utits
{

    #region 系统设置
    public const int MaxDocLevel = 5;
    public const string UploadPath = "Upload";
    #endregion

    #region 暂时注销(当前登录用户信息)

    #region 当前登录用户信息
    public static Users CurrentUser
    {
        get
        {
            Users item = null;
            try
            {
                var cBll = new BLL_User();

                if (HttpContext.Current.Session["LDRAFT_USERNUM"] != null)
                {
                    var temp = HashEncrypt.DecryptQueryString(HttpContext.Current.Session["NJDDM_USERNUM"].ToString());
                    Guid tempguid = new Guid(temp);
                    if (!string.IsNullOrEmpty(temp))
                        item = cBll.GetObjectById(tempguid);
                }
            }
            catch (Exception ex)
            {
                MessageLog.AddLog("Utits.CurrentUser异常：" + ex.Message);
            }
            return item;
        }
    }

    /// <summary>
    /// 判断是否登录
    /// 1防止session名称修改和sessionState形式存储判别
    /// 2一项目多种身份登录形式
    /// </summary>
    public static bool IsLogin
    {
        get { return CurrentUser != null; }
    }
    public static bool IsAdmin
    {
        get
        {
            var AdminRole = Config.AdminRole;
            if (AdminRole == Guid.Empty)
                return false;
            return true;// CurrentRoleID == AdminRole;
        }
    }

    #endregion

    #endregion


    #region 当前用户

    #endregion


    #region 判断页面权限
    //public static bool IsNodePageAuth(int iCurrentPageNodeId)
    //{
    //    return new AuthRoleNodeBll().IsNodePageAuth(Utits.CurrentUserID, iCurrentPageNodeId);
    //}

    //public static bool IsNodePageAuth(StringBuilder szRet, int[] iRangePage, int iCurrentPageNodeId)
    //{
    //    Guid iUSERID = Utits.CurrentUserID;
    //    if (iUSERID == Guid.Empty)
    //    {
    //        var sRetrunModel = new ReturnMessageModel();
    //        sRetrunModel.ErrorType = 3;
    //        sRetrunModel.MessageContent = "未登录.";
    //        szRet.Append(sRetrunModel.ToJSON());
    //        return false;
    //    }
    //    if (!iRangePage.Contains(iCurrentPageNodeId))
    //    {
    //        var sRetrunModel = new ReturnMessageModel();
    //        sRetrunModel.ErrorType = 0;
    //        sRetrunModel.MessageContent = "NodeId参数错误：该页面不具有操作该功能.";
    //        szRet.Append(sRetrunModel.ToJSON());
    //        return false;
    //    }
    //    bool isFlag = new AuthRoleNodeBll().IsNodePageAuth(iUSERID, iCurrentPageNodeId);
    //    if (!isFlag)
    //    {
    //        var sRetrunModel = new ReturnMessageModel();
    //        sRetrunModel.ErrorType = 5;
    //        sRetrunModel.MessageContent = "无操作权限.";
    //        szRet.Append(sRetrunModel.ToJSON());
    //        return false;
    //    }
    //    return true;
    //}
    #endregion

    #region 判断操作权限
    /// <summary>
    /// 判断页面上的按钮是否拥有操作权限
    /// </summary>
    /// <param name="szRet">返回json字符串结果</param>
    /// <param name="iRangePage">能操作该按钮的页面数组</param>
    /// <param name="iCurrentPageNodeId">当前操作页面的ID</param>
    /// <param name="iCurrentButtonId">当前操作按钮的ID</param>
    /// <returns></returns>
    //public static bool IsOperateAuth(StringBuilder szRet, int[] iRangePage, int iCurrentPageNodeId, int iCurrentButtonId)
    //{
    //    Guid iUSERID = Utits.CurrentUserID;
    //    if (iUSERID == Guid.Empty)
    //    {
    //        var sRetrunModel = new ReturnMessageModel();
    //        sRetrunModel.ErrorType = 3;
    //        sRetrunModel.MessageContent = "未登录.";
    //        szRet.Append(sRetrunModel.ToJSON());
    //        return false;
    //    }
    //    if (!iRangePage.Contains(iCurrentPageNodeId))
    //    {
    //        var sRetrunModel = new ReturnMessageModel();
    //        sRetrunModel.ErrorType = 0;
    //        sRetrunModel.MessageContent = "NodeId参数错误：该页面不能操作该功能.";
    //        szRet.Append(sRetrunModel.ToJSON());
    //        return false;
    //    }
    //    var cBll = new AuthRoleNodeButtonBll();
    //    bool isFlag = cBll.IsAuthRoleNodeButton(iUSERID, iCurrentPageNodeId, iCurrentButtonId);
    //    if (!isFlag)
    //    {
    //        var sRetrunModel = new ReturnMessageModel();
    //        sRetrunModel.ErrorType = 5;
    //        sRetrunModel.MessageContent = "无操作权限.";
    //        szRet.Append(sRetrunModel.ToJSON());
    //        return false;
    //    }
    //    return true;
    //}

    /// <summary>
    /// 判断页面上的按钮是否拥有操作权限
    /// </summary>
    /// <param name="szRet">返回json字符串结果</param>
    /// <param name="iRangePage">能操作该按钮的页面数组</param>    
    /// <param name="iCurrentPageNodeId">当前操作页面的ID</param>
    /// <param name="iRangeButton">能操作按钮数组</param>
    /// <param name="iCurrentButtonId">当前操作按钮的ID</param>
    /// <returns></returns>
    //public static bool IsOperateAuth(StringBuilder szRet, int[] iRangePage, int iCurrentPageNodeId, int[] iRangeButton, int iCurrentButtonId)
    //{
    //    Guid iUSERID = Utits.CurrentUserID;
    //    if (iUSERID == Guid.Empty)
    //    {
    //        var sRetrunModel = new ReturnMessageModel();
    //        sRetrunModel.ErrorType = 3;
    //        sRetrunModel.MessageContent = "未登录.";
    //        szRet.Append(sRetrunModel.ToJSON());
    //        return false;
    //    }
    //    if (!iRangePage.Contains(iCurrentPageNodeId))
    //    {
    //        var sRetrunModel = new ReturnMessageModel();
    //        sRetrunModel.ErrorType = 0;
    //        sRetrunModel.MessageContent = "NodeId参数错误：该页面不能操作该功能.";
    //        szRet.Append(sRetrunModel.ToJSON());
    //        return false;
    //    }
    //    if (!iRangeButton.Contains(iCurrentButtonId))
    //    {
    //        var sRetrunModel = new ReturnMessageModel();
    //        sRetrunModel.ErrorType = 0;
    //        sRetrunModel.MessageContent = "ButtonId参数错误：该按钮不能操作该功能.";
    //        szRet.Append(sRetrunModel.ToJSON());
    //        return false;
    //    }
    //    var cBll = new AuthRoleNodeButtonBll();
    //    bool isFlag = cBll.IsAuthRoleNodeButton(iUSERID, iCurrentPageNodeId, iCurrentButtonId);
    //    if (!isFlag)
    //    {
    //        var sRetrunModel = new ReturnMessageModel();
    //        sRetrunModel.ErrorType = 5;
    //        sRetrunModel.MessageContent = "无操作权限.";
    //        szRet.Append(sRetrunModel.ToJSON());
    //        return false;
    //    }
    //    return true;
    //}

    #endregion

    #region 其他

    /// <summary>
    /// 得到CheckBoxList中选中了的值,去掉最后一个分割符号
    /// </summary>
    /// <param name="checkList">CheckBoxList</param>
    /// <param name="separator">分割符号</param>
    /// <returns></returns>
    public static string GetChecked(CheckBoxList checkList, char separator)
    {
        string selval = "";
        for (var i = 0; i < checkList.Items.Count; i++)
        {
            if (checkList.Items[i].Selected)
            {
                selval += checkList.Items[i].Value + separator;
            }
        }
        if (!string.IsNullOrEmpty(selval))
        {
            selval = selval.TrimEnd(separator);
        }
        return selval;
    }

    /// <summary>
    /// 用字符串分割字符串
    /// 得到的就是通过模式串分割后得到的数组
    /// </summary>
    /// <param name="szString">将要分割的字符串</param>
    /// <param name="separator">用来分割的模式串</param>
    /// <returns></returns>
    public static string[] StringSplit(string szString, string separator)
    {
        // 在由正则表达式模式定义的位置(这里是逗号的位置)拆分输入字符串。
        return Regex.Split(szString, separator);
    }

    /// <summary>
    /// 返回与 Web 服务器上的指定虚拟路径相对应的物理文件路径。
    /// </summary>
    /// <param name="strPath"></param>
    /// <returns></returns>
    public static string GetMapPath(string strPath)
    {
        if (HttpContext.Current != null)
        {
            return HttpContext.Current.Server.MapPath(strPath);
        }

        //非web程序引用
        strPath = strPath.Replace("/", "\\");
        if (strPath.StartsWith("\\"))
        {
            strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
        }
        return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
    }

    #region DownloadAttachment
    /// <summary>
    /// 下载附件
    /// </summary>
    /// <param name="ms">MemoryStream是内存流,为系统内存提供读写操作</param>
    /// <param name="fileName">附件名称</param>
    //public static void DownloadAttachment(System.IO.MemoryStream ms, string fileName)
    //{
    //    System.Web.HttpContext.Current.Response.Clear();
    //    System.Web.HttpContext.Current.Response.Charset = "utf-8";
    //    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;fileName=" + HttpUtility.UrlEncode(fileName));
    //    System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
    //    System.Web.HttpContext.Current.Response.BinaryWrite(ms.ToArray());
    //    System.Web.HttpContext.Current.Response.End();
    //}
    //public static void DownloadAttachment(System.IO.FileInfo file, EExportFormat exportFormat)
    //{
    //    System.Web.HttpContext.Current.Response.Clear();
    //    System.Web.HttpContext.Current.Response.Charset = "GB2312";
    //    System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
    //    //添加头信息，为"文件下载/另存为"对话框指定默认文件名
    //    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(file.Name));
    //    //添加头信息，指定文件大小，让浏览器能够显示下载进度
    //    System.Web.HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());

    //    //指定返回的是一个不能被客户端读取的流，必须被下载
    //    System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
    //    switch (exportFormat)
    //    {
    //        case EExportFormat.Doc:
    //            //指定返回的是一个不能被客户端读取的流，必须被下载
    //            System.Web.HttpContext.Current.Response.ContentType = "application/ms-word";
    //            break;
    //        case EExportFormat.Xls:
    //            //指定返回的是一个不能被客户端读取的流，必须被下载
    //            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
    //            break;
    //        default:
    //            //指定返回的是一个不能被客户端读取的流，必须被下载
    //            //System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
    //            System.Web.HttpContext.Current.Response.ContentType = "application nd.ms-excel";
    //            break;
    //    }
    //    //把文件流发送到客户端
    //    System.Web.HttpContext.Current.Response.WriteFile(file.FullName);
    //    //停止页面的执行
    //    System.Web.HttpContext.Current.Response.End();
    //}
    ///// <summary>
    ///// 下载附件
    ///// cvs 逗号“，”换列
    ///// excel “\t”换列
    ///// cvs、excel “\n”换行
    ///// </summary>
    ///// <param name="fileName">导出名称</param>
    ///// <param name="exportFormat">导出类型CSV，xls</param>
    ///// <param name="buf">导出的内容</param>
    //public static void DownloadAttachment(string fileName, EExportFormat exportFormat, System.Text.StringBuilder buf)
    //{

    //    System.Web.HttpContext.Current.Response.Clear();
    //    System.Web.HttpContext.Current.Response.Buffer = true;
    //    System.Web.HttpContext.Current.Response.Charset = "GB2312";
    //    System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
    //    switch (exportFormat)
    //    {
    //        case EExportFormat.Doc:
    //            //添加头信息，为"文件下载/另存为"对话框指定默认文件名
    //            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.GetEncoding("UTF-8")) + DateTime.Now.ToString("yyyy-MM-dd") + ".doc");
    //            //指定返回的是一个不能被客户端读取的流，必须被下载
    //            System.Web.HttpContext.Current.Response.ContentType = "application/ms-word";
    //            break;
    //        case EExportFormat.Xls:
    //            //添加头信息，为"文件下载/另存为"对话框指定默认文件名
    //            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.GetEncoding("UTF-8")) + DateTime.Now.ToString("yyyy-MM-dd") + ".xls");
    //            //指定返回的是一个不能被客户端读取的流，必须被下载
    //            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
    //            break;
    //        default:
    //            //添加头信息，为"文件下载/另存为"对话框指定默认文件名
    //            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.GetEncoding("UTF-8")) + DateTime.Now.ToString("yyyy-MM-dd") + ".csv");
    //            //指定返回的是一个不能被客户端读取的流，必须被下载
    //            //System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
    //            System.Web.HttpContext.Current.Response.ContentType = "application nd.ms-excel";
    //            break;
    //    }


    //    var rw = new System.IO.StringWriter();
    //    var hw = new System.Web.UI.HtmlTextWriter(rw);
    //    hw.Write(buf.ToString());
    //    System.Web.HttpContext.Current.Response.Write(rw.ToString());
    //    System.Web.HttpContext.Current.Response.End();
    //}
    #endregion

    #region 浏览器信息
    /// <summary>
    /// 获取远程浏览器端 IP 地址
    /// </summary>
    /// <returns>返回 IP 地址</returns>
    public static string GetIpAddress()
    {
        string szRet = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(szRet))
        {
            szRet = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        if (string.IsNullOrEmpty(szRet))
        {
            szRet = HttpContext.Current.Request.UserHostAddress;
        }
        if (string.IsNullOrEmpty(szRet))
        {
            szRet = "0.0.0.0";
        }
        return szRet;
    }

    /// <summary>
    /// 得到用户浏览器类型
    /// </summary>
    /// <returns></returns>
    public static string GetBrowse()
    {
        return System.Web.HttpContext.Current.Request.Browser.Type;
    }

    /// <summary>
    /// 获取浏览器端操作系统名称
    /// </summary>
    /// <returns></returns>
    public static string GetOsName()
    {
        string osVersion = System.Web.HttpContext.Current.Request.Browser.Platform;
        string userAgent = System.Web.HttpContext.Current.Request.UserAgent;
        if (userAgent == null)
            return "";
        if (userAgent.Contains("NT 6.3"))
        {
            osVersion = "Windows8.1";
        }
        else if (userAgent.Contains("NT 6.2"))
        {
            osVersion = "Windows8";
        }
        else if (userAgent.Contains("NT 6.1"))
        {
            osVersion = "Windows7";
        }
        else if (userAgent.Contains("NT 6.0"))
        {
            osVersion = "WindowsVista";
        }
        else if (userAgent.Contains("NT 5.2"))
        {
            osVersion = "WindowsServer2003";
        }
        else if (userAgent.Contains("NT 5.1"))
        {
            osVersion = "WindowsXP";
        }
        else if (userAgent.Contains("NT 5"))
        {
            osVersion = "Windows2000";
        }
        else if (userAgent.Contains("NT 4"))
        {
            osVersion = "WindowsNT4.0";
        }
        else if (userAgent.Contains("Me"))
        {
            osVersion = "WindowsMe";
        }
        else if (userAgent.Contains("98"))
        {
            osVersion = "Windows98";
        }
        else if (userAgent.Contains("95"))
        {
            osVersion = "Windows95";
        }
        else if (userAgent.Contains("Mac"))
        {
            osVersion = "Mac";
        }
        else if (userAgent.Contains("Unix"))
        {
            osVersion = "UNIX";
        }
        else if (userAgent.Contains("Linux"))
        {
            osVersion = "Linux";
        }
        else if (userAgent.Contains("SunOS"))
        {
            osVersion = "SunOS";
        }
        return osVersion;
    }
    #endregion

    /// <summary>
    /// 产生不重复随机数
    /// </summary>
    /// <param name="count">共产生多少随机数</param>
    /// <param name="minValue">最小值</param>
    /// <param name="maxValue">最大值</param>
    /// <returns>int[]数组</returns>
    public static int[] GetRandomNum(int count, int minValue, int maxValue)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());

        int length = maxValue - minValue + 1;
        byte[] keys = new byte[length];
        rnd.NextBytes(keys);
        int[] items = new int[length];
        for (int i = 0; i < length; i++)
        {
            items[i] = i + minValue;
        }
        Array.Sort(keys, items);
        int[] result = new int[count];
        Array.Copy(items, result, count);
        return result;
    }

    /// <summary>
    /// 产生随机字符串
    /// </summary>
    /// <returns>字符串位数</returns> 
    public static string GetRandomString(int length = 5)
    {
        int number;
        char code;
        string checkCode = String.Empty;
        System.Random random = new Random(Guid.NewGuid().GetHashCode());

        for (int i = 0; i < length + 1; i++)
        {
            number = random.Next();

            if (number % 2 == 0)
                code = (char)('0' + (char)(number % 10));
            else
                code = (char)('A' + (char)(number % 26));
            checkCode += code.ToString();
        }
        return checkCode;
    }

    /// <summary>
    /// 产生随机字母
    /// </summary>
    /// <returns>字符串位数</returns>
    public static string GetRandomLetter(int length = 2)
    {
        int number;
        char code;
        string checkCode = String.Empty;
        System.Random random = new Random(Guid.NewGuid().GetHashCode());
        for (int i = 0; i < length; i++)
        {
            number = random.Next();
            code = (char)('A' + (char)(number % 26));
            checkCode += code.ToString();
        }
        return checkCode;
    }

    /// <summary>
    /// 得到一个文件的大小(单位KB)
    /// </summary>
    /// <returns></returns>
    public static string GetFileSize(string file)
    {
        if (!System.IO.File.Exists(file))
        {
            return "";
        }
        System.IO.FileInfo fi = new System.IO.FileInfo(file);

        return (fi.Length / 1000).ToString("###,###");
    }

    /// <summary>
    /// Json特符字符过滤
    /// </summary>
    /// <param name="s">要过滤的源字符串</param>
    /// <returns>返回过滤的字符串</returns>
    public static string JsonCharFilter(string s)
    {
        if (string.IsNullOrEmpty(s))
            return s;
        s = s.Replace("\\", "\\\\");
        s = s.Replace("\b", "\\\b");
        s = s.Replace("\t", "\\\t");
        s = s.Replace("\n", "\\\n");
        s = s.Replace("\n", "\\\n");
        s = s.Replace("\f", "\\\f");
        s = s.Replace("\r", "\\\r");
        return s.Replace("\"", "\\\"");
    }
    #endregion
}

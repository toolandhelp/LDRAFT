using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lm.CommonLib
{
    public class FileUtils
    {
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static FileDeleteMode DeleteFile(string file)
        {
            var path = HttpContext.Current.Server.MapPath(file);
            if (!File.Exists(path)) return FileDeleteMode.NotExist;
            try
            {
                File.Delete(path);
                return FileDeleteMode.Success;
            }
            catch (Exception ex)
            {
                return FileDeleteMode.Fail;
            }
        }

        /// <summary>
        /// 获取媒体文件播放时长
        /// </summary>
        /// <param name="path">媒体文件路径</param>
        /// <returns></returns>
        //public static string GetMediaTimeLen(string path)
        //{
        //    try
        //    {
        //        Shell32.Shell shell = new Shell32.Shell();
        //        //文件路径
        //        Shell32.Folder folder = shell.NameSpace(path.Substring(0, path.LastIndexOf("\\")));
        //        //文件名称
        //        Shell32.FolderItem folderitem = folder.ParseName(path.Substring(path.LastIndexOf("\\") + 1));
        //        return folder.GetDetailsOf(folderitem, 27);

        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }

        //}
        /// <summary>
        /// 获取文件类型 
        /// </summary>
        /// <param name="FileContentType"></param>
        /// <returns></returns>
        public static AttachFileContentType GetFileType(string FileContentType)
        {
            if (!string.IsNullOrEmpty(FileContentType))
            {
                FileContentType = FileContentType.ToLower();
                if (FileContentType == "application/msword" || FileContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document") return AttachFileContentType.Word;
                else if (FileContentType == "application/vnd.ms-excel" || FileContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") return AttachFileContentType.Excel;
                else if (FileContentType == "application/vnd.ms-powerpoint" || FileContentType == "application/vnd.openxmlformats-officedocument.presentationml.presentation") return AttachFileContentType.PPT;
                else if (FileContentType == "text/plain") return AttachFileContentType.TXT;
                else if (FileContentType.StartsWith("image")) return AttachFileContentType.Image;
                else if (FileContentType == "application/pdf") return AttachFileContentType.Pdf;
                else if (FileContentType == "text/html") return AttachFileContentType.Html;
                else if (FileContentType == "application/x-shockwave-flash") return AttachFileContentType.SWF;
                else if (FileContentType.StartsWith("audio")) return AttachFileContentType.Audio;
                else if (FileContentType.StartsWith("video")) return AttachFileContentType.Video;
                else if (FileContentType == "application/octet-stream") return AttachFileContentType.RAR;
            }
            return AttachFileContentType.Other;
        }

        /// <summary>
        /// 获取文件类型 _  switch
        /// </summary>
        /// <param name="FileContentType"></param>
        /// <returns></returns>
        public static AttachFileContentType GetFileType1(string FileContentType)
        {
            if (!string.IsNullOrEmpty(FileContentType))
            {
                FileContentType = FileContentType.ToLower();
                switch (FileContentType)
                {
                    case "application/octet-stream":
                        return AttachFileContentType.RAR;
                        break;
                }
            }
            return AttachFileContentType.Other;
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="fileContentLength"></param>
        /// <returns></returns>
        public static string GetFileSize(double fileContentLength, int idx = 0)
        {
            string[] unitArray = new string[] { "KB", "MB", "GB" };
            if (fileContentLength < 1) return "";
            double r = fileContentLength / 1000;
            if (r > 1000)
                return GetFileSize(r, idx + 1);
            return r.ToString("f2").TrimEnd('0') + unitArray[idx];
        }

        public static string CopyFile(string oldFile, string newPath)
        {
            var path = HttpContext.Current.Server.MapPath(oldFile);
            if (File.Exists(path))
            {
                try
                {
                    string newLoaclPath = HttpContext.Current.Server.MapPath(newPath);
                    if (!Directory.Exists(newLoaclPath)) Directory.CreateDirectory(newLoaclPath);
                    string ext = Path.GetExtension(path);
                    string newFullPath = string.Format("{0}/{1}{2}", newPath, Guid.NewGuid(), ext);
                    File.Copy(path, HttpContext.Current.Server.MapPath(newFullPath));
                    return newFullPath;
                }
                catch (Exception ex)
                {
                }
            }
            return string.Empty;
        }

        public static string GetLocalPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }
    }

    #region 文件删除错误枚举
    /// <summary>
    /// 文件删除枚举
    /// </summary>
    public enum FileDeleteMode
    {
        Success,
        ParamErr,
        NotFound,
        NotExist,
        Fail
    }
    #endregion
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.CommonLib
{
    #region 日志操作状态枚举
    public enum OperatType
    {
        /// <summary>
        /// 上传
        /// </summary>
        Add = 1,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 2,
        /// <summary>
        /// 重命名
        /// </summary>
        ReName = 3,
    }
    #endregion

    #region 数据状态枚举
    public enum StageMode
    {
        /// <summary>
        /// 在用
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 停用
        /// </summary>
        Disable = -1
    }
    #endregion
    #region 附件文件类型枚举
    /// <summary>
    /// 附件文件类型
    /// </summary>
    public enum AttachFileContentType
    {
        /// <summary>
        /// word文档
        /// </summary>
        Word = 1,
        /// <summary>
        /// excel文件
        /// </summary>
        Excel = 2,
        /// <summary>
        /// ppt文件
        /// </summary>
        PPT = 3,
        /// <summary>
        /// 文本文档
        /// </summary>
        TXT = 4,
        /// <summary>
        /// 图片
        /// </summary>
        Image = 5,
        /// <summary>
        /// pdf文件
        /// </summary>
        Pdf = 6,
        /// <summary>
        /// 网页文件
        /// </summary>
        Html = 7,
        /// <summary>
        /// flash文件
        /// </summary>
        SWF = 8,
        /// <summary>
        /// 音频文件
        /// </summary>
        Audio = 9,
        /// <summary>
        /// 视频文件
        /// </summary>
        Video = 10,
        /// <summary>
        /// rar 或者 Zip 文件 (rar兼容 Zip 格式)
        /// </summary>
        RAR = 11,
        /// <summary>
        /// 其他文件
        /// </summary>
        Other = 99
    }
    #endregion
    #region 文档类型枚举
    public enum DirFlag
    {
        /// <summary>
        /// 个人文档
        /// </summary>
        Private = 1,
        /// <summary>
        /// 公共文档
        /// </summary>
        Public = 2
    }
    #endregion
    #region 文档授权类型枚举
    public enum AuthFlag
    {
        /// <summary>
        /// 全员
        /// </summary>
        All = 1,
        /// <summary>
        /// 指定用户
        /// </summary>
        User = 2
    }
    #endregion
    #region 事务审核结果枚举
    public enum AuditFlag
    {
        /// <summary>
        /// 通过
        /// </summary>
        Pass = 2,
        /// <summary>
        /// 不通过
        /// </summary>
        NoPass = 3
    }
    #endregion

    #region 权限类型
    public enum PerType
    {
        admin = 0,
        Read = 1,
        Write = 2,
        Manage = 3,
        NoPer = 4
    }
    #endregion


}
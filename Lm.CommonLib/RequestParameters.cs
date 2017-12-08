using System;
using System.Web;
using System.Text.RegularExpressions;

namespace Lm.CommonLib
{
    /// <summary>
    /// 最好和RegexValidate类一起使用
    /// 接受参数类(HttpContext.Current.Request)
    /// </summary>
    public class RequestParameters
    {
        private static string HttpRequestString(string szString)
        {
            return HttpContext.Current.Request[szString];
        }
        /// <summary>
        /// Guid?
        /// </summary>
        /// <param name="szString"></param>
        /// <returns>默认返回null</returns>
        public static Guid? PGuidNull(string szString)
        {
            var sztemp = HttpRequestString(szString);
            if (string.IsNullOrEmpty(sztemp))
                return null;
            if (Regex.IsMatch(sztemp.Trim(), "^[A-Fa-f0-9]{8}(-[A-Fa-f0-9]{4}){3}-[A-Fa-f0-9]{12}$"))
            {
                return new Guid(sztemp.Trim());
            }
            return null;
        }
        /// <summary>
        /// Guid
        /// </summary>
        /// <param name="szString"></param>
        /// <returns>默认返回Guid.Empty</returns>
        public static Guid PGuid(string szString)
        {
            var sztemp = HttpRequestString(szString);
            if (string.IsNullOrEmpty(sztemp))
                return Guid.Empty;
            if (Regex.IsMatch(sztemp.Trim(), "^[A-Fa-f0-9]{8}(-[A-Fa-f0-9]{4}){3}-[A-Fa-f0-9]{12}$"))
            {
                return new Guid(sztemp.Trim());
            }
            return Guid.Empty;
        }
        /// <summary>
        /// string
        /// </summary>
        /// <param name="szString"></param>
        /// <returns>默认返回“”</returns>
        public static string Pstring(string szString)
        {
            var sztemp = HttpRequestString(szString);
            return string.IsNullOrEmpty(sztemp) ? "" : sztemp.Trim();
        }

        /// <summary>
        /// string
        /// </summary>
        /// <param name="szString"></param>
        /// <returns> 默认返回null</returns>
        public static string PstringNull(string szString)
        {
            var sztemp = HttpRequestString(szString);
            return string.IsNullOrEmpty(sztemp) ? null : sztemp.Trim();
        }

        /// <summary>
        /// DateTime?
        /// </summary>
        /// <param name="szString"></param>
        /// <returns>默认返回null</returns>
        public static DateTime? PDateTime(string szString)
        {
            var sztemp = HttpRequestString(szString);
            if (string.IsNullOrEmpty(sztemp))
                return null;
            try
            {
                return Convert.ToDateTime(sztemp.Trim());
            }
            catch (FormatException)
            {
                return null;
            }
        }

        public static short Pshort(string szString)
        {
            var sztemp = HttpRequestString(szString);
            short result = 0;
            if (string.IsNullOrEmpty(sztemp))
                return result;
            if (Regex.IsMatch(sztemp.Trim(), @"^-?(([1-9]+\d*)|(0))$"))
            {
                try
                {
                    return Convert.ToInt16(sztemp.Trim());
                }
                catch (OverflowException)
                {
                    return result;
                }
            }
            return result;
        }
        public static int Pint(string szString)
        {
            var sztemp = HttpRequestString(szString);
            int result = 0;
            if (string.IsNullOrEmpty(sztemp))
                return result;
            if (Regex.IsMatch(sztemp.Trim(), @"^-?(([1-9]+\d*)|(0))$"))
            {
                try
                {
                    return Convert.ToInt32(sztemp.Trim());
                }
                catch (OverflowException)
                {
                    return result;
                }
            }
            return result;
        }
        public static int? PintNull(string szString)
        {
            var sztemp = HttpRequestString(szString);
            if (string.IsNullOrEmpty(sztemp))
                return null;
            if (Regex.IsMatch(sztemp.Trim(), @"^-?(([1-9]+\d*)|(0))$"))
            {
                try
                {
                    return Convert.ToInt32(sztemp.Trim());
                }
                catch (OverflowException)
                {
                    return null;
                }
            }
            return null;
        }
        public static double? PdoubleNull(string szString)
        {
            var sztemp = HttpRequestString(szString);
            if (string.IsNullOrEmpty(sztemp))
                return null;

            if (Regex.IsMatch(sztemp.Trim(), "^(-?\\d+)(\\.\\d+)?$"))
            {
                try
                {
                    return Convert.ToDouble(sztemp.Trim());
                }
                catch (OverflowException)
                {
                    return null;
                }
            }
            return null;
        }
        public static long Plong(string szString)
        {
            var sztemp = HttpRequestString(szString);
            long result = 0;
            if (string.IsNullOrEmpty(sztemp))
                return result;
            if (Regex.IsMatch(sztemp.Trim(), @"^-?(([1-9]+\d*)|(0))$"))
            {
                try
                {
                    return Convert.ToInt64(sztemp.Trim());
                }
                catch (OverflowException)
                {
                    return result;
                }
            }
            return result;
        }

        #region 浮点
        //float:浮点型，含字节数为4，32bit，数值范围为-3.4E38~3.4E38（7个有效位）
        //double:双精度实型，含字节数为8，64bit数值范围-1.7E308~1.7E308（15个有效位）
        //decimal:数字型，128bit，不存在精度损失，常用于银行帐目计算。（28个有效位）
        public static float Pfloat(string szString)
        {
            var sztemp = HttpRequestString(szString);
            float result = 0.0f;
            if (string.IsNullOrEmpty(sztemp))
                return result;

            if (Regex.IsMatch(szString.Trim(), "^(-?\\d+)(\\.\\d+)?$"))
            {
                try
                {
                    return Convert.ToSingle(sztemp.Trim());
                }
                catch (OverflowException)
                {
                    return result;
                }
            }
            return result;
        }
        public static double Pdouble(string szString)
        {
            var sztemp = HttpRequestString(szString);
            double result = 0.0d;
            if (string.IsNullOrEmpty(sztemp))
                return result;

            if (Regex.IsMatch(sztemp.Trim(), "^(-?\\d+)(\\.\\d+)?$"))
            {
                try
                {
                    return Convert.ToDouble(sztemp.Trim());
                }
                catch (OverflowException)
                {
                    return result;
                }
            }
            return result;
        }
        public static decimal Pdecimal(string szString)
        {
            var sztemp = HttpRequestString(szString);
            decimal result = 0.0m;
            if (string.IsNullOrEmpty(sztemp))
                return result;

            if (Regex.IsMatch(sztemp.Trim(), "^(-?\\d+)(\\.\\d+)?$"))
            {
                try
                {
                    return Convert.ToDecimal(sztemp.Trim());
                }
                catch (OverflowException)
                {
                    return result;
                }
            }
            return result;
        }


        ///  <summary>
        /// 保留N位小数四舍五入
        ///  </summary>
        ///  <param name="szString">参数</param>
        /// <param name="iPointLength">强制规定必须是大于0的位数</param>
        /// <returns></returns>        
        public static float Pfloat(string szString, int iPointLength)
        {
            var sztemp = HttpRequestString(szString);
            float result = 0.0f;
            if (string.IsNullOrEmpty(sztemp))
                return result;

            if (Regex.IsMatch(sztemp.Trim(), "^(-?\\d+)(\\.\\d+)?$"))
            {
                try
                {
                    result = Convert.ToSingle(sztemp.Trim());
                    return Convert.ToSingle(result.ToString("f" + iPointLength));
                }
                catch (OverflowException)
                {
                    return result;
                }
            }
            return result;
        }
        /// <summary>
        ///保留N位小数四舍五入
        /// </summary>
        /// <param name="szString">参数</param>
        /// <param name="iPointLength">强制规定必须是大于0的位数</param>
        /// <returns></returns>
        public static double Pdouble(string szString, int iPointLength)
        {
            var sztemp = HttpRequestString(szString);
            double result = 0.0d;
            if (string.IsNullOrEmpty(sztemp))
                return result;

            if (Regex.IsMatch(sztemp.Trim(), "^(-?\\d+)(\\.\\d+)?$"))
            {
                try
                {
                    result = Convert.ToDouble(sztemp.Trim());
                    return Convert.ToDouble(result.ToString("f" + iPointLength));
                }
                catch (OverflowException)
                {
                    return result;
                }
            }
            return result;
        }
        /// <summary>
        ///保留N位小数四舍五入
        /// </summary>
        /// <param name="szString">参数</param>
        /// <param name="iPointLength">强制规定必须是大于0的位数</param>
        /// <returns></returns>
        public static decimal Pdecimal(string szString, int iPointLength)
        {
            var sztemp = HttpRequestString(szString);
            decimal result = 0.0m;
            if (string.IsNullOrEmpty(sztemp))
                return result;

            if (Regex.IsMatch(sztemp.Trim(), "^(-?\\d+)(\\.\\d+)?$"))
            {
                try
                {
                    result = Convert.ToDecimal(sztemp.Trim());
                    return Convert.ToDecimal(result.ToString("f" + iPointLength));
                }
                catch (OverflowException)
                {
                    return result;
                }
            }
            return result;
        }
        #endregion

        public static TimeSpan Ptimespan(string szString)
        {
            var sztemp = HttpRequestString(szString);
            TimeSpan result = TimeSpan.Zero;
            if (string.IsNullOrEmpty(sztemp))
                return TimeSpan.Zero;
            try
            {
                if (!TimeSpan.TryParse(sztemp.Trim(), out result))
                    return TimeSpan.Zero;
                return result;
            }
            catch (OverflowException)
            {
                return TimeSpan.Zero;
            }
        }
        public static TimeSpan? PtimespanNull(string szString)
        {
            var sztemp = HttpRequestString(szString);
            if (string.IsNullOrEmpty(sztemp))
                return null;

            try
            {
                TimeSpan result = TimeSpan.Zero;
                if (!TimeSpan.TryParse(sztemp.Trim(), out result))
                    return TimeSpan.Zero;
                return result;
            }
            catch (OverflowException)
            {
                return null;
            }
        }
    }
}

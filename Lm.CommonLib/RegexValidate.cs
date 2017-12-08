using System.Text.RegularExpressions;

namespace Lm.CommonLib
{
    /// <summary>
    /// <para>正则表达式验证</para>
    /// <para>数据验证不能用try-catch异常判断，产生异常->效率极低。</para>
    /// <para>framework4.5对正则表达式重写过，效率更高。</para>
    /// </summary>
    public class RegexValidate
    {
        //GUID
        public static bool IsGuid(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            return Regex.IsMatch(s.Trim(), "^[A-Fa-f0-9]{8}(-[A-Fa-f0-9]{4}){3}-[A-Fa-f0-9]{12}$");
        }

        public static bool IsNumber(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            return Regex.IsMatch(s.Trim(), "^[0-9]*$");
        }

        //只能输入零和非零开头的正整数数字
        public static bool IsPositiveInt(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            return Regex.IsMatch(s.Trim(), "^(0|[1-9][0-9]*)$");
        }

        //整数 
        public static bool IsInt(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            return Regex.IsMatch(s.Trim(), "^-?\\d+$");
        }

        //浮点数
        public static bool IsFloat(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            return Regex.IsMatch(s.Trim(), "^(-?\\d+)(\\.\\d+)?$");
        }

        //验证电话号码
        public static bool IsPhone(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            return Regex.IsMatch(s.Trim(), @"^((\(\d{3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}$");
        }

        //验证11位手机号码
        public static bool IsMobile(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            return Regex.IsMatch(s.Trim(), @"^1[3|4|5|7|8][0-9]\d{8}$");
        }

        //身份证
        public static bool IsCard(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            return Regex.IsMatch(s.Trim(), @"^\d{15}(\d{2}[xX0-9])?$");
        }

        //邮件
        public static bool IsEmail(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            return Regex.IsMatch(s.Trim(), @"^[\w\.]+([-]\w+)*@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]");
        }

        //URL
        public static bool IsUrl(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            return Regex.IsMatch(s.Trim(), @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }

        //验证YYYYMMDD格式的字符串是否为日期格式
        public static bool IsDateYyyymmdd(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            if (s.Length == 8)
            {
                s = s.Substring(0, 4) + "-" + s.Substring(4, 2) + "-" + s.Substring(6, 2);
                return IsDateTime(s, 1);
            }
            else
                return false;
        }
        
        /// <summary>
        ///<para>验证日期时间格式，具体对应格式</para>
        ///<para> iType参数对应格式</para>        
        ///<para> 1-20对应日期格式</para>
        ///<para> 1:YYYY-MM-DD</para>
        ///<para> 2:YYYY/MM/DD</para>
        ///<para> 3:YYYY.MM.DD</para>
        ///<para> 4:DD/MM/YYYY</para>
        ///<para> 5:YYYY-MM-DD YYYY-M-DD YYYY-MM-D YYYY-M-D</para>
        ///<para> 6:YYYY/MM/DD YYYY/M/DD YYYY/MM/D YYYY/M/D</para>
        ///<para> 7:YYYY.MM.DD YYYY.M.DD YYYY.MM.D YYYY.M.D</para>
        ///<para> 8:DD/MM/YYYY DD/M/YYYY D/MM/YYYY D/M/YYYY</para>
        ///<para> 21-80对应日期时间格式（1对n形式，n对n形式）</para>
        ///<para> 21:YYYY-MM-DD HH:mm:ss H:mm:ss H:m:ss H:m:s HH:m:ss HH:mm:s</para>
        ///<para> 22:YYYY/MM/DD HH:mm:ss H:mm:ss H:m:ss H:m:s HH:m:ss HH:mm:s</para>
        ///<para> 23:YYYY.MM.DD HH:mm:ss H:mm:ss H:m:ss H:m:s HH:m:ss HH:mm:s</para>
        ///<para> 24:DD/MM/YYYY HH:mm:ss H:mm:ss H:m:ss H:m:s HH:m:ss HH:mm:s</para>
        ///<para> 25:YYYY-MM-DD YYYY-M-DD YYYY-MM-D YYYY-M-D HH:mm:ss H:mm:ss H:m:ss H:m:s HH:m:ss HH:mm:s</para>
        ///<para> 26:YYYY/MM/DD YYYY/M/DD YYYY/MM/D YYYY/M/D HH:mm:ss H:mm:ss H:m:ss H:m:s HH:m:ss HH:mm:s</para>
        ///<para> 27:YYYY.MM.DD YYYY.M.DD YYYY.MM.D YYYY.M.D HH:mm:ss H:mm:ss H:m:ss H:m:s HH:m:ss HH:mm:s</para>
        ///<para> 28:DD/MM/YYYY DD/M/YYYY D/MM/YYYY D/M/YYYY HH:mm:ss H:mm:ss H:m:ss H:m:s HH:m:ss HH:mm:s</para>
        ///<para> 81-90对应时间格式</para>
        ///<para> 81:HH:mm:ss H:mm:ss H:m:ss H:m:s HH:m:ss HH:mm:s</para>
        /// </summary>
        /// <param name="s"></param>
        /// <param name="iType"></param>
        /// <returns></returns>
        public static bool IsDateTime(string s, int iType)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            switch (iType)
            {
                case 1://YYYY-MM-DD            
                    return Regex.IsMatch(s.Trim(), "^(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|(02-(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))-02-29)$");
                case 2://YYYY/MM/DD
                    return Regex.IsMatch(s.Trim(), "^(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})/(((0[13578]|1[02])/(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)/(0[1-9]|[12][0-9]|30))|(02/(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))/02/29)$");
                case 3://YYYY.MM.DD
                    return Regex.IsMatch(s.Trim(), "^(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3}).(((0[13578]|1[02]).(0[1-9]|[12][0-9]|3[01]))|((0[469]|11).(0[1-9]|[12][0-9]|30))|(02.(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00)).02.29)$");
                case 4://DD/MM/YYYY
                    return Regex.IsMatch(s.Trim(), "^(((0[1-9]|[12][0-9]|3[01])/((0[13578]|1[02]))|((0[1-9]|[12][0-9]|30)/(0[469]|11))|(0[1-9]|[1][0-9]|2[0-8])/(02))/([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3}))|(29/02/(([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00)))$");
                case 5://YYYY-MM-DD YYYY-M-DD YYYY-MM-D YYYY-M-D
                    return Regex.IsMatch(s.Trim(), "^(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0?[13578]|1[02])-(0?[1-9]|[12][0-9]|3[01]))|((0?[469]|11)-(0?[1-9]|[12][0-9]|30))|(0?2-(0?[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))-0?2-29)$");
                case 6://YYYY/MM/DD YYYY/M/DD YYYY/MM/D YYYY/M/D
                    return Regex.IsMatch(s.Trim(), "^(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})/(((0?[13578]|1[02])/(0?[1-9]|[12][0-9]|3[01]))|((0?[469]|11)/(0?[1-9]|[12][0-9]|30))|(0?2/(0?[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))/0?2/29)$");
                case 7://YYYY.MM.DD YYYY.M.DD YYYY.MM.D YYYY.M.D
                    return Regex.IsMatch(s.Trim(), "^(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3}).(((0?[13578]|1[02]).(0?[1-9]|[12][0-9]|3[01]))|((0?[469]|11).(0?[1-9]|[12][0-9]|30))|(0?2.(0?[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00)).0?2.29)$");
                case 8://DD/MM/YYYY DD/M/YYYY D/MM/YYYY D/M/YYYY
                    return Regex.IsMatch(s.Trim(), "^(((0?[1-9]|[12][0-9]|3[01])/((0?[13578]|1[02]))|((0?[1-9]|[12][0-9]|30)/(0?[469]|11))|(0?[1-9]|[1][0-9]|2[0-8])/(0?2))/([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3}))|(29/02/(([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00)))$");

                case 21://YYYY-MM-DD HH:mm:ss H:mm:ss H:m:ss H:m:s HH:m:ss HH:mm:s            
                    return Regex.IsMatch(s.Trim(), "^(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|(02-(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))-02-29) ((20|21|22|23|[0-1]?\\d):[0-5]?\\d:[0-5]?\\d)$");
                case 22://YYYY/MM/DD HH:mm:ss H:mm:ss H:m:ss H:m:s HH:m:ss HH:mm:s
                    return Regex.IsMatch(s.Trim(), "^(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})/(((0[13578]|1[02])/(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)/(0[1-9]|[12][0-9]|30))|(02/(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))/02/29) ((20|21|22|23|[0-1]?\\d):[0-5]?\\d:[0-5]?\\d)$");
                case 23://YYYY.MM.DD HH:mm:ss H:mm:ss H:m:ss H:m:s HH:m:ss HH:mm:s
                    return Regex.IsMatch(s.Trim(), "^(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3}).(((0[13578]|1[02]).(0[1-9]|[12][0-9]|3[01]))|((0[469]|11).(0[1-9]|[12][0-9]|30))|(02.(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00)).02.29) ((20|21|22|23|[0-1]?\\d):[0-5]?\\d:[0-5]?\\d)$");
                case 24://DD/MM/YYYY HH:mm:ss H:mm:ss H:m:ss H:m:s HH:m:ss HH:mm:s
                    return Regex.IsMatch(s.Trim(), "^(((0[1-9]|[12][0-9]|3[01])/((0[13578]|1[02]))|((0[1-9]|[12][0-9]|30)/(0[469]|11))|(0[1-9]|[1][0-9]|2[0-8])/(02))/([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3}))|(29/02/(([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))) ((20|21|22|23|[0-1]?\\d):[0-5]?\\d:[0-5]?\\d)$");
                case 25://YYYY-MM-DD YYYY-M-DD YYYY-MM-D YYYY-M-D HH:mm:ss H:mm:ss H:m:ss H:m:s HH:m:ss HH:mm:s
                    return Regex.IsMatch(s.Trim(), "^(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0?[13578]|1[02])-(0?[1-9]|[12][0-9]|3[01]))|((0?[469]|11)-(0?[1-9]|[12][0-9]|30))|(0?2-(0?[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))-0?2-29) ((20|21|22|23|[0-1]?\\d):[0-5]?\\d:[0-5]?\\d)$");
                case 26://YYYY/MM/DD YYYY/M/DD YYYY/MM/D YYYY/M/D HH:mm:ss H:mm:ss H:m:ss H:m:s HH:m:ss HH:mm:s
                    return Regex.IsMatch(s.Trim(), "^(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})/(((0?[13578]|1[02])/(0?[1-9]|[12][0-9]|3[01]))|((0?[469]|11)/(0?[1-9]|[12][0-9]|30))|(0?2/(0?[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))/0?2/29) ((20|21|22|23|[0-1]?\\d):[0-5]?\\d:[0-5]?\\d)$");
                case 27://YYYY.MM.DD YYYY.M.DD YYYY.MM.D YYYY.M.D HH:mm:ss H:mm:ss H:m:ss H:m:s HH:m:ss HH:mm:s
                    return Regex.IsMatch(s.Trim(), "^(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3}).(((0?[13578]|1[02]).(0?[1-9]|[12][0-9]|3[01]))|((0?[469]|11).(0?[1-9]|[12][0-9]|30))|(0?2.(0?[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00)).0?2.29) ((20|21|22|23|[0-1]?\\d):[0-5]?\\d:[0-5]?\\d)$");
                case 28://DD/MM/YYYY DD/M/YYYY D/MM/YYYY D/M/YYYY HH:mm:ss H:mm:ss H:m:ss H:m:s HH:m:ss HH:mm:s
                    return Regex.IsMatch(s.Trim(), "^(((0?[1-9]|[12][0-9]|3[01])/((0?[13578]|1[02]))|((0?[1-9]|[12][0-9]|30)/(0?[469]|11))|(0?[1-9]|[1][0-9]|2[0-8])/(0?2))/([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3}))|(29/02/(([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))) ((20|21|22|23|[0-1]?\\d):[0-5]?\\d:[0-5]?\\d)$");
                
                case 81://HH:mm:ss H:mm:ss H:m:ss H:m:s HH:m:ss HH:mm:s
                    return Regex.IsMatch(s, "^((20|21|22|23|[0-1]?\\d):[0-5]?\\d:[0-5]?\\d)$");
                default:
                    return false;
            }
        }

        //Zip
        public static bool IsZip(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            return Regex.IsMatch(s.Trim(), @"^[1-9]\d{5}$");
        }

        //English
        public static bool IsEnglish(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            return Regex.IsMatch(s.Trim(), @"^[A-Za-z]+$");
        }

        //简体中文
        public static bool IsChinese(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            return Regex.IsMatch(s.Trim(), @"^[\u0391-\uFFE5]+$");
        }

        //验证是否为中文（简体与繁体）
        public static bool IsChina(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            return Regex.IsMatch(s.Trim(), @"[\u4E00-\u9FA5]|[\uFE30-\uFFA0]");
        }

        public static bool IsIpAddress(string s)
        {
            System.Net.IPAddress ip;
            return System.Net.IPAddress.TryParse(s, out ip);
        }

        public static bool IsBase64String(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            //A-Z, a-z, 0-9, +, /, =
            return Regex.IsMatch(s.Trim(), @"[A-Za-z0-9\+\/\=]");
        }

        public static bool IsSafeAccount(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            //A-Z, a-z, 0-9, _, 简体中文
            return Regex.IsMatch(s.Trim(), @"^[A-Za-z0-9_\u4e00-\u9fa5]{3,32}$");
            //长度为3-32的所有字符
            //return Regex.IsMatch(s.Trim(), "^.{3,32}$");
        }
    }
}

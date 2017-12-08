using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Lm.CommonLib
{
    /// <summary>
    /// Hash加密
    /// </summary>
    public class HashEncrypt
    {
        #region MD5加密
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="szSource">待加密字串</param>
        /// <param name="times">加密次数</param>
        /// <returns>加密后的字串</returns>
        private string MD5Encrypt(string szSource, int times)
        {
            string md5 = MD5Encrypt(szSource);
            for (int i = 0; i < times - 1; i++)
            {
                md5 = MD5Encrypt(md5);
            }
            return md5;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="szSource">待加密字串</param>
        /// <returns>加密后的字串</returns>
        private string MD5Encrypt(string szSource)
        {
            return MSMD5(szSource, 32);
        }

        /// <summary>
        /// md5 encrypt
        /// 第三种创建方式
        /// </summary>
        /// <param name="strSource">待加密字串</param>
        /// <param name="length">16或32值之一,其它则采用.net默认MD5加密算法</param>
        /// <returns>加密后的字串</returns>
        private string MSMD5(string strSource, int length)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(strSource);
            byte[] hashValue = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(bytes);
            StringBuilder szBuilder = new StringBuilder();
            switch (length)
            {
                case 16:
                    for (int i = 4; i < 12; i++)
                        szBuilder.Append(hashValue[i].ToString("x2"));
                    break;
                case 32:
                    for (int i = 0; i < 16; i++)
                    {
                        szBuilder.Append(hashValue[i].ToString("x2"));
                    }
                    break;
                default:
                    for (int i = 0; i < hashValue.Length; i++)
                    {
                        szBuilder.Append(hashValue[i].ToString("x2"));
                    }
                    break;
            }
            char[] charArrays = szBuilder.ToString().ToCharArray();
            Array.Reverse(charArrays);
            return new string(charArrays);
        }

        /// <summary>
        /// md5 encrypt 第二种创建方式
        /// </summary>
        /// <param name="szString">待加密字符串</param>
        /// <returns></returns>
        private string MSMD5Second(String szString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(szString);
            byte[] result = md5.ComputeHash(bytes);
            String ret = "";
            for (int i = 0; i < result.Length; i++)
                ret += result[i].ToString("x").PadLeft(2, '0');
            //表示如果一个字符串的长度小于指定的值，则在字符串的左侧（也就是前面）用指定的字符填充，直到字符串长度达到最小值。
            //PadLeft(2, '0'):表示不满两位，用0补足 如是“1”补足后就是“01”注意补足的位置是在左侧（前面）
            return ret;
        }

        /// <summary>
        /// get md5 hash value 第一种创建方式
        /// </summary>
        /// <param name="szString"></param>
        /// <returns></returns>
        private string MSMD5First(string szString)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            var md5Hasher = MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] bytes = md5Hasher.ComputeHash(Encoding.Unicode.GetBytes(szString));
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < bytes.Length; i++)
            {
                sBuilder.Append(bytes[i].ToString("x2"));
                //X 十六进制 ，X是大写,x是小写 2:每次都是两位数
                //假设有两个数10和26，正常情况十六进制显示0xA、0x1A，这样看起来不整齐，为了好看，可以指定"X2"，这样显示出来就是：0x0A、0x1A。
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        #endregion

        #region DES加密、解密
        private const string _QueryStringKey = "abcdefgh"; //URL传输参数加密Key
        private const string _PassWordKey = "abcdefgh";  //PassWord加密Key        

        /// <summary>
        /// 加密URL传输的字符串
        /// </summary>
        /// <param name="s">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptQueryString(string s)
        {
            return DESEncrypt(s, _QueryStringKey);
        }

        /// <summary>
        /// 解密URL传输的字符串
        /// </summary>
        /// <param name="s">要解密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string DecryptQueryString(string s)
        {
            return DESDecrypt(s, _QueryStringKey);
        }

        /// <summary>
        /// 加密帐号口令
        /// </summary>
        /// <param name="PassWord">要加密的帐号口令</param>
        /// <returns>加密后的帐号口令</returns>
        public static string DESEncryptPassWord(string PassWord)
        {
            return DESEncrypt(PassWord, _PassWordKey);
        }
        /// <summary>
        /// 解密帐号口令
        /// </summary>
        /// <param name="PassWord">要解密帐号口令</param>
        /// <returns>解密后的帐号口令</returns>
        public static string DESDecryptPassWord(string PassWord)
        {
            return DESDecrypt(PassWord, _PassWordKey);
        }
        #region DES详细
        /// <summary>
        /// DES 加密过程
        /// </summary>
        /// <param name="pToEncrypt">要加密的字符串</param>
        /// <param name="sKey">加密Key</param>
        /// <returns>加密后的字符串</returns>
        private static string DESEncrypt(string pToEncrypt, string sKey)
        {
            try
            {
                //需要用的对称密钥 
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();  //把字符串放到byte数组中  

                byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
                //byte[]  inputByteArray=Encoding.Unicode.GetBytes(pToEncrypt);  

                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);  //建立加密对象的密钥和偏移量

                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);   //原文使用ASCIIEncoding.ASCII方法的GetBytes方法 
                MemoryStream ms = new MemoryStream();     //使得输入密码必须输入英文文本
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                StringBuilder ret = new StringBuilder();
                foreach (byte b in ms.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }
                return ret.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        /// <summary>
        /// DES 解密过程
        /// </summary>
        /// <param name="pToDecrypt">要解密的字符串</param>
        /// <param name="sKey">加密Key</param>
        /// <returns>解密后的字符串</returns>
        private static string DESDecrypt(string pToDecrypt, string sKey)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
                for (int x = 0; x < pToDecrypt.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);  //建立加密对象的密钥和偏移量，此值重要，不能修改  
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                StringBuilder ret = new StringBuilder();  //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象  

                return System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }
        #endregion
        #endregion

        #region 通用方法
        //原型md5
        public static string md5(string szString)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(szString, "MD5").ToLower();
        }
        //原型sha1
        public static string sha1(string szString)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(szString, "SHA1").ToLower();
        }
        //原型HmacSha1
        public static string HmacSha1(string szString, string szKey)
        {
            System.Security.Cryptography.HMACSHA1 hmacsha1 = new System.Security.Cryptography.HMACSHA1();
            hmacsha1.Key = Encoding.UTF8.GetBytes(szKey);
            byte[] dataBuffer = Encoding.UTF8.GetBytes(szString);
            byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);

            StringBuilder ret = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString().ToLower();
        }
        //截取字符
        private static string SubString(string szString, int length)
        {
            string szStr = "";
            if (!string.IsNullOrEmpty(szString))
            {
                if (szString.Length > length)
                    szStr = szString.Substring(0, length);
                else
                    szStr = szString;
            }
            return szStr;
        }
        #endregion

        /// <summary>
        /// 后台加密算法
        /// 明码：admin  ->加密后：15b2cedb20de534ad43b54303ae9a772
        /// 明码：111111 ->加密后：54809f31a5f991aeba92d0f910367544
        /// </summary>
        /// <param name="s">md5(szSource)=明码后的md5</param>
        /// <returns></returns>
        public static string BgPassWord(string s)
        {
            //md5(sha1(md5(明码))+sha1(md5(明码))）
            return md5(sha1(s) + sha1(s));
        }
        /// <summary>
        /// 前台加密算法
        /// </summary>
        /// <param name="s">md5(szSource)=明码后的md5</param>
        /// <returns></returns>
        public static string IndexPassWord(string s)
        {
            //md5(sha1(md5(明码))+md5(明码)）
            return md5(sha1(s) + s);
        }
    }
}

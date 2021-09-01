using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Service.Models
{
    public class ConvertUtil
    {
        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
    "đ",
    "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
    "í","ì","ỉ","ĩ","ị",
    "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
    "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
    "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
    "d",
    "e","e","e","e","e","e","e","e","e","e","e",
    "i","i","i","i","i",
    "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
    "u","u","u","u","u","u","u","u","u","u","u",
    "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
        //public static string RemoveUnicode(string text)
        //{
        //    string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
        //        "đ",
        //        "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
        //        "í","ì","ỉ","ĩ","ị",
        //        "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
        //        "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
        //        "ý","ỳ","ỷ","ỹ","ỵ",};
        //    string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
        //        "d",
        //        "e","e","e","e","e","e","e","e","e","e","e",
        //        "i","i","i","i","i",
        //        "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
        //        "u","u","u","u","u","u","u","u","u","u","u",
        //        "y","y","y","y","y",};
        //    for (int i = 0; i < arr1.Length; i++)
        //    {
        //        text = text.Replace(arr1[i], arr2[i]);
        //        text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
        //    }
        //    return text;
        //}
        public static int ToInt32(object obj)
        {
            int retVal;

            try
            {
                retVal = Convert.ToInt32(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }

        public static bool ToBool(object obj)
        {
            bool retVal;

            try
            {
                retVal = Convert.ToBoolean(obj);
            }
            catch
            {
                retVal = false;
            }

            return retVal;
        }

        public static List<int> GetValuesArray(string ltsSourceValues)
        {
            var ltsValues = new List<int>();
            if (!string.IsNullOrEmpty(ltsSourceValues))
            {
                if (ltsSourceValues.Contains(","))
                    ltsValues = ltsSourceValues.Split(',').Select(o => o != "" ? Convert.ToInt32(o) : 0).ToList();
                else
                    ltsValues.Add(Convert.ToInt32(ltsSourceValues));
            }
            return ltsValues;
        }
        public static List<string> GetValuesArrayString(string ltsSourceValues)
        {
            var ltsValues = new List<string>();
            if (!string.IsNullOrEmpty(ltsSourceValues))
            {
                if (ltsSourceValues.Contains(","))
                    ltsValues = ltsSourceValues.Split(',').Select(o => o != "" ? Convert.ToString(o) : "").ToList();
                else
                    ltsValues.Add(Convert.ToString(ltsSourceValues));
            }
            return ltsValues;
        }
        public static string StringToBoolString(string obj)
        {
            var retVal = "0";
            if (obj.ToLower() == "true")
                retVal = "1";
            return retVal;
        }
        public static int ToInt32(object obj, int defaultValue)
        {
            int retVal = defaultValue;

            if (obj == null || obj == DBNull.Value)
                return retVal;

            try
            {
                retVal = Convert.ToInt32(obj);
            }
            catch
            {
                retVal = defaultValue;
            }

            return retVal;
        }

        public static double ToDouble(object obj)
        {
            double retVal;

            try
            {
                retVal = Convert.ToDouble(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }

        public static long ToLong(object obj)
        {
            long retVal;

            try
            {
                retVal = Convert.ToInt64(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }

        public static decimal ToDecimal(object obj)
        {
            decimal retVal;

            try
            {
                retVal = Convert.ToDecimal(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }

        public static DateTime StringToDate(string obj)
        {
            DateTime retVal;

            try
            {
                var year = obj.Substring(0, 4);
                var mounth = obj.Substring(4, 2);
                var day = obj.Substring(6, 2);

                retVal = Convert.ToDateTime(year + '/' + mounth + '/' + day + ' ' + obj.Substring(8, 2) + ':' + obj.Substring(10, 2) + ':' + obj.Substring(12, 2));
            }
            catch
            {
                retVal = DateTime.Now;
            }
            return retVal;
        }
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder sb = new StringBuilder();
            char c;
            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                c = Convert.ToChar(Convert.ToInt32(rand.Next(65, 87)));
                sb.Append(c);
            }
            if (lowerCase)
                return sb.ToString().ToLower();
            return sb.ToString();

        }
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public static double ToDouble(object obj, double defaultValue)
        {
            double retVal = 0;

            if (obj == null || obj == DBNull.Value)
                return retVal;

            try
            {
                retVal = Convert.ToDouble(obj);
            }
            catch
            {
                retVal = defaultValue;
            }

            return retVal;
        }

        public static string ToString(object obj)
        {
            string retVal;

            try
            {
                retVal = Convert.ToString(obj);
            }
            catch
            {
                retVal = String.Empty;
            }

            return retVal;
        }

        public static string ToString(object obj, string defaultValue)
        {
            var retVal = String.Empty;

            if (obj == null || obj == DBNull.Value)
                return retVal;

            try
            {
                retVal = Convert.ToString(obj);
            }
            catch
            {
                retVal = defaultValue;
            }

            return retVal;
        }

        public static string ToDate(object obj)
        {
            var retVal = DateTime.Now;
            var strArr = obj.ToString().Split(' ');
            var lenStrArr = strArr.Length;
            try
            {
                if (lenStrArr >= 1)
                {
                    var str = strArr[0];
                    var strTemp = str.Split('/');
                    if (strTemp.Length == 3)
                    {
                        var t = string.Empty;
                        if (lenStrArr == 2)
                        {
                            t = strArr[1];
                        }
                        string input = string.Format("{0}-{1}-{2}", strTemp[2], strTemp[1], strTemp[0]);

                        return Convert.ToDateTime(input).ToString("dd/MM/yyyy");
                    }
                }
            }
            catch
            {
                return DateTime.Now.Date.ToString("dd/MM/yyyy");
            }
            return DateTime.Now.Date.ToString("dd/MM/yyyy");
        }

        public static bool CheckDateTime()
        {
            try
            {

                Convert.ToDateTime("31/12/2014");
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public static DateTime ToDateTimeNew(string obj)
        {

            var retVal = DateTime.Now;
            var strArr = obj.Split(' ');
            var lenStrArr = strArr.Length;
            try
            {
                if (CheckDateTime())
                {
                    return Convert.ToDateTime(obj);
                }

                if (lenStrArr >= 1)
                {
                    var str = strArr[0];
                    var strTemp = str.Split('/');
                    if (strTemp.Length == 3)
                    {
                        var t = string.Empty;
                        if (lenStrArr == 2)
                        {
                            t = strArr[1];
                        }
                        var input = string.Format("{0}-{2}-{1} {3}", strTemp[2], strTemp[1], strTemp[0], t);

                        retVal = Convert.ToDateTime(input);
                    }
                }
            }
            catch
            {
                retVal = DateTime.Now;
            }

            return retVal;
        }

        public static DateTime ToDateTime(string obj)
        {

            var retVal = DateTime.Now;
            var strArr = obj.Split(' ');
            var lenStrArr = strArr.Length;
            try
            {
                if (CheckDateTime())
                {
                    return Convert.ToDateTime(obj);
                }

                if (lenStrArr >= 1)
                {
                    var str = strArr[0];
                    var strTemp = str.Split('/');
                    if (strTemp.Length == 3)
                    {
                        var t = string.Empty;
                        if (lenStrArr == 2)
                        {
                            t = strArr[1];
                        }
                        var input = string.Format("{0}-{1}-{2} {3}", strTemp[2], strTemp[1], strTemp[0], t);

                        retVal = Convert.ToDateTime(input);
                    }
                }
            }
            catch
            {
                if (lenStrArr >= 1)
                {
                    var str = strArr[0];
                    var strTemp = str.Split('/');
                    if (strTemp.Length == 3)
                    {
                        var t = string.Empty;
                        if (lenStrArr == 2)
                        {
                            t = strArr[1];
                        }
                        var input = string.Format("{0}-{2}-{1} {3}", strTemp[2], strTemp[1], strTemp[0], t);

                        retVal = Convert.ToDateTime(input);
                    }
                }
            }

            return retVal;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public static string Replace(string template, IEnumerable<TokenEmailItem> tokens, bool htmlEncode)
        //{
        //    if (string.IsNullOrWhiteSpace(template))
        //        throw new ArgumentNullException("template");

        //    if (tokens == null)
        //        throw new ArgumentNullException("tokens");

        //    foreach (var token in tokens)
        //    {
        //        var tokenValue = token.Value;
        //        //do not encode URLs
        //        if (htmlEncode && !token.NeverHtmlEncoded)
        //            tokenValue = HttpUtility.HtmlEncode(tokenValue);
        //        template = Replace(template, String.Format(@"%{0}%", token.Key), tokenValue);
        //    }
        //    return template;

        //}
        private static readonly StringComparison _stringComparison;
        private static string Replace(string original, string pattern, string replacement)
        {
            if (_stringComparison == StringComparison.Ordinal)
            {
                return original.Replace(pattern, replacement);
            }
            int position0, position1;
            int count = position0 = 0;
            var inc = (original.Length / pattern.Length) * (replacement.Length - pattern.Length);
            var chars = new char[original.Length + Math.Max(0, inc)];
            while ((position1 = original.IndexOf(pattern, position0, _stringComparison)) != -1)
            {
                for (var i = position0; i < position1; ++i)
                    chars[count++] = original[i];
                foreach (var t in replacement)
                    chars[count++] = t;
                position0 = position1 + pattern.Length;
            }
            if (position0 == 0) return original;
            for (var i = position0; i < original.Length; ++i)
                chars[count++] = original[i];
            return new string(chars, 0, count);
        }

        public static DateTime GetCurrentCultureDate(DateTime myDate)
        {
            try
            {
                var newDate = Convert.ToDateTime("31/12/2015");
                return Convert.ToDateTime(myDate.ToString("dd/MM/yyyy"));
            }
            catch (Exception)
            {
                return Convert.ToDateTime(myDate.ToString("MM/dd/yyyy"));
            }
        }

        public static bool IsDdMmYyyyDate()
        {
            try
            {
                var newDate = Convert.ToDateTime("31/12/2015");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }




        public static string CommaPriceVi(object price)
        {
            return string.Format("{0:N0}", price).Replace(".", ",");
        }


        public static List<string> GetValuesArrayTag(string ltsSourceValues)
        {
            var ltsValues = new List<string>();
            if (string.IsNullOrEmpty(ltsSourceValues)) return ltsValues;
            var lts = ltsSourceValues.Replace(", ", ",");
            if (lts.Contains(","))
            {
                ltsValues = Regex.Split(lts, ",").ToList();
            }
            else
                ltsValues.Add(Convert.ToString(ltsSourceValues));
            return ltsValues.Where(x => !string.IsNullOrEmpty(x.ToString())).ToList();
        }
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
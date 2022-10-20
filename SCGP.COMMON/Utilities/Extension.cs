using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace SCGP.COA.COMMON.Utilities
{
    public static class Extension
    {
        private static TimeZoneInfo _bangkokTimeZone
        {
            get
            {
                var _timeId = TimeZoneInfo.Local.Id;
                if (CommonUtil.IsWindows)
                {
                    _timeId = "SE Asia Standard Time";
                }
                else if (CommonUtil.IsLinux)
                {
                    _timeId = "Asia/Bangkok";
                }
                return TimeZoneInfo.FindSystemTimeZoneById(_timeId);
            }
        }
        public static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        public static bool IsLinux => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        #region datatime
        public static DateTime GetLocalDate(this DateTime s)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(s.ToUniversalTime(), _bangkokTimeZone);
        }
        public static bool IsDefaultDateTime(this string sDate, string format)
        {
            if (sDate == null) return true;
            var defaultValue = format
                .Replace('d', '0')
                .Replace('M', '0')
                .Replace('y','0')
                .Replace('m','0')
                .Replace('H', '0')
                .Replace('h', '0')
                .Replace('s', '0');
            return sDate == defaultValue;
        }
        public static DateTime? StringToDateTime(this string sDate, string format)
        {
            if (sDate == null) return null;
            if (sDate.IsDefaultDateTime(format)) return null;

            DateTime date = DateTime.ParseExact(sDate, format, CultureInfo.InvariantCulture);
            return date;
        }
        public static string StringToNewFormatDateTime(this string sDate, string oldFormat, string newFormat)
        {
            if (sDate == null) return null;
            if (sDate.IsDefaultDateTime(oldFormat)) return null;

            DateTime date = DateTime.ParseExact(sDate, oldFormat, CultureInfo.InvariantCulture);
            return date.ToString(newFormat);
        }
        #endregion

        #region string
        public static string FirstLetterToUpper(this string s)
        {
            if (s == null)
                return null;

            if (s.Length > 1)
                return char.ToUpper(s[0]) + s.Substring(1);

            return s.ToUpper();
        }
        public static List<string> SplitToList(this string s, string splitter = ",")
        {
            if (string.IsNullOrEmpty(s)) return new List<string>();
            else return s.Split(splitter).ToList();
        }
        public static bool TryParseStringToInt(this string sVal)
        {
            if (string.IsNullOrEmpty(sVal)) return false;
            var tmpResult = int.TryParse(sVal, out _);
            if (tmpResult == false)
                return false;
            if (int.Parse(sVal) < 0)
                return false;

            return true;
        }

        public static bool TryParseStringToDecimal(this string sVal)
        {
            if (string.IsNullOrEmpty(sVal)) return false;
            var tmpResult = decimal.TryParse(sVal, out _);
            if (tmpResult == false)
                return false;
            if (decimal.Parse(sVal) < 0)
                return false;

            return true;
        }

        public static bool TryParseStringToLong(this string sVal)
        {
            if (string.IsNullOrEmpty(sVal)) return false;
            var tmpResult = long.TryParse(sVal, out _);
            if (tmpResult == false)
                return false;
            if (long.Parse(sVal) < 0)
                return false;

            return true;
        }

        public static bool IsTrueFalse(this string s,string yesOption = "1",string noOption = "0")
        {
            if (s == yesOption)
            {
                return true;
            }
            else if (s == noOption)
            {
                return false;
            }
            return false;
        }

        public static bool? IsTrueFalseNull(this string s, string yesOption = "1", string noOption = "0")
        {
            if (s == null)
            {
                return null;
            }
            else if (s == yesOption)
            {
                return true;
            }
            else if (s == noOption)
            {
                return false;
            }
            return false;
        }

        public static bool IsNullOrEmpty(this string param)
        {
            return string.IsNullOrEmpty(param);
        }

        #endregion

        #region linq
        public static List<T> Sort<T>(this List<T> searchResult, int? sortOrder, string sortField)
        {
            if (sortOrder == 1)
                searchResult = searchResult.OrderBy(i => i.GetType().GetProperty(sortField).GetValue(i, null)).ToList();
            else if (sortOrder == -1)
                searchResult = searchResult.OrderByDescending(i => i.GetType().GetProperty(sortField).GetValue(i, null)).ToList();

            return searchResult;
        }

        public static IQueryable<T> Sort<T>(this IQueryable<T> en, int? sortOrder, string sortField)
        {
            if (!string.IsNullOrEmpty(sortField))
            {
                var type = typeof(T);
                var prop = type.GetProperty(sortField);
                if (prop != null)
                {
                    var param = Expression.Parameter(type);
                    var expr = Expression.Lambda<Func<T, object>>(
                        Expression.Convert(Expression.Property(param, prop), typeof(object)),
                        param
                    );
                    if (sortOrder == -1)
                        en = en.OrderByDescending(expr);
                    else
                        en = en.OrderBy(expr);
                }
            }
            return en;
        }

        public static IQueryable<T> Page<T>(this IQueryable<T> en, int pageSize, int page)
        {
            if (pageSize == 0)
                pageSize = 10;
            if (page == 0)
                page = 1;

            page = page - 1;
            return en.Skip(page * pageSize).Take(pageSize);
        }

        public static List<T> Page<T>(this List<T> en, int pageSize, int page)
        {
            if (pageSize == 0)
                pageSize = 10;
            if (page == 0)
                page = 1;

            page = page - 1;
            return en.Skip(page * pageSize).Take(pageSize).ToList();
        }

        public static List<List<T>> SplitList<T>(this List<T> data, int nSize)
        {
            var list = new List<List<T>>();

            for (int i = 0; i < data.Count; i += nSize)
            {
                list.Add(data.GetRange(i, Math.Min(nSize, data.Count - i)));
            }

            return list;
        }
        #endregion

        #region exception
        public static string GetMessageError(this Exception e)
        {
            while (e.InnerException != null) e = e.InnerException;
            return e.Message;
        }
        public static string GetMessageErrorAll(this Exception e,string splitMsg = "|")
        {
            string eMsg = e.Message;
            while (e.InnerException != null)
            {
                eMsg += (splitMsg + e.InnerException.Message);
                e = e.InnerException;
            }
            return eMsg;
        }
        #endregion

        #region object
        public static string ToJSON(this object obj)
        {
            if (obj == null) return "";
            return JsonConvert.SerializeObject(obj);
        }
        public static string ToJSONRemoveNull(this object obj)
        {
            return JsonConvert.SerializeObject(obj,
                      Formatting.None,
                      new JsonSerializerSettings
                      {
                          NullValueHandling = NullValueHandling.Ignore
                      });
        }
        public static T ToObject<T>(this string s)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(s);
            }
            catch (Exception e)
            {
                var ex = new Exception("can't deserializeObject object:" + s,e);
                throw ex;
            }

        }

        public static Dictionary<string, TValue> ToDictionary<TValue>(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, TValue>>(json);
            return dictionary;
        }
        #endregion

        #region enum
        /// <summary>
        /// Get the Description from the DescriptionAttribute.
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue)
        {
            return enumValue.GetType()
                       .GetMember(enumValue.ToString())
                       .First()
                       .GetCustomAttribute<DescriptionAttribute>()?
                       .Description ?? string.Empty;
        }
        #endregion enum

        #region number
        public static string ToFormat(this decimal num,string decimalDigit = "")
        {
            return num.ToString("N" + decimalDigit);
        }
        public static string ToFormat(this decimal? num, string decimalDigit = "")
        {
            if (!num.HasValue) return null;
            return num.Value.ToFormat(decimalDigit);
        }
        public static string ToFormat(this int num, string decimalDigit = "")
        {
            return num.ToString("N" + decimalDigit);
        }
        public static string ToFormat(this int? num, string decimalDigit = "")
        {
            if (!num.HasValue) return null;
            return num.Value.ToFormat(decimalDigit);
        }

        public static bool IsNullOrZero(this decimal? num)
        {
            return !num.HasValue || num == 0;
        }
        #endregion

        #region List
        public static bool AnyAndNotNull<T>(this IEnumerable<T> l)
        {
            if (l == null) return false;
            if (l.Any() == false) return false;
            return true;
        }

        public static string ListToString(this List<string> strs)
        {
            if (strs.AnyAndNotNull() == false) return "";
            var r = String.Join(",", strs);
            return r;
        }
        #endregion
    }
}

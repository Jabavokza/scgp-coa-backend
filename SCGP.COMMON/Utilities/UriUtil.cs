using System;
using System.Collections.Generic;
using System.Text;

namespace SCGP.COA.COMMON.Utilities
{
    public class UriUtil
    {
        public static string BtoaEncrypt(string text)
        {
            byte[] bytes = Encoding.GetEncoding(28591).GetBytes(text);
            string res = Convert.ToBase64String(bytes);
            return res;
        }

        public static string AtobDecrypt(string text)
        {
            byte[] bytes = Convert.FromBase64String(text);
            string res = Encoding.GetEncoding(28591).GetString(bytes);
            return res;
        }

        public static string EncodeURI(string text)
        {
            string res = Uri.EscapeDataString(text);
            return res;
        }

        public static string DecodeURI(string text)
        {
            string res = Uri.UnescapeDataString(text);
            return res;
        }
    }
}

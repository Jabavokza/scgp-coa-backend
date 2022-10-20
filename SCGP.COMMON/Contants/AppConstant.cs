using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SCGP.COA.COMMON.Constants
{
    public class AppConstant
    {
        public class INTERFACE
        {
            public enum MESSAGE_TYPE
            {
                [Description("application/json")]
                JSON,
                [Description("application/xml")]
                XML,
                [Description("application/x-www-form-urlencoded")]
                URL_ENCODE,
            }
        }

        public class FORMAT
        {
            public const string BASIC_DATETIME_SEC = "dd/MM/yyyy HH:mm:ss";
            public const string WEB_DATE = "yyyy-MM-dd";
            public const string WEB_DATETIME = "yyyy-MM-dd HH:mm";
            public const string WEB_DATETIME_SEC = "yyyy-MM-dd HH:mm:ss";
            public const string EMAIL_DATE = "dd/MM/yyyy";
            public const string TIME = "HH:mm";
        }

        public class SPLIT_STRING 
        {
            public const string EMAIL_ADDRESS = "@";
        }

        public static class Domain
        {
            public const string CEMENTHAI = "cementhai";
            public const string EXTERNAL = "external";
        }

        public static class Server
        {
            public const string CEMENTHAI = "cementhai.com";
        }

        public static class PasswordPolicy
        {
            public const int MINIMUM_LENGTH = 8;
            public const int UPPER_CASE_LENGTH = 1;
            public const int LOWER_CASE_LENGTH = 1;
            public const int NUMERIC_LENGTH = 1;
            public const int NON_ALPHABET_LENGTH = 1;
        }
    }
}

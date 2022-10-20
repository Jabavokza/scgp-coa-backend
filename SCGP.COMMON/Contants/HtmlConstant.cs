namespace SCGP.COA.COMMON.Contants
{
    public class HtmlConstant
    {
        public const string TAG_OPEN = "<{0}>";
        public const string TAG_CLOSE = "</{0}>";

        public class TAG
        {
            public const string B = "b";
            public const string DIV = "div";
            public const string TABLE = "table";
            public const string THERD = "thead";
            public const string TBODY = "tbody";
            public const string TFOOT = "tfoot";
            public const string TR = "tr";
            public const string TD = "td";
            public const string TH = "th";
            public const string FONT = "font";
            public const string BR = "<br/>";
            public const string SPAN = "span";
            public const string BLANK_SPACE = "&nbsp;";
        }

        public class PROPERTY
        {
            public const string CLASS = "class";
            public const string COLSPAN = "colspan";
            public const string ROWSPAN = "rowspan";
            public const string ID = "id";
            public const string SPAN = "span";
            //public const string H2 = "h2";
            //public const string WIDTH = "width";
        }

        public class STYLE
        {
            public const string PROPERTY = "style=\"{0}\"";

            public class BORDER
            {
                public const string TOP = "border-top : 1px solid black;";
                public const string LEFT = "border-left : 1px solid black;";
                public const string RIGHT = "border-right : 1px solid black;";
                public const string BOTTOM = "border-bottom : 1px solid black;";
                public const string LR = LEFT + RIGHT;
                public const string COLLAPSE = "border-collapse: collapse;";
            }

            public class SETUP
            {
                public const string EXACT_COLOR = "-webkit-print-color-adjust: exact;";
                public const string BREAK_INSIDE = "page-break-inside:auto;";
                public const string FONT_BOLD = "font-weight: bold;";
            }
        }

    }
}

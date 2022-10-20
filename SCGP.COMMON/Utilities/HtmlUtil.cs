using SCGP.COA.COMMON.Constants;
using SCGP.COA.COMMON.Contants;
using SCGP.COA.COMMON.Models;

namespace SCGP.COA.COMMON.Utilities
{
    public class HTMLUtil
    {
        public static string GetStyleValue(string style, string propName)
        {
            var value = "";
            if (style != null)
            {
                var styles = style.Split(';').ToList();
                var targetStyle = styles.FirstOrDefault(x => x.StartsWith(propName, StringComparison.OrdinalIgnoreCase));
                if (targetStyle != null)
                {
                    var targetStyleData = targetStyle.Split(':').ToList();
                    value = targetStyleData[1] != null ? targetStyleData[1] : targetStyleData[0];
                }
            }
            return value;
        }

        public static string SetTag(string tag, string value = null, string style = null, params HTMLPropertyModel[] props)
        {
            var html = OpenTag(tag, style, props);

            html += (value ?? "");
            html += string.Format(HtmlConstant.TAG_CLOSE, tag);

            return html;
        }


        public static string OpenTag(string tag, string style = null, params HTMLPropertyModel[] props)
        {
            var html = "";
            if (string.IsNullOrEmpty(tag))
            {
                return html;
            }

            html = string.Format(HtmlConstant.TAG_OPEN, tag);

            if (!string.IsNullOrEmpty(style))
            {
                html = SetStyleToTag(html, style);
            }

            if (props != null && props.Count() > 0)
            {
                props.ToList().ForEach(prop =>
                {
                    if (prop != null)
                        html = SetPropertyToTag(html, prop.PropName, prop.PropValue);
                });
            }

            return html;
        }

        public static string CloseTag(string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                return "";
            }
            return string.Format(HtmlConstant.TAG_CLOSE, tag);
        }

        private static string SetStyleToTag(string tag, string style)
        {
            if (string.IsNullOrEmpty(tag) || string.IsNullOrEmpty(style))
            {
                if (tag == null)
                    tag = "";
                return tag;
            }

            var stlye = string.Format(HtmlConstant.STYLE.PROPERTY, style);
            return tag.Replace(">", $" {stlye} >");
        }

        public static string SetPropertyToTag(string tag, string propName, string propValue)
        {
            if (string.IsNullOrEmpty(propValue) || string.IsNullOrEmpty(propName))
            {
                if (tag == null)
                    tag = "";
                return tag;
            }
            return tag.Replace(">", $" {propName}=\"{propValue}\">");
        }

        public static HTMLPropertyModel SetColSpan(int colSpan)
        {
            var result = new HTMLPropertyModel("colspan", colSpan.ToString());

            if (colSpan == 0)
            {
                result = new HTMLPropertyModel("colspan", "100%");
            }
            return result;
        }
        public static HTMLPropertyModel SetRowSpan(int rowSpan)
        {
            var result = new HTMLPropertyModel("rowspan", rowSpan.ToString());
            return result;
        }
        public static HTMLPropertyModel SetClass(string _class)
        {
            var result = new HTMLPropertyModel("class", _class);
            return result;
        }
        public static string GetDateTimeNow(string dateFormat, string timeFormat)
        {
            string date = DateTime.Now.GetLocalDate().ToString(dateFormat);
            string time = DateTime.Now.GetLocalDate().ToString(timeFormat);

            return date + time;
        }
        public static string GetDateTime(DateTime? value, string dateFormat, string timeFormat, bool breakLine = false)
        {
            string date = value?.ToString(dateFormat);
            string time = value?.ToString(timeFormat);

            if (breakLine)
            {
                return date + "<br/>" + time;
            }
            else
            {
                return date + " " + time;
            }

        }

    }

}

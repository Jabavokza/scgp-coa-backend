using System;
using System.Collections.Generic;
using System.Text;

namespace SCGP.COA.COMMON.Models
{
    public class HTMLModel
    {
        public string HtmlCodeStr { get; set; }

        public HTMLModel(string code)
        {
            HtmlCodeStr = code;
        }
    }

    public class HTMLPropertyModel
    {
        public string PropName { get; set; }
        public string PropValue { get; set; }

        public HTMLPropertyModel(string name, string value)
        {
            PropName = name;
            PropValue = value;
        }
    }

    public class HTMLRowSpanConfigModel
    {
        public string FieldName { get; set; }
        public int BaseRowSpanValue { get; set; }
        public int CurrentRowSpanValue { get; set; }

        public HTMLRowSpanConfigModel(string fieldName, int rowspan)
        {
            FieldName = fieldName;
            BaseRowSpanValue = rowspan;
            CurrentRowSpanValue = rowspan;
        }
    }

    public class PrintHtmlModel
    {
        public string FileName { get; set; }
        public string SheetHtml { get; set; }
    }
}

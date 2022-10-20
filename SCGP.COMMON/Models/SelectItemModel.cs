using System;
using System.Collections.Generic;
using System.Text;

namespace SCGP.COA.COMMON.Models
{
    public class SelectItemModel<T>
    {
        public string Label { get; set; }
        public T Value { get; set; }
        public string Name { get; set; }

        public SelectItemModel()
        { }

        public SelectItemModel(string label, T value)
        {
            Label = label;
            Value = value;
        }

        public SelectItemModel(string name, T value, bool haveValue)
        {
            Label = haveValue ? $"{value}-{name}" : "";
            Value = value;
            Name = name;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace SCGP.COA.COMMON.Models
{
    public class SelectItemDataModel<T, D>
    {
        public string Label { get; set; }
        public T Value { get; set; }
        public string Name { get; set; }
        public D Data { get; set; }

        public SelectItemDataModel()
        { }

        public SelectItemDataModel(string label, T value)
        {
            Label = label;
            Value = value;
        }

        public SelectItemDataModel(string label, T value, D data)
        {
            Label = label;
            Value = value;
            Data = data;
        }

        public SelectItemDataModel(string name, T value, bool haveValue, D data)
        {
            Label = haveValue ? $"{value}-{name}" : "";
            Value = value;
            Name = name;
            Data = data;
        }
    }
}
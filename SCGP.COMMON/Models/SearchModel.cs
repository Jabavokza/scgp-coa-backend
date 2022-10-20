using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCGP.COA.COMMON.Models
{
    public class SearchReqModel<T>
    {
        public int? PageIndex { get; set; } = 1;
        public int? PageSize { get; set; }
        public string SortField { get; set; }
        public int? SortOrder { get; set; }
        public T Criteria { get; set; }
    }

    public class SearchResModel<T>
    {
        public int TotalRecord { get; set; } = 0;
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; }
        public List<T> Result { get; set; } = new List<T>();

    }

}

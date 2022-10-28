namespace SCGP.COA.DATAACCESS.Models
{
    public class MasterMaintainHeaderSearchCriterialModel
    {
        public string FormNumber { get; set; }
        public string PrintedDateTimeFormat { get; set; }
    }

    public class MasterMaintainHeaderSearchResultModel
    {
        public int HeaderId { get; set; }
        public string FormNumber { get; set; }
        public string PrintedDateTimeFormat { get; set; }
    }

    public class MasterMaintainHeaderModel
    {
        public int HeaderId { get; set; }
        public string FormNumber { get; set; }
        public string DatetimeFormat { get; set; }
    }
}

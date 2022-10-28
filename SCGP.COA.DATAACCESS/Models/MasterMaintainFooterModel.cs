namespace SCGP.COA.DATAACCESS.Models
{
    public class MasterMaintainFooterSearchCriterialModel
    {
        public string FormNumber { get; set; }
    }

    public class MasterMaintainFooterSearchResultModel
    {
        public int FooterId { get; set; }
        public string FormNumber { get; set; }
    }

    public class MasterMaintainFooterModel
    {
        public int FooterId { get; set; }
        public string FormNumber { get; set; }
        public string? TextTestcondition { get; set; }
        public string? TextApprovedby { get; set; }
        public string? TextPrintedby { get; set; }
        public string? TextTelnumber { get; set; }
        public string? TextAdditional1 { get; set; }
        public string? TextAdditional2 { get; set; }
    }
}

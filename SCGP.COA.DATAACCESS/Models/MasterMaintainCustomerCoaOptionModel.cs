namespace SCGP.COA.DATAACCESS.Models
{
    public class MasterMaintainCustomerCoaOptionSearchResultModel
    {
        public int CustomerCoaOptionId { get; set; }
        public string CustomerCode { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public bool Pdf { get; set; }
        public bool Text { get; set; }
        public bool Excel { get; set; }
        public bool Dp { get; set; }
        public bool DpBarcode { get; set; }
        public string AdditionFooterText { get; set; } = null!;
    }

    public class MasterMaintainCustomerCoaOptionModel
    {
        public int CustomerCoaOptionId { get; set; }
        public string CustomerCode { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public bool DefaultOutputPdf { get; set; }
        public bool DefaultOutputText { get; set; }
        public bool DefaultOutputExcel { get; set; }
        public bool DefaultOutputDp { get; set; }
        public bool DefaultOutputDpBarcode { get; set; }
        public string CoaFooterText { get; set; } = null!;
    }
}

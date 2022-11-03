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
        public bool EditData { get; set; }
    }

    public class MasterMaintainCustomerCoaOptionModel
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
}

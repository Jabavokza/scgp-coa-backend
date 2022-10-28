namespace SCGP.COA.DATAACCESS.Models
{
    public class MasterMaintainFormCoaSearchResultModel
    {
        public int FormCoaId { get; set; }
        public int SequenceNo { get; set; }
        public string? Grade { get; set; }
        public decimal? Gram { get; set; }
        public string? MaterialSale { get; set; }
        public string? CustomerCode { get; set; }
        public int? FormPdfId { get; set; }
        public int? FormTextId { get; set; }
        public int? FormExcelId { get; set; }
    }

    public class MasterMaintainFormCoaModel
    {
        public int FormCoaId { get; set; }
        public int SequenceNo { get; set; }
        public string? Grade { get; set; }
        public decimal? Gram { get; set; }
        public string? MaterialSale { get; set; }
        public string? CustomerCode { get; set; }
        public int? FormPdfId { get; set; }
        public int? FormTextId { get; set; }
        public int? FormExcelId { get; set; }
    }
}

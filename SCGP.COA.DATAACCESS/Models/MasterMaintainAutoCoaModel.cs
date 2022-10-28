namespace SCGP.COA.DATAACCESS.Models
{
    public class MasterMaintainAutoCoaSearchResultModel
    {
        public int AutoCoaId { get; set; }
        public string CustomerCode { get; set; }
        public string ShipToCode { get; set; }
        public bool AutocoaActive { get; set; }
    }
    public class MasterMaintainAutoCoaModel
    {
        public int AutoCoaId { get; set; }
        public string CustomerCode { get; set; }
        public string ShipToCode { get; set; }
        public bool AutocoaActive { get; set; }
    }
}

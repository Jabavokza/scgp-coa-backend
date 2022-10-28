namespace SCGP.COA.DATAACCESS.Models
{
    public class MasterMaintainFormSearchCriterialModel
    {
        public int? FormTemplateId { get; set; }
        public string FormName { get; set; }
    }

    public class MasterMaintainFormSearchResultModel
    {
        public int FormId { get; set; }
        public string FormName { get; set; }
        public int FormTemplateId { get; set; }
        public string FormTemplate { get; set; }
    }

    public class MasterMaintainFormModel
    {
        public int FormId { get; set; }
        public string FormNumber { get; set; } = null!;
        public int FormTemplateId { get; set; }
        public int? Property1Id { get; set; }
        public int? Property2Id { get; set; }
        public int? Property3Id { get; set; }
        public int? Property4Id { get; set; }
        public int? Property5Id { get; set; }
        public int? Property6Id { get; set; }
        public int? Property7Id { get; set; }
        public int? Property8Id { get; set; }
        public int? Property9Id { get; set; }
        public int? Property10Id { get; set; }
        public int? Property11Id { get; set; }
        public int? Property12Id { get; set; }
        public int? Property13Id { get; set; }
        public int? Property14Id { get; set; }
        public int? Property15Id { get; set; }
        public int? Property16Id { get; set; }
        public int? Property17Id { get; set; }
        public int? Property18Id { get; set; }
        public int? Property19Id { get; set; }
        public int? Property20Id { get; set; }
    }
}

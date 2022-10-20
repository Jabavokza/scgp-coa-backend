namespace SCGP.COA.DATAACCESS.Models
{
    public class UserSearchCriterialModel
    {
        public string UserName { get; set; }
        public int? Group { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UserSearchResultModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string ShortUserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? IsAdmin { get; set; }
    }
}

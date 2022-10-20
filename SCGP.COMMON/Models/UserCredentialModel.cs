namespace SCGP.COA.COMMON.Models
{
    public class UserCredential
    {
        public UserCredential()
        {
            this.Roles = new List<string>();
        }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public string Domain { get; set; }
        public string ClientId { get; set; }
        public int? GroupId { get; set; }
        public string GroupName { get; set; }
        public List<string> Roles { get; set; }
        public string PhoneNumber { get; set; }
        public string Organization { get; set; }
        public bool IsAdmin { get; set; }
    }
}

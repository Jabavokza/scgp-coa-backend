namespace SCGP.COA.BUSINESSLOGIC.Models
{
    public class TokenQuery
    {
        public string grant_type { get; set; }
        public string? refresh_token { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Domain { get; set; }
    }
    public class TokenModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public int refresh_token_expires_in { get; set; }
    }
}

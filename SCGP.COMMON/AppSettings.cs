namespace SCGP.COA.COMMON
{
    public class AppSettings
    {
        public string Environment { get; set; }
        public string WebUrl { get; set; }
        public string[] AllowedHosts { get; set; }
        public int JwtExpireMinutes { get; set; }
        public int RefreshTokenExpireMinutes { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public string JwtKey { get; set; }
        public string ApiUrl { get; set; }
    }
}

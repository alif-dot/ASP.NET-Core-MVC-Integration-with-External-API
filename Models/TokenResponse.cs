namespace JwtToken.Models
{
    public class TokenResponse
    {
        public string token { get; set; }
        public long token_expire { get; set; }
        public int user_id { get; set; }
        public string user_email { get; set; }
        public string username { get; set; }
        public bool customer_phone { get; set; }
    }
}

using Newtonsoft.Json;

namespace UdemyASP.NETCOREIdenity.Authorization
{
    public class JwtToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = null!;
        [JsonProperty("expires_at")]
        public DateTime ExpiresAt { get; set; }
    }
}

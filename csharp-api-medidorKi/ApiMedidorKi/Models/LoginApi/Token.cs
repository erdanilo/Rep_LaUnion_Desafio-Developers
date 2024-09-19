using Newtonsoft.Json;

namespace ApiMedidorKi.Models.LoginApi
{
    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("Estado")]
        public bool Estado { get; set; }

        [JsonProperty("error_description")]
        public string Error_Description { get; set; }

    }
}
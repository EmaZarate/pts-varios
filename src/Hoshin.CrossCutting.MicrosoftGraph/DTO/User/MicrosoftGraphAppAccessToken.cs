using Newtonsoft.Json;
using System;

namespace Hoshin.CrossCutting.MicrosoftGraph.DTO.User
{
    public class MicrosoftGraphAppAccessToken
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        public DateTime TimeAcquired { get; set; }
    }
}

using Newtonsoft.Json;

namespace Hoshin.CrossCutting.MicrosoftGraph.DTO.User
{
    public class MicrosoftGraphUserData
    {
        public string Id { get; set; }
        public string UserPrincipalName { get; set; }
        public string Mail { get; set; }
        public string JobTitle { get; set; }
        [JsonProperty("givenName")]
        public string FirstName { get; set; }
        [JsonProperty("surname")]
        public string LastName { get; set; }
        public string MobilePhone { get; set; }
        public string OfficeLocation { get; set; }
        public MicrosoftGraphPictureData Picture { get; set; }
    }
}

using Newtonsoft.Json;

namespace Hoshin.CrossCutting.MicrosoftGraph.DTO.User
{
    public class MicrosoftGraphPicture
    {
        public int Height { get; set; }
        public int Width { get; set; }
        [JsonProperty("is_silhouette")]
        public bool IsSilhouette { get; set; }
        public string Url { get; set; }
    }
}

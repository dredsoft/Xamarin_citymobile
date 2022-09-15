using Newtonsoft.Json;

namespace CityApp.Models.Models.Base
{
    public class HttpResponseErrorModel
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }

        public override string ToString() => $"Code {Code}: Message {Message}";
    }
}

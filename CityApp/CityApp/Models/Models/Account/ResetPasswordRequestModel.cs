using Newtonsoft.Json;

namespace CityApp.Models.Models.Account
{
    public class ResetPasswordRequestModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}

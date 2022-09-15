using CityApp.Models.Enums;
using Newtonsoft.Json;

namespace CityApp.Models.Models.Base
{
    public class UserContextModel
    {
	    [JsonProperty("id")]
	    public string Id { get; set; }

	    [JsonProperty("email")]
	    public string Email { get; set; }

	    [JsonProperty("firstName")]
	    public string FirstName { get; set; }

	    [JsonProperty("lastName")]
	    public string LastName { get; set; }

	    [JsonProperty("permission")]
	    public SystemPermissions Permission { get; set; }

	    [JsonProperty("token")]
	    public string Token { get; set; }

	    [JsonProperty("expires")]
	    public long Expires { get; set; }

	}
}

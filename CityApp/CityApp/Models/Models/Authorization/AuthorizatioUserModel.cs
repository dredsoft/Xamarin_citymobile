using CityApp.Models.Models.Base;
using Newtonsoft.Json;

namespace CityApp.Models.Models.Authorization
{
    public class AuthorizatioUserModel : UserContextModel
	{
        [JsonProperty("profileImageUrl")]
        public string ProfileImageUrl { get; set; }

		public string FullName => $"{FirstName} {LastName}";
	}
}

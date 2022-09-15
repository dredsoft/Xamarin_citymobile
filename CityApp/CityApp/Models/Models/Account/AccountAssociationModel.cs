using CityApp.Models.Enums;
using Newtonsoft.Json;

namespace CityApp.Models.Models.Account
{
    public class AccountAssociationModel
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("accountNumber")]
        public long AccountNumber { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }

        [JsonProperty("expirationUtc")]
        public long? ExpirationUtc { get; set; }

        [JsonProperty("features")]
        public AccountFeatures Features { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("permissions")]
        public AccountPermissions Permissions { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}

using System;
using Newtonsoft.Json;

namespace CityApp.Models.Models.Account
{
    public class AccountModel
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("accountNumber")]
        public long AccountNumber { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }

        [JsonProperty("distance")]
        public long Distance { get; set; }

        [JsonProperty("expirationUtc")]
        public DateTime? ExpirationUtc { get; set; }

        [JsonProperty("latitude")]
        public long Latitude { get; set; }

        [JsonProperty("longitude")]
        public long Longitude { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

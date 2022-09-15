using System;
using Newtonsoft.Json;

namespace CityApp.Models.Models.Account
{
    public class GetAccoutsRequestModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("latitude")]
        public decimal Latitude { get; set; }
        [JsonProperty("longitude")]
        public decimal Longitude { get; set; }
        [JsonProperty("accountId")]
        public Guid AccountId { get; set; }
        [JsonProperty("accountNumber")]
        public long AccountNumber { get; set; }
        [JsonProperty("expirationUtc")]
        public DateTime? ExpirationUtc { get; set; }
        [JsonProperty("disabled")]
        public bool Disabled { get; set; }
        [JsonProperty("Distance")]
        public decimal Distance { get; set; }
    }
}

using System;
using CityApp.Models.Enums;
using Newtonsoft.Json;

namespace CityApp.Models.Models.Violation
{
    public class ViolationModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("customName")]
        public string CustomName { get; set; }

        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }

        [JsonProperty("typeName")]
        public string TypeName { get; set; }

        [JsonProperty("helpUrl")]
        public string HelpUrl { get; set; }

        [JsonProperty("customHelpUrl")]
        public string CustomHelpUrl { get; set; }

        [JsonProperty("code")]
        public object Code { get; set; }

        [JsonProperty("fee")]
        public object Fee { get; set; }

        [JsonProperty("actions")]
        public ViolationActions Actions { get; set; }

        [JsonProperty("customActions")]
        public ViolationActions CustomActions { get; set; }

        [JsonProperty("requiredFields")]
        public ViolationRequiredFields RequiredFields { get; set; }

        [JsonProperty("customRequiredFields")]
        public ViolationRequiredFields CustomRequiredFields { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("customDescription")]
        public string CustomDescription { get; set; }

        [JsonProperty("reminderMinutes")]
        public object ReminderMinutes { get; set; }

        [JsonProperty("reminderMessage")]
        public object ReminderMessage { get; set; }

        [JsonProperty("displayDescription")]
        public string DisplayDescription { get; set; }

        [JsonProperty("displayRequiredFields")]
        public ViolationRequiredFields DisplayRequiredFields { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("displayActions")]
        public ViolationActions DisplayActions { get; set; }

        [JsonProperty("displayHelpUrl")]
        public string DisplayHelpUrl { get; set; }

        [JsonProperty("violationQuestion")]
        public object[] ViolationQuestion { get; set; }
    }
}

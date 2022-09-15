using System.Collections.Generic;
using Newtonsoft.Json;

namespace CityApp.Models.Models.Citation
{
    public class CitationsModel
    {
		#region Properties

	    [JsonProperty("citationList")]
	    public IEnumerable<CitationModel> CitationModel { get; set; }

	    [JsonProperty("createdById")]
	    public string CreatedById { get; set; }

	    [JsonProperty("createdFrom")]
	    public object CreatedFrom { get; set; }

	    [JsonProperty("createdTo")]
	    public object CreatedTo { get; set; }

	    [JsonProperty("licensePlate")]
	    public object LicensePlate { get; set; }

	    [JsonProperty("pageSize")]
	    public long PageSize { get; set; }

	    [JsonProperty("page")]
	    public long Page { get; set; }

	    [JsonProperty("totalItems")]
	    public long TotalItems { get; set; }

	    [JsonProperty("itemsPerPage")]
	    public long ItemsPerPage { get; set; }

	    [JsonProperty("currentPage")]
	    public long CurrentPage { get; set; }

		#endregion
	}
}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CityApp.Models.Models.Citation
{
    public class CitationModel
    {
		#region Properties

		[JsonProperty("id")]
		public Guid CitationId { get; set; }

	    [JsonProperty("violationId")]
	    public Guid ViolationId { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("assignedTo")]
		public object AssignedTo { get; set; }

		[JsonProperty("assignedToId")]
		public object AssignedToId { get; set; }

		[JsonProperty("assignedToFullName")]
		public object AssignedToFullName { get; set; }

		[JsonProperty("violationName")]
		public string ViolationName { get; set; }

		[JsonProperty("violationCode")]
		public string ViolationCode { get; set; }

		[JsonProperty("latitude")]
		public double Latitude { get; set; }

		[JsonProperty("longitude")]
		public double Longitude { get; set; }

		[JsonProperty("licensePlate")]
		public string LicensePlate { get; set; }

		[JsonProperty("locationDescription")]
		public string LocationDescription { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("citationNumber")]
		public long CitationNumber { get; set; }

		[JsonProperty("vehicleColor")]
		public string VehicleColor { get; set; }

		[JsonProperty("vehicleMake")]
		public string VehicleMake { get; set; }

		[JsonProperty("vehicleModel")]
		public string VehicleModel { get; set; }

		[JsonProperty("vehicleType")]
		public string VehicleType { get; set; }

		[JsonProperty("city")]
		public string City { get; set; }

		[JsonProperty("state")]
		public string State { get; set; }

		[JsonProperty("street")]
		public string Street { get; set; }

		[JsonProperty("postalCode")]
		public string PostalCode { get; set; }

		[JsonProperty("createUtc")]
		public System.DateTime CreateUtc { get; set; }

		[JsonProperty("vinNumber")]
		public string VinNumber { get; set; }

		public string Thumbnail { get; set; }

		[JsonProperty("comments")]
		public object[] Comments { get; set; }

		[JsonProperty("citationAttachment")]
		public IList<CitationAttachment> CitationAttachment { get; set; }

		#endregion
	}
}

using System;
using CityApp.Models.Enums;

namespace CityApp.Models.Models.Citation
{
    public class BaseCitationModel
    {
		    public Guid? Id { get; set; }
		    public Guid? ViolationId { get; set; }

		    public CitationStatus Status { get; set; }

		    public Guid? AssignedToId { get; set; }

		    public decimal Latitude { get; set; }

		    public decimal Longitude { get; set; }

		    public long CitationNumber { get; set; }

		    public string LocationDescription { get; set; }

		    public string Description { get; set; }

		    public string VinNumber { get; set; }
	}
}


using System.Collections.Generic;
using CityApp.Models.Enums;
using Newtonsoft.Json;

namespace CityApp.Models.Models.Citation
{
	public class CitationAttachment
	{
		#region Properties

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("fileName")]
		public string FileName { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("mimeType")]
		public string MimeType { get; set; }

		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("attachmentType")]
		public CitationAttachmentType AttachmentType { get; set; }

		[JsonProperty("contentLength")]
		public long ContentLength { get; set; }

		[JsonProperty("duration")]
		public long? Duration { get; set; }

		[JsonProperty("displayDuration")]
		public string DisplayDuration { get; set; }

		[JsonProperty("citations")]
		public IEnumerable<CitationAttachment> Citations { get; set; }

		#endregion
	}
}

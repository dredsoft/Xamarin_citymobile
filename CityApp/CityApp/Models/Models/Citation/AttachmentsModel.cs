using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CityApp.Models.Models.Citation
{
	public class AttachmentsModel
	{
		[JsonProperty("attachmentId")]
		public Guid AttachmentId { get; set; }

		[JsonProperty("citationId")]
		public Guid CitationId { get; set; }

		public string DeviceReceipt { get; set; }

		public string DevicePublicKey { get; set; }

		public List<CitationAttachment> Attachments { get; set; }
	}
}

using Newtonsoft.Json;

namespace CityApp.Models.Models.Base.JsonOperations
{
	public class JsonOperationResultGeneric<T> : IJsonOperationResult<T>
	{
		[JsonProperty("data")]
		public T Data { get; set; }

		[JsonProperty("errors")]
		public HttpResponseErrorModel[] Errors { get; set; }

		[JsonProperty("message")]
		public string Message { get; set; }

		[JsonProperty("success")]
		public bool IsSuccessfull { get; set; }
	}
}

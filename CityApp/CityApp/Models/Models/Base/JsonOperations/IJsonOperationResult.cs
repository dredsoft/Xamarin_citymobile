namespace CityApp.Models.Models.Base.JsonOperations
{
    public interface IJsonOperationResult
    {
	    HttpResponseErrorModel[] Errors { get; set; }

	    string Message { get; set; }

	    bool IsSuccessfull { get; set; }
	}
}

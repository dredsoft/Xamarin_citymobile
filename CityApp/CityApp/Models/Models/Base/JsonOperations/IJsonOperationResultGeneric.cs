namespace CityApp.Models.Models.Base.JsonOperations
{
    public interface IJsonOperationResult<T> : IJsonOperationResult
    {
	    T Data { get; set; }
	}
}

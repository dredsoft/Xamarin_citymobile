namespace CityApp.Utilities.Validation.Abstractions
{
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; set; }
        bool Check(T value);
    }
}

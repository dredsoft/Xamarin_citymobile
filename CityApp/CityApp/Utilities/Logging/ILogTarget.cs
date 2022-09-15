namespace CityApp.Utilities.Logging
{
    public interface ILogTarget
    {
        ILogger Logger { get; }
    }
}

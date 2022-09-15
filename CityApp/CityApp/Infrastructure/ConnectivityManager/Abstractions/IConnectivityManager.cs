namespace CityApp.Infrastructure.ConnectivityManager.Abstractions
{
    public interface IConnectivityManager
    {
	    #region Properties

	    bool IsConnected { get; }

	    #endregion
	}
}

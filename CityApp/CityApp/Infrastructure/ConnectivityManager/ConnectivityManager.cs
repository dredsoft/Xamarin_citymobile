using CityApp.Infrastructure.ConnectivityManager.Abstractions;
using CityApp.Infrastructure.ConnectivityManager.Implementations;

namespace CityApp.Infrastructure.ConnectivityManager
{
    public class ConnectivityManager
    {
	    #region Constructors

	    static ConnectivityManager()
	    {
		    Instance = new CrossConnectivityManagerImplementation();
	    }

	    private ConnectivityManager()
	    {
	    }

	    #endregion

	    #region Properties

	    public static IConnectivityManager Instance { get; }

	    #endregion
    }
}


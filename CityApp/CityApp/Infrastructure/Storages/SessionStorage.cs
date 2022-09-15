using CityApp.Infrastructure.Storages.Abstractions;
using CityApp.Infrastructure.Storages.Implementations;

namespace CityApp.Infrastructure.Storages
{
    public class SessionStorage
    {
	    #region Constructors

	    static SessionStorage()
	    {
		    Instance = new SessionStorageImplementation();
	    }

	    private SessionStorage()
	    {
	    }

	    #endregion

	    #region Properties

	    public static ISessionStorage Instance { get; }

	    #endregion
	}
}

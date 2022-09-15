using CityApp.Infrastructure.Storages.Abstractions;
using CityApp.Infrastructure.Storages.Implementations;

namespace CityApp.Infrastructure.Storages
{
	public class SettingStorage
    {
	    #region Constructors

	    static SettingStorage()
	    {
		    Instance = new CrossSettingsStorageImplementation();
	    }

	    private SettingStorage()
	    {
	    }

	    #endregion

	    #region Properties

	    public static ISettingStorage Instance { get; }

	    #endregion
	}
}

using CityApp.Models.Models.Authorization;
using CityApp.Models.Models.Base;
using Zebra.Sdk.Printer.Discovery;

namespace CityApp.Infrastructure.Storages.Abstractions
{
    public interface ISessionStorage
    {
		#region Properties

	    AuthorizatioUserModel UserContext { get; set; }

	    DeviceContextModel DeviceContext { get; set; }

        DiscoveredPrinter DiscoveredPrinterContext { get; set; }

        #endregion

        #region Methods

        void Set<T>(string key, T value);

	    T Get<T>(string key);

	    bool TryGet<T>(string key, out T value);

	    bool Contains(string key);

	    bool TryRemove(string key);

	    bool TryGetAndRemove<T>(string key, out T value);

	    void Clear();

	    #endregion
	}
}

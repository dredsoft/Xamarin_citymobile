using CityApp.Infrastructure.ConnectivityManager.Abstractions;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;

namespace CityApp.Infrastructure.ConnectivityManager.Implementations
{
	public class CrossConnectivityManagerImplementation : IConnectivityManager
	{
		#region Private fields

		private readonly IConnectivity _crossConnectivity = CrossConnectivity.Current;

		#endregion

		#region Implementation of IConnectivityManager

		public bool IsConnected => _crossConnectivity.IsConnected;

		#endregion
	}
}

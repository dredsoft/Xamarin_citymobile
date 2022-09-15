using Android.OS;
using CityApp.Droid.Services.GovAppService.Abstractions;

namespace CityApp.Droid.Services.GovAppService
{
	public class GovAppServiceBinder : Binder
	{
		#region Properties

		public IGovAppService Service { get; }

		#endregion

		#region Constructors

		public GovAppServiceBinder(IGovAppService service)
		{
			Service = service;
		}

		#endregion
	}
}
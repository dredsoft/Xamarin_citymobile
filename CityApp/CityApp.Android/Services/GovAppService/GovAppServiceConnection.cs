using Android.Content;
using Android.OS;

namespace CityApp.Droid.Services.GovAppService
{
	public class GovAppServiceConnection : Java.Lang.Object, IServiceConnection
	{
		#region Properties

		public bool IsConnected { get; private set; }

		public GovAppServiceBinder Binder { get; private set; }

		#endregion

		#region Constructors

		public GovAppServiceConnection(MainActivity activity)
		{
			IsConnected = false;

			Binder = null;
		}

		#endregion

		#region Public Methods

		public void OnServiceConnected(ComponentName name, IBinder service)
		{
			Binder = service as GovAppServiceBinder;
			IsConnected = Binder != null;
		}

		public void OnServiceDisconnected(ComponentName name)
		{
			IsConnected = false;
			Binder = null;
		}

		#endregion
	}
}
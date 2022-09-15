using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using CityApp.Droid.Services.GovAppService;
using FFImageLoading.Forms.Droid;
using Plugin.Permissions;
using Plugin.MediaManager.Forms.Android;
using Android;
using Android.Support.V4.App;

namespace CityApp.Droid
{
    [Activity(Label = "CityApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {   //BUG: .NET Standart have bug with resolving some nuget packages for android.
		//https://bugzilla.xamarin.com/show_bug.cgi?id=59313
		//https://github.com/Microsoft/msbuild/issues/2615

		#region Properties

	    public GovAppServiceConnection ServiceConnection { get; set; }

		#endregion

		#region Public Methods

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
	    {
		    //base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		    PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
	    }

		#endregion

		#region Protected Methods

		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			InitializePlugins(bundle);

			LoadApplication(new App());

            ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.AccessCoarseLocation }, 1);
        }

	    protected override void OnStart()
		{
			base.OnStart();

			StartBackgroundService();
		}

	    protected override void OnStop()
	    {
		    base.OnStop();

		    if (ServiceConnection.IsConnected)
		    {
			    UnbindService(ServiceConnection);
			}
		}

		#endregion

		#region Private Methods

		public void InitializePlugins(Bundle bundle)
		{
			CachedImageRenderer.Init(false);
		    VideoViewRenderer.Init();
            Xamarin.FormsGoogleMaps.Init(this, bundle);
			UserDialogs.Init(this);
		}

	    private void StartBackgroundService()
	    {
			if (ServiceConnection == null)
			{
				ServiceConnection = new GovAppServiceConnection(this);
			}

			var serviceToStart = new Intent(this, typeof(GovAppService));
			BindService(serviceToStart, ServiceConnection, Bind.AutoCreate);
		}

		#endregion
	}
}


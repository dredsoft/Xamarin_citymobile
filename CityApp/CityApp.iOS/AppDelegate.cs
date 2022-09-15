using FFImageLoading.Forms.Touch;
using Foundation;
using Plugin.MediaManager.Forms.iOS;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace CityApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : FormsApplicationDelegate
	{
		#region Public Methods

		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//

		//BUG: .NET Standart have bug with resolving some nuget packages for android. Remove redunant packages (PropertyChanged.Fody, Newtonsoft.Json) once the issue will be resolved 
		//https://bugzilla.xamarin.com/show_bug.cgi?id=59313
		//https://github.com/Microsoft/msbuild/issues/2615

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{

			Xamarin.Forms.Forms.Init();

			InitializePlugins();

			LoadApplication(new CityApp.App());

			return base.FinishedLaunching(app, options);
		}

		#endregion

		#region Private Methods

		public void InitializePlugins()
		{
			CachedImageRenderer.Init();
			VideoViewRenderer.Init();
			Xamarin.FormsGoogleMaps.Init(IOSPlatformConstant.GOOGLE_MAPS_IOS_API_KEY);
		}

		#endregion


	}
}

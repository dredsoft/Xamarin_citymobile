using System;
using System.Threading.Tasks;
using CityApp.Core.ViewModels.Abstractions;
using CityApp.Infrastructure.NavigationManager;
using CityApp.Resources;
using CityApp.Utilities.Logging;
using CityApp.Utilities.UserDialogs;
using CityApp.Utilities.UserDialogs.Components.Alert;
using Microsoft.AppCenter.Crashes;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace CityApp.Modules.Violations.RecordViolation
{
    public class RecordViolationViewModel : BaseViewModel
    {
		#region Private Fields

		private MediaFile _video;

	    private Guid _videoId;

		#endregion

		#region Constructors

		public RecordViolationViewModel(INavigationManager navigationManager) : base(navigationManager)
		{

		}

		#endregion

		#region Properties

		public override ILogger Logger => LogManager.GetLog();

	    public bool IsVideoAdded => _video != null;

		public MediaFile Video => _video;

	    public Guid VideoId => _videoId;
	   
	    #endregion

		#region Private Methods

		protected virtual async Task RecordVideo()
		{
			Logger.Trace();

			_video = null;
			_videoId = Guid.Empty;

		    if (await CheckCameraPermisisons())
		    {
			    try
			    {
				    _videoId = Guid.NewGuid();

					_video = await Utilities.XamCrossMedia.CrossMedia.Instance.TakeVideoAsync(new StoreVideoOptions
				    {
					    Name = $"{_videoId.ToString()}{CommonConstants.VIDEO_EXTENSION}",
					    Directory = CommonConstants.VIDEO_DIRECTORY_NAME,
					    DesiredLength = new TimeSpan(0, 1, 0),
					    DefaultCamera = CameraDevice.Rear,
					    SaveToAlbum = false,
						Quality = VideoQuality.Medium,
						CompressionQuality = 75
					});
			    }
			    catch (Exception e)
			    {
					Crashes.TrackError(e);
			    }

				SendPropertyChanged(() => IsVideoAdded);
			}
	    }		

		private async Task<bool> CheckCameraPermisisons()
	    {
		    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakeVideoSupported)
		    {
			   
			    UserDialogs.Instance.Alert.Show(new AlertConfig
			    {
				    Title = AppResources.txtMessage,
				    Message = AppResources.msgNoCameraAvailable,
				    OkText = AppResources.txtOK,
			    });

				return false;
		    }
			var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
		    var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

		    if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
		    {
			    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
			    cameraStatus = results[Permission.Camera];
			    storageStatus = results[Permission.Storage];
		    }

		    if (cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
		    {
			   return true;
		    }

			return false;
	    }

		#endregion
	}
}

using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using CityApp.Infrastructure.NavigationManager;
using CityApp.Infrastructure.Storages;
using CityApp.Infrastructure.Storages.Constants;
using CityApp.Models.Enums;
using CityApp.Models.Extensions;
using CityApp.Models.Models.Violation;
using CityApp.Modules.Location;
using CityApp.Modules.MediaPlayer;
using CityApp.Modules.Violations.RecordViolation;
using CityApp.Modules.Violations.ViolationCreateEdit;
using CityApp.Services;
using CityApp.Services.Violation;
using CityApp.Utilities.ActivityContext;
using CityApp.Utilities.Logging;
using Microsoft.AppCenter.Crashes;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;

namespace CityApp.Modules.Violations.ViolationDetails
{
    public class ViolationDetailsViewModel : RecordViolationViewModel
    {
		#region Fields

		private readonly IVideoService _videoService;
	    private readonly IViolationService _violationService;
		private string _videoSource;
	    private ViolationModel _violation;
	    private ImageSource _videoThumbnailSource;
		private string _description;
		#endregion

		#region Constructors

		public ViolationDetailsViewModel(INavigationManager navigationManager, IViolationService violationService, IVideoService videoService) : base(navigationManager)
		{
			_violationService = violationService;

			_videoService = videoService;
		}

		#endregion

		#region Properties

	    public override ILogger Logger => LogManager.GetLog();

	    public string Description
	    {
		    get => _description;
		    set => SetProperty(ref _description, value);
	    }

		public ImageSource VideoThumbnailSource
	    {
		    get => _videoThumbnailSource;
		    set => SetProperty(ref _videoThumbnailSource, value);
	    }

		#endregion

		#region Commands

		public ICommand WatchVideoCommand => new Command(WatchVideoExecute);

	    public ICommand SubmitViolationCommand => new Command(SubmitViolationExecute);

		#endregion

		#region Public Methods

		public override async Task Init(object args)
		{
			using (ActivityContext.MakeContext(this))
			{
				await base.Init(args).ConfigureAwait(false);

				var violationId = SessionStorage.Instance.Get<Guid>(StorageConstants.VIOLATION_ID_KEY);

				_violation = _violationService.GetViolationById(violationId);

				Title = _violation.DisplayName;
				Description = _violation.DisplayDescription;

				_videoSource = _violation.DisplayHelpUrl;


				if (SessionStorage.Instance.TryGet<byte[]>(_violation.DisplayHelpUrl, out var sessionThumbnail))
				{
					VideoThumbnailSource = ImageSource.FromStream(() => new MemoryStream(sessionThumbnail));
				}

				else if (string.IsNullOrWhiteSpace(_videoSource))
				{
					VideoThumbnailSource = ImageSource.FromFile("default_thumb.png");
				}
				else
				{				
					var thumbnail = _videoService.GetVideoThumbnailFromWebUri(_videoSource);

					var resizedThumbnail = _videoService.ResizeThumbnail(thumbnail, 480, 360);

					SessionStorage.Instance.Set(_violation.DisplayHelpUrl, resizedThumbnail);

					VideoThumbnailSource = ImageSource.FromStream(() => new MemoryStream(thumbnail));
				}
			}
		}

		#endregion

		#region Private Methods

		private async void SubmitViolationExecute()
	    {
		    using (ActivityContext.MakeContext(this))
		    {
			    var position = await GetCurrentLocation();

			    if (position != null)
			    {
				    SessionStorage.Instance.Set(StorageConstants.POSITION_ITEM_KEY, position);
				}

				await RecordVideo();

				if (IsVideoAdded)
			    {
				    SessionStorage.Instance.Set(StorageConstants.VIOLATION_ITEM_KEY, _violation);
					SessionStorage.Instance.Set(StorageConstants.VIOLATION_VIDEO_FILE_KEY, Video);
				    SessionStorage.Instance.Set(StorageConstants.VIDEO_FILE_ID_KEY, VideoId);

					var violationCategory= SessionStorage.Instance.Get<ViolationCategoryClientModel>(StorageConstants.CURRENT_VIOLATION_CATEGORY_KEY);

					if (violationCategory.Name == ViolationTypes.TextingAndDriving.GetTextRepresentation())
				    {
					    await NavigationManager.NavigateToAsync<VirtualViolationCreateEditViewModel>();
					}
				    else
				    {
						await NavigationManager.NavigateToAsync<LocationViewModel>();
					}
				}
			}	
		}

	    private async void WatchVideoExecute()
	    {      
            if (!string.IsNullOrWhiteSpace(_videoSource))
            {
                SessionStorage.Instance.Set(StorageConstants.VIDEO_SOURCE_KEY, _videoSource);

                await NavigationManager.NavigateToAsync<MediaPlayerViewModel>();
            }
        }

	    private async Task<Position> GetCurrentLocation()
	    {
			try
			{
				var locator = CrossGeolocator.Current;
				locator.DesiredAccuracy = 100;
				if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
				{
					return await locator.GetLastKnownLocationAsync();
				}

				return await locator.GetPositionAsync(TimeSpan.FromSeconds(5), null, true);				
			}
			catch (Exception e)
			{
				Crashes.TrackError(e);
			}

			return null;
	    }

		#endregion
	}
}

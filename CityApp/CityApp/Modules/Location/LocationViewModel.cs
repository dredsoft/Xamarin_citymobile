using System.Windows.Input;
using CityApp.Core.ViewModels.Abstractions;
using CityApp.Infrastructure.NavigationManager;
using CityApp.Infrastructure.Storages;
using CityApp.Infrastructure.Storages.Constants;
using CityApp.Modules.Violations.ViolationCreateEdit;
using CityApp.Resources;
using CityApp.Utilities.Logging;
using CityApp.Utilities.UserDialogs;
using CityApp.Utilities.UserDialogs.Components.Alert;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using MapPosition = Xamarin.Forms.GoogleMaps.Position;
using GeoPosition = Plugin.Geolocator.Abstractions.Position;

namespace CityApp.Modules.Location
{
	public class LocationViewModel : BaseViewModel
	{
		#region Private Fields

		private GeoPosition _violationPosition;

		private Map _map;

		private Circle _mapCircle;

		private readonly GeoPosition _currentUserPosition;

		#endregion

		#region Constructors

		public LocationViewModel(INavigationManager navigationManager) : base(navigationManager)
		{
			_currentUserPosition = SessionStorage.Instance.Get<GeoPosition>(StorageConstants.POSITION_ITEM_KEY);

			ViolationPosition = _currentUserPosition;

			Title = AppResources.txtConfirmLocation;

			MapCircle = new Circle
			{
				Radius = Distance.FromKilometers(CommonConstants.LOCATION_RESTRICT_VALUE),
				StrokeColor = Color.RoyalBlue,
				FillColor = Color.Transparent,
				StrokeWidth = 1,
				Center = new MapPosition(_currentUserPosition.Latitude, _currentUserPosition.Longitude)
			};
		}

		#endregion

		#region Properties

		public CameraUpdate InitialCameraPosition => CameraUpdateFactory.NewPositionZoom(
			new MapPosition(ViolationPosition.Latitude, ViolationPosition.Longitude), CommonConstants.MAP_ZOOM_VALUE);

		public override ILogger Logger => LogManager.GetLog();

		public GeoPosition ViolationPosition
		{
			get => _violationPosition;
			set => SetProperty(ref _violationPosition, value);
		}

		public Map Map
		{
			get => _map;
			set => SetProperty(ref _map, value);
		}

		public Circle MapCircle
		{
			get => _mapCircle;
			set => SetProperty(ref _mapCircle, value);
		}

		#endregion

		#region Commands

		public ICommand CameraMovingCommand => new Command<CameraMovingEventArgs>(CameraMovingExecute);

		public ICommand NextCommand => new Command(NextExecute);

		#endregion

		#region Public Methods

		private void CameraMovingExecute(CameraMovingEventArgs cameraMovingEventArgs)
		{
			var target = cameraMovingEventArgs.Position.Target;
			ViolationPosition = new GeoPosition(target.Latitude, target.Longitude);
		}

		public override void PageClosing()
		{
			SessionStorage.Instance.TryRemove(StorageConstants.POSITION_ITEM_KEY);
			SessionStorage.Instance.TryRemove(StorageConstants.VIOLATION_ITEM_KEY);
			SessionStorage.Instance.TryRemove(StorageConstants.VIOLATION_VIDEO_FILE_KEY);
			SessionStorage.Instance.TryRemove(StorageConstants.VIDEO_FILE_ID_KEY);
			SessionStorage.Instance.TryRemove(StorageConstants.THUMBNAIL_ITEM_KEY);
		}

		#endregion

		#region Private Methods

		private async void NextExecute()
		{
			if (ViolationPositionValidate(_currentUserPosition, ViolationPosition))
			{
				SessionStorage.Instance.Set(StorageConstants.POSITION_ITEM_KEY, ViolationPosition);

				await NavigationManager.NavigateToAsync<VirtualViolationCreateEditViewModel>();
			}
			else
			{
				Map.MoveToRegion(MapSpan.FromCenterAndRadius(
					new MapPosition(_currentUserPosition.Latitude, _currentUserPosition.Longitude),
					Distance.FromKilometers(CommonConstants.MAP_ZOOM_DISTANCE_VALUE)));

				UserDialogs.Instance.Alert.Show(new AlertConfig
				{
					Title = AppResources.txtMessage,
					Message = AppResources.msgCanNotSetPin,
					OkText = AppResources.txtOK,
				});
			}
		}

		private bool ViolationPositionValidate(GeoPosition firstPosition, GeoPosition secondPosition) =>
			firstPosition.CalculateDistance(secondPosition, GeolocatorUtils.DistanceUnits.Kilometers) <
			CommonConstants.LOCATION_RESTRICT_VALUE;

		#endregion
	}
}

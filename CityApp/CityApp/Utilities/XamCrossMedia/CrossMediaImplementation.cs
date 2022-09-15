using System.Threading.Tasks;
using Plugin.Media.Abstractions;

namespace CityApp.Utilities.XamCrossMedia
{
    public class CrossMediaImplementation : ICrossMedia
    {
		#region Properties

	    public bool IsCameraAvailable => Plugin.Media.CrossMedia.Current.IsCameraAvailable;

	    public bool IsTakePhotoSupported => Plugin.Media.CrossMedia.Current.IsTakePhotoSupported;

		public bool IsPickPhotoSupported => Plugin.Media.CrossMedia.Current.IsPickPhotoSupported;

		public bool IsTakeVideoSupported => Plugin.Media.CrossMedia.Current.IsTakeVideoSupported;

		public bool IsPickVideoSupported => Plugin.Media.CrossMedia.Current.IsPickVideoSupported;

		#endregion

		#region Methods

	    public async Task<bool> Initialize() => await Plugin.Media.CrossMedia.Current.Initialize();

		public async Task<MediaFile> PickPhotoAsync(PickMediaOptions options = null) => await Plugin.Media.CrossMedia.Current.PickPhotoAsync(options);

		public async Task<MediaFile> TakePhotoAsync(StoreCameraMediaOptions options) => await Plugin.Media.CrossMedia.Current.TakePhotoAsync(options);

		public async Task<MediaFile> PickVideoAsync() => await Plugin.Media.CrossMedia.Current.PickVideoAsync();

		public async Task<MediaFile> TakeVideoAsync(StoreVideoOptions options) => await Plugin.Media.CrossMedia.Current.TakeVideoAsync(options);

		#endregion
	}
}

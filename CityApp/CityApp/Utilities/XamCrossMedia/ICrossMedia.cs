using System.Threading.Tasks;
using Plugin.Media.Abstractions;

namespace CityApp.Utilities.XamCrossMedia
{
	public interface ICrossMedia
	{
		#region Properties

		bool IsCameraAvailable { get; }

		bool IsTakePhotoSupported { get; }

		bool IsPickPhotoSupported { get; }

		bool IsTakeVideoSupported { get; }

		bool IsPickVideoSupported { get; }

		#endregion

		#region Methods

		Task<bool> Initialize();

		Task<MediaFile> PickPhotoAsync(PickMediaOptions options = null);

		Task<MediaFile> TakePhotoAsync(StoreCameraMediaOptions options);

		Task<MediaFile> PickVideoAsync();

		Task<MediaFile> TakeVideoAsync(StoreVideoOptions options);

		#endregion

	}
}

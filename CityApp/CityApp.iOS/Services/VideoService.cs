using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using AVFoundation;
using CityApp.iOS.Services;
using CityApp.Infrastructure.Storages;
using CityApp.Services;
using CoreGraphics;
using CoreMedia;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(VideoService))]
namespace CityApp.iOS.Services
{
	public class VideoService : IVideoService
    {
	    #region Public Methods

	    public byte[] GetVideoThumbnailFromWebUri(string url)
	    {
		    var asset = AVAsset.FromUrl((new NSUrl(url)));
		    return GetImageSource(asset);
	    }

	    public byte[] GetVideoThumbnailFromFile(string path)
	    {
		    var asset = AVAsset.FromUrl(NSUrl.FromFilename(path));
		    return GetImageSource(asset);
	    }

	    public string GetPathForNewVideo()
	    {
		    var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		    var directoryname = Path.Combine(documents, "CityApp");
		    Directory.CreateDirectory(directoryname);
		    string name = $"Video_{SessionStorage.Instance.UserContext.Id}_{DateTime.Now.ToString("hhmmss")}";

		    return Path.ChangeExtension(name, CommonConstants.VIDEO_EXTENSION);
	    }

	    public byte[] ResizeThumbnail(byte[] thumbnail, int width, int height)
	    {
			var originalImage = new UIImage(NSData.FromArray(thumbnail));

		    using (var context = new CGBitmapContext(IntPtr.Zero, width, height, 8, 4 * width,
			    CGColorSpace.CreateDeviceRGB(),
			    CGImageAlphaInfo.PremultipliedFirst))
		    {
			    var imageRect = new RectangleF(0, 0, width, height);

			    context.DrawImage(imageRect, originalImage.CGImage);

			    var resizedImage = UIImage.FromImage(context.ToImage(), 0, originalImage.Orientation);

			    return resizedImage.AsJPEG().ToArray();
		    }
		}

	    public Task<string> CompressVideo(string path)
	    {
		    throw new NotImplementedException();
	    }

	    #endregion

		#region Private Methods

		private byte[] GetImageSource(AVAsset asset)
		{
			using (var imageGenerator = new AVAssetImageGenerator(asset))
			using (var imageRef = imageGenerator.CopyCGImageAtTime(new CMTime(1, 1), out var actualTime, out var error))
			{
				if (imageRef == null)
				{
					return null;
				}
				var image = UIImage.FromImage(imageRef);

				using (NSData imageData = image.AsPNG())
				{
					var myByteArray = new byte[imageData.Length];
					System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, myByteArray, 0, Convert.ToInt32(imageData.Length));
					return myByteArray;
				}
			}
		}

		#endregion
	}
}
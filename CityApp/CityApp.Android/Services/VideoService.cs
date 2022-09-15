using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Media;
using CityApp.Droid.Services;
using CityApp.Droid.Services.Media;
using CityApp.Services;
using Xamarin.Forms;
using Environment = System.Environment;
using File = Java.IO.File;
using Path = System.IO.Path;

[assembly: Dependency(typeof(VideoService))]
namespace CityApp.Droid.Services
{
	public class VideoService : IVideoService
    {
		#region Private Fields

	    private readonly string LOCAL_VIDEO_LOCATION = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "violations");

		#endregion

		#region Implemantation of IVideoService

		public byte[] GetVideoThumbnailFromWebUri(string url)
	    {
		    var retriever = new MediaMetadataRetriever();

		    retriever.SetDataSource(url, new Dictionary<string, string>());

		    return GetImageSource(retriever);
	    }

	    public byte[] GetVideoThumbnailFromFile(string path)
	    {
		    var retriever = new MediaMetadataRetriever();

			retriever.SetDataSource(path);

		    return GetImageSource(retriever);
	    }

	    public string GetPathForNewVideo()
	    {
		    var tempFile = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
		    var tempVideoFile = Path.ChangeExtension(tempFile, CommonConstants.VIDEO_EXTENSION);
		    return Path.Combine(LOCAL_VIDEO_LOCATION, tempVideoFile);
	    }

	    public byte[] ResizeThumbnail(byte[] thumbnail, int width, int height)
	    {
		    var originalImage = BitmapFactory.DecodeByteArray(thumbnail, 0, thumbnail.Length);

		    var resizedImage = Bitmap.CreateScaledBitmap(originalImage, width, height, false);

		    using (var ms = new MemoryStream())
		    {
			    resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);

			    return ms.ToArray();
		    }
		}

	    #endregion

		#region Private Methods

		private byte[] GetImageSource(MediaMetadataRetriever retriever)
		{
			var bitmap = retriever.GetFrameAtTime(0);
			if (bitmap != null)
			{
				var stream = new MemoryStream();
				bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
				byte[] bitmapData = stream.ToArray();
				return bitmapData;
			}
			return null;
		}

	    public async Task<string> CompressVideo(string path)
	    {
		    var inputFIle = new File(path);

		    var videoConverter = new VideoConverter(Forms.Context);

		    var outputFile = await videoConverter.ConvertFileAsync(inputFIle);

		    if (outputFile != null)
		    {
			    inputFIle.Delete();
			}
		    
			return outputFile?.Path;;
		}

		#endregion
	}
}
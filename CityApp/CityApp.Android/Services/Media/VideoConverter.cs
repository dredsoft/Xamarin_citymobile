using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Media;
using Java.IO;

namespace CityApp.Droid.Services.Media
{
	public class VideoConverter
	{
		#region

		private File _inputFile;

		private File _ouputFile;

		private readonly Context _context;

		#endregion

		#region Constructor

		public VideoConverter(Context context)
		{
			_context = context;
		}

		#endregion

		public async Task<File> ConvertFileAsync(
			File inputFile)
		{
			_inputFile = inputFile;
			_ouputFile = new File($"{inputFile.Parent}/{Guid.NewGuid()}.mp4");

			_ouputFile.DeleteOnExit();

			var cmdParams = SetCmdParameters();

			var mediaRetriever = new MediaMetadataRetriever();
			mediaRetriever.SetDataSource(_inputFile.CanonicalPath);

            await FFMpeg.Xamarin.FFmpegLibrary.Run(
                _context,
                cmdParams);

            return _ouputFile;
		}

		#region Private Methods

		private string SetCmdParameters() => $"-i {_inputFile.CanonicalPath} -an -threads 0 -preset ultrafast -s 480x320 -c:v libx264 {_ouputFile.CanonicalPath}";

		#endregion

	}
}
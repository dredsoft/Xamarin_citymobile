using System;
using System.Globalization;
using Autofac;
using CityApp.Services.AmazonService;
using CityApp.Utilities.Logging;
using Xamarin.Forms;

namespace CityApp.Interactions.Converters
{
	public class SubmissionThubnailToUrlConverter : IValueConverter
    {
        private readonly ILogger Logger = LogManager.GetLog();

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Logger.Trace();

			var amazonService = App.Container.Resolve<IAWSS3Service>();

			if (value is string thumbnailKey)
			{
				var url = amazonService.ReadFileUrl(thumbnailKey);

				return ImageSource.FromUri(new Uri(url));
			}

			return null;
		}

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

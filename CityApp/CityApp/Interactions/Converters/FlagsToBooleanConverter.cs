using System;
using System.Globalization;
using Xamarin.Forms;

namespace CityApp.Interactions.Converters
{
	public class FlagsToBooleanConverter : IValueConverter
	{
		#region Implementation of IValueConverter

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Enum castValue && parameter is Enum castFlags)
			{
				return castValue.HasFlag(castFlags);
			}

			throw new Exception("Argument type invalid");
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if ((bool)value)
				return parameter;

			throw new Exception("StringContainsConverter: It's false, I won't bind back.");
		}

		#endregion
	}
}

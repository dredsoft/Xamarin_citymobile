using System;
using System.Globalization;
using Xamarin.Forms;

namespace CityApp.Interactions.Converters
{
	public class StringToBooleanConverter : IValueConverter
    {
	    #region Public Methods

	    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	    {
		    if (targetType != typeof(bool))
		    {
			    throw new ArgumentException("Incorrect target type");
		    }

		    return !string.IsNullOrWhiteSpace(value?.ToString());
	    }

	    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	    {
		    return null;
	    }

	    #endregion
    }
}

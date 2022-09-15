using System;
using System.Globalization;
using Xamarin.Forms;

namespace CityApp.Interactions.Converters
{
    class InverseBooleanConverter : IValueConverter
    {
	    #region Public Methods

	    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	    {
		    return !(bool)value;
	    }

	    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	    {
		    if (targetType != typeof(bool))
		    {
			    throw new ArgumentException("Incorrect value type");
		    }

		    return !(bool)value;
	    }

	    #endregion
	}
}

using System;
using System.Globalization;
using Xamarin.Forms;

namespace CityApp.Interactions.Converters
{
    public class NotNullToBooleanConverter : IValueConverter
    {
        #region Public Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
}

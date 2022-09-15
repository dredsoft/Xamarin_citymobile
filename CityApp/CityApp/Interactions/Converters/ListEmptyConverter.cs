using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CityApp.Interactions.Converters
{
    public class ListEmptyConverter : IValueConverter
    {
        #region Public Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is IList list) {
                return list.Count <= 0;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
}

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace CityApp.Controls.Overrides
{
	public class CityMap : Map
	{
		public static BindableProperty CircleProperty =
			BindableProperty.Create(nameof(Circle), typeof(Circle), typeof(CityMap), propertyChanged: PropertySChanged);

		private static void PropertySChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue != null && oldValue != newValue)
			{
				var map = (CityMap) bindable;
				map.Circles.Clear();
				
				var circles = (Circle)newValue;
				map.Circles.Add(circles);
			}
		}

		public Circle Circle
		{
			get => (Circle) GetValue(CircleProperty);
			set => SetValue(CircleProperty, value);
		}
	}
}

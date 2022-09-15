using Xamarin.Forms;

namespace CityApp.Controls.Overrides
{
    public class CustomEntry : Entry
    {
        public static BindableProperty UnderlineColorProperty =
            BindableProperty.Create(nameof(UnderlineColor), typeof(Color), typeof(CustomEntry), Color.Default);

        public Color UnderlineColor
        {
            get => (Color) GetValue(UnderlineColorProperty);
            set => SetValue(UnderlineColorProperty, value);
        }
    }
}

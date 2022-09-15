using System;
using Xamarin.Forms;

namespace CityApp.Controls.Overrides
{
    public partial class CheckBox
    {
        public static BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CheckBox), false, BindingMode.TwoWay, propertyChanged:
            (bindable, oldValue, newValue) => { (bindable as CheckBox)?.OnChecked(bindable, newValue); });
        public static BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CheckBox), string.Empty);
        public static BindableProperty ImageHeightProperty = BindableProperty.Create(nameof(ImageHeight), typeof(int), typeof(CheckBox), 0);
        public static BindableProperty ImageWidhtProperty = BindableProperty.Create(nameof(ImageWidht), typeof(int), typeof(CheckBox), 0);
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CheckBox), Color.Default);
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CheckBox), 14.0);
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(CheckBox), string.Empty);

        public CheckBox()
        {
            InitializeComponent();
        }

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public int ImageHeight
        {
            get => (int)GetValue(ImageHeightProperty);
            set => SetValue(ImageHeightProperty, value);
        }

        public int ImageWidht
        {
            get => (int)GetValue(ImageWidhtProperty);
            set => SetValue(ImageWidhtProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        private void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            IsChecked = !IsChecked;
        }

        private void OnChecked(BindableObject bindable, object newValue)
        {
            if (bindable is CheckBox)
            {
                var value = newValue as bool?;
                Box.Source = value != null && (bool)value ? "checkbox_checked" : "checkbox_unchecked";
            }
        }
    }
}
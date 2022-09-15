using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Views;
using CityApp.Controls.Overrides;
using CityApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace CityApp.Droid.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context ctxt) : base(ctxt)
        {}

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null)
            {
                return;
            }

            if (Element is CustomEntry customEntry)
            {
                SetUnderlineColor(customEntry);
            }

            Control.Gravity = GravityFlags.CenterVertical;
        }

        private void SetUnderlineColor(CustomEntry customEntry)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Control.BackgroundTintList = ColorStateList.ValueOf(customEntry.UnderlineColor.ToAndroid());
            }
            else
            {
                Control.Background.SetColorFilter(customEntry.UnderlineColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
            }
        }
    }
}
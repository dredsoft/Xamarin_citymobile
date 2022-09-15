using Android.Content;
using Android.Graphics.Drawables;
using CityApp.Controls.Overrides;
using CityApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PageableListView), typeof(PageableListViewRenderer))]
namespace CityApp.Droid.Renderers
{
	public class PageableListViewRenderer : ListViewRenderer
	{
		public PageableListViewRenderer(Context context) : base(context)
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<ListView> elementChangedEventArgs)
		{
			base.OnElementChanged(elementChangedEventArgs);

			var list = (PageableListView) Element;

			if(list.ItemClickDisable)
			{
				Control.ItemClick += null;

				Control.Selector = new StateListDrawable();
			}
		}
	}
}
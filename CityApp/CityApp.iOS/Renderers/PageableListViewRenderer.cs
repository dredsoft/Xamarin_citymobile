using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CityApp.Controls.Overrides;
using CityApp.iOS.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PageableListView), typeof(PageableListViewRenderer))]
namespace CityApp.iOS.Renderers
{
	public class PageableListViewRenderer : ListViewRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				// Unsubscribe from event handlers and cleanup any resources
			}

			if (e.NewElement != null)
			{
				var list = (PageableListView)Element;

				if (list.ItemClickDisable)
				{
					Control.AllowsSelection = false;
				}
			}
		}
	}
}
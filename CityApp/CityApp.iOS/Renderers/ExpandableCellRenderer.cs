using CityApp.Controls.Overrides.Cells;
using CityApp.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExpandableCell), typeof(ExpandableCellRenderer))]
namespace CityApp.iOS.Renderers
{
	public class ExpandableCellRenderer : ViewCellRenderer
	{
		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			if (item is ViewCell vc)
			{
				var sr = vc.View.Measure(tv.Frame.Width, double.PositiveInfinity, MeasureFlags.IncludeMargins);

				if (vc.Height != sr.Request.Height)
				{
					vc.ForceUpdateSize();

					sr = vc.View.Measure(tv.Frame.Width, double.PositiveInfinity, MeasureFlags.IncludeMargins);
					vc.Height = sr.Request.Height;
				}
			}

			return base.GetCell(item, reusableCell, tv);
		}
	}
}
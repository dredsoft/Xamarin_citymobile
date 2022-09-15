using CityApp.Controls.Overrides.Cells;
using CityApp.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomTextCell), typeof(AccountCellRenderer))]
namespace CityApp.iOS.Renderers
{
    public class AccountCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
            return cell;
        }
    }
}
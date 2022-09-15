using Xamarin.Forms;

namespace CityApp.Controls.Overrides.Cells
{
    public class ExpandableCell : ViewCell
    {
	    #region Bindable

	    public static BindableProperty IsExpandedProperty = BindableProperty.Create(
		    nameof(IsExpanded),
		    typeof(bool),
		    typeof(ExpandableCell),
		    false,
		    propertyChanged: IsExpandedPropertyChanged);

	    #endregion

	    #region Properties

	    public bool IsExpanded
	    {
		    get => (bool)GetValue(IsExpandedProperty);
		    set => SetValue(IsExpandedProperty, value);
	    }

	    #endregion

	    #region Event handlers

	    private static void IsExpandedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
	    {
		    var expandableCell = (ExpandableCell)bindable;
		    expandableCell.ForceUpdateSize();
	    }

	    #endregion
	}
}

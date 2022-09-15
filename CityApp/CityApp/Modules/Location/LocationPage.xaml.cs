using CityApp.Core.Pages;
using Xamarin.Forms.Xaml;

namespace CityApp.Modules.Location
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LocationPage : BasePage
	{
		#region Constructors

		public LocationPage()
		{
			InitializeComponent();		
		}

		#endregion

		#region Protected Methods

		//Fix issue with display map on next page

		protected override void OnAppearing()
		{

			if (!GridContainer.Children.Contains(Map))
			{
				GridContainer.Children.Insert(1, Map);
			}

			if (BindingContext is LocationViewModel locationViewModel)
			{
				locationViewModel.Map = Map;
			}
			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			if (GridContainer.Children.Contains(Map))
			{
				GridContainer.Children.Remove(Map);
			}

			base.OnDisappearing();
		}

		#endregion
	}
}
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CityApp.Modules.Home.SubmissionHistory.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VehicleInfromationView : Grid
	{
		#region Bindable

		public static readonly BindableProperty IsMoreDetailsProperty = BindableProperty.Create(
			nameof(IsMoreDetails),
			typeof(bool),
			typeof(VehicleInfromationView),
			false,
			BindingMode.TwoWay);

		#endregion

		#region Constructors

		public VehicleInfromationView ()
		{
			InitializeComponent ();
		}

		#endregion

		#region Properties

		public bool IsMoreDetails
		{
			get => (bool)GetValue(IsMoreDetailsProperty);
			set => SetValue(IsMoreDetailsProperty, value);
		}

		#endregion

		#region Event Handler

		private void ShowMoreDetails_OnTapped(object sender, EventArgs e)
		{
			IsMoreDetails =  !IsMoreDetails;
		}

		#endregion
	}
}
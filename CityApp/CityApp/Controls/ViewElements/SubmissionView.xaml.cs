using Xamarin.Forms;

namespace CityApp.Controls.ViewElements
{
	public partial class SubmissionView : ContentView
	{
		#region Bindable Properties

		public static readonly BindableProperty ThumbnailProperty = BindableProperty.Create(
			nameof(Thumbnail),
			typeof(ImageSource), 
			typeof(SubmissionView), 
			ImageSource.FromFile("default_thumb.png"));

		public static readonly BindableProperty StatusProperty = BindableProperty.Create(
			nameof(Status), 
			typeof(string),
			typeof(SubmissionView));

		public static readonly BindableProperty LocationProperty = BindableProperty.Create(
			nameof(Location),
			typeof(string),
			typeof(SubmissionView));


		#endregion

		#region Constructores

		public SubmissionView()
		{
			InitializeComponent();
		}

		#endregion

		#region Properties

		[TypeConverter(typeof(ImageSourceConverter))]
		public ImageSource Thumbnail
		{
			get => (ImageSource)GetValue(ThumbnailProperty);
			set => SetValue(ThumbnailProperty, value);
		}

		public string Status
		{
			get => (string)GetValue(StatusProperty);
			set => SetValue(StatusProperty, value);
		}

		public string Location
		{
			get => (string)GetValue(LocationProperty);
			set => SetValue(LocationProperty, value);
		}

		#endregion
	}
}
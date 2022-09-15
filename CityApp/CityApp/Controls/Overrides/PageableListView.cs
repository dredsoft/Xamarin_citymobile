using System.Windows.Input;
using CityApp.Models.Extensions;
using Xamarin.Forms;

namespace CityApp.Controls.Overrides
{
	public class PageableListView : ListView
	{
		#region Bindable Properties

		public static readonly BindableProperty PageSizeProperty = BindableProperty.Create(
			nameof(PageSize),
			typeof(int),
			typeof(PageableListView),
			ControlConstants.PAGE_STANDARD_SIZE,
			BindingMode.OneWayToSource);

		public static readonly BindableProperty PageIndexProperty = BindableProperty.Create(
			nameof(PageIndex),
			typeof(int),
			typeof(PageableListView),
			1);

		public static readonly BindableProperty LoadMoreCommandProperty = BindableProperty.Create(
			nameof(LoadMoreCommand),
			typeof(ICommand),
			typeof(PageableListView));

		public static readonly BindableProperty IsLoadingMoreProperty = BindableProperty.Create(
			nameof(IsLoadingMore),
			typeof(bool),
			typeof(PageableListView),
			false,
			propertyChanged: IsLoadingMorePropertyChanged);

		public static readonly BindableProperty IsEmptyListProperty = BindableProperty.Create(
			nameof(IsEmptyList),
			typeof(bool),
			typeof(PageableListView),
			false,
			propertyChanged: IsEmptyListPropertyChanged);

		#endregion

		#region Private Fields

		private readonly ActivityIndicator _loadingActivityIndicator;

		#endregion

		#region Constructors

		public PageableListView(ListViewCachingStrategy cachingStrategy) : base(cachingStrategy)
		{
			_loadingActivityIndicator = new ActivityIndicator
			{
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Color = Color.Gray,
				IsRunning = true,
				IsVisible = false
			};

			var busyIndicatorContainer = new Grid();
			busyIndicatorContainer.Children.Add(_loadingActivityIndicator, 0, 0);

			Footer = busyIndicatorContainer;

			ItemAppearing += OnItemAppearing;
			ItemSelected += OnItemSelected;
		}

		public PageableListView() : this(ListViewCachingStrategy.RetainElement)
		{
		}

		#endregion

		#region Properties

		public bool ItemClickDisable { get; set; }

		public int PageSize
		{
			get => (int)GetValue(PageSizeProperty);
			set => SetValue(PageSizeProperty, value);
		}

		public int PageIndex
		{
			get => (int)GetValue(PageIndexProperty);
			set => SetValue(PageIndexProperty, value);
		}

		public bool IsLoadingMore
		{
			get => (bool)GetValue(IsLoadingMoreProperty);
			set => SetValue(IsLoadingMoreProperty, value);
		}

		public ICommand LoadMoreCommand
		{
			get => (ICommand)GetValue(LoadMoreCommandProperty);
			set => SetValue(LoadMoreCommandProperty, value);
		}

		public bool IsEmptyList
		{
			get => (bool)GetValue(IsEmptyListProperty);
			set => SetValue(IsEmptyListProperty, value);
		}

		public DataTemplate EmptyListViewDataTemplate { get; set; }

		#endregion

		#region Event Handlers

		private static void IsLoadingMorePropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var list = (PageableListView)bindable;
			list._loadingActivityIndicator.IsVisible = (bool)newValue;
		}

		private void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
		{
			var listView = (PageableListView)sender;

			if (LoadMoreCommand == null)
			{
				return;
			}

			if (listView.IsLoadingMore || listView.ItemsSource.IndexOf(e.Item) < (listView.PageIndex) * listView.PageSize - 1)
			{
				return;
			}

			if (LoadMoreCommand.CanExecute(e.Item))
			{
				LoadMoreCommand.Execute(e.Item);
			}
		}

		private void OnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
		{
			if (selectedItemChangedEventArgs.SelectedItem != null)
			{
				((ListView)sender).SelectedItem = null;
			}
		}

		private static void IsEmptyListPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var list = (PageableListView)bindable;
			var newValueBool = (bool)newValue;

			if (list.EmptyListViewDataTemplate != null)
			{
				if (newValueBool)
				{
					var listHeader = (View)list.EmptyListViewDataTemplate.CreateContent();
					listHeader.HorizontalOptions = LayoutOptions.CenterAndExpand;
					listHeader.VerticalOptions = LayoutOptions.CenterAndExpand;

					list.Footer = null;
					list.Header = new StackLayout
					{
						HeightRequest = list.Height,
						Children = { listHeader }
					};
				}
				else
				{
					list.Header = null;
					list.Footer = list._loadingActivityIndicator;
				}
			}
		}

		#endregion
	}
}

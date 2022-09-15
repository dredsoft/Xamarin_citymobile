using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using CityApp.Core.ViewModels.Abstractions;
using CityApp.Utilities.Logging;
using Xamarin.Forms;

namespace CityApp.Core.Pages
{
    public delegate void PageClosedEventHandler(object sender, EventArgs e);

    public class BasePage : ContentPage, IBasePage, ILogTarget
    {

		#region Events

		public event PageClosedEventHandler PageClosing;

		#endregion

		#region Constructors

		public BasePage()
	    {
		    ControlTemplate = (ControlTemplate) Application.Current.Resources["ContentPageTemplate"];
	    }

		#endregion

		#region Properties

		public ILogger Logger => LogManager.GetLog();

		#endregion

		#region Public Methods

	    public void SetBinding<TSource>(BindableProperty targetProperty,
		    string path, BindingMode mode = BindingMode.Default,
		    IValueConverter converter = null, string stringFormat = null)
	    {
		    Logger.Trace();
		    this.SetBinding(targetProperty, path, mode,
			    converter, stringFormat);
	    }

	    public void OnPageClosing()
	    {
		    Logger.Trace();
		    PageClosing?.Invoke(this, new EventArgs());
	    }

		#endregion

		#region Protected Methods

		protected override void OnBindingContextChanged()
		{
			Logger.Trace();
			base.OnBindingContextChanged();

			var viewModel = BindingContext as IViewModel;

			if (viewModel?.ToolbarItems == null)
			{
				return;
			}

			viewModel.ToolbarItems.CollectionChanged += ViewModel_ToolbarItems_CollectionChanged;

			foreach (var toolBarItem in viewModel.ToolbarItems)
			{
				if (ToolbarItems.All(x => x.Text != toolBarItem.Text))
				{
					ToolbarItems.Add(toolBarItem);
				}
			}
		}

		#endregion

		#region Private Methods

	    private void ViewModel_ToolbarItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
	    {
		    Logger.Trace();
		    ToolbarItems.Clear();

		    if (!(sender is ObservableCollection<ToolbarItem> vmToolbar))
		    {
			    return;
		    }

		    foreach (var item in vmToolbar)
		    {
			    if (ToolbarItems.All(x => x.Text != item.Text))
			    {
				    ToolbarItems.Add(item);
			    }
		    }
	    }

		#endregion
	}
}

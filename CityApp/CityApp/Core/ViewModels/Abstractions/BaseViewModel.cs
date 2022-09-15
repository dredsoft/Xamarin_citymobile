using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CityApp.Core.ViewModels.Implementations;
using CityApp.Infrastructure.NavigationManager;
using CityApp.Models.Models.Base;
using CityApp.Models.Models.Base.JsonOperations;
using CityApp.Resources;
using CityApp.Utilities.Logging;
using CityApp.Utilities.Validation.Abstractions;
using CityApp.Utilities.UserDialogs;
using CityApp.Utilities.UserDialogs.Components.Alert;
using Xamarin.Forms;

namespace CityApp.Core.ViewModels.Abstractions
{
	public abstract class BaseViewModel : NotifyPropertyChangedImplementation, IViewModel
	{
		#region Fields

		private object _initArgs;

		private bool _started;

		private ITabbedViewModel _parentViewModel;

		private bool _isVisible;

		private bool _isBusy;

		private string _title;

		private string _icon;

		protected readonly INavigationManager NavigationManager;

		#endregion

		#region Constructors

		protected BaseViewModel(INavigationManager navigationManager)
		{
			NavigationManager = navigationManager;

			ToolbarItems = new ObservableCollection<ToolbarItem>();

			Title = string.Empty;
		}

		#endregion

		#region Properties

		public ObservableCollection<ToolbarItem> ToolbarItems { get; set; }

		public abstract ILogger Logger { get; }

		public bool IsVisible
		{
			get => _isVisible;
			set => SetProperty(ref _isVisible, value);
		}

		public ITabbedViewModel ParentViewModel
		{
			get => _parentViewModel;
			set => SetProperty(ref _parentViewModel, value);
		}

		public bool IsBusy
		{
			get => _isBusy;
			set => SetProperty(ref _isBusy, value);
		}

		public string Title
		{
			get => _title;
			set => SetProperty(ref _title, value);
		}

		public string Icon
		{
			get => _icon;
			set => SetProperty(ref _icon, value);
		}

		public virtual Type DrawerMenuViewModelType => null;

		public virtual Type PageType => null;

		#endregion

		#region Public Methods

		public virtual async Task Init(object args)
		{
			await Task.Run(() =>
			{
				Logger.Trace();
				_initArgs = args;
			});
		}

		public virtual void OnStart()
		{
			Logger.Trace();
			_started = true;
		}

		public virtual void OnAppearing()
		{
			Logger.Trace();
			IsVisible = true;

			if (!_started)
			{
				_started = true;
				OnStart();
			}
		}

		public virtual void OnDisappearing()
		{
			Logger.Trace();
			IsVisible = false;
		}

		public virtual void PageClosing()
		{
			Logger.Trace();
		}

		#endregion

		#region Protected Methods

		protected bool ValidateResponse(IJsonOperationResult response)
		{
			var validationResult = response.IsSuccessfull;

			if (!response.IsSuccessfull)
			{
				NotifyUser(response.Message, response.Errors);
			}

			return validationResult;
		}

		protected void NotifyUser(string message, HttpResponseErrorModel [] errors)
		{
			UserDialogs.Instance.Alert.Show(new AlertConfig
			{
				Title = AppResources.txtMessage,
				Message = !string.IsNullOrEmpty(message) ? message : errors.FirstOrDefault().ToString(),
				OkText = AppResources.txtOK,
			});		
		}

		protected bool CheckIfDataValid<T>(ValidatableObject<T> validationObject)
		{
			validationObject.Validate();

			if (!validationObject.IsValid)
			{
				NotifyUser(validationObject.Errors.First(), null);

				return false;
			}

			return true;
		}

		#endregion
	}
}

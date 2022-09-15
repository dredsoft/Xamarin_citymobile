using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CityApp.Core.ViewModels.Implementations;
using CityApp.Infrastructure.NavigationManager;
using CityApp.Infrastructure.Storages;
using CityApp.Infrastructure.Storages.Constants;
using CityApp.Models.Models.Violation;
using CityApp.Modules.Violations.ViolationDetails;
using CityApp.Services.Violation;
using CityApp.Utilities.ActivityContext;
using CityApp.Utilities.Logging;
using Xamarin.Forms;

namespace CityApp.Modules.Violations.Violations
{
    public class ViolationsListViewModel : ListViewModel<ViolationClientModel>
    {
		#region Fields

	    private bool _isSearching;

	    private string _searchText;

	    private readonly IViolationService _violationService;

		private readonly ViolationCategoryClientModel _currentCategory;
	    private readonly ViolationTypeClientModel _currentTypeClientModel;

		#endregion

		#region Constructors

		public ViolationsListViewModel(INavigationManager navigationManager, IViolationService violationService) : base(navigationManager)
		{
			_violationService = violationService;

			_currentCategory = SessionStorage.Instance.Get<ViolationCategoryClientModel>(StorageConstants.CURRENT_VIOLATION_CATEGORY_KEY);

			_currentTypeClientModel = SessionStorage.Instance.Get<ViolationTypeClientModel>(StorageConstants.CURRENT_VIOLATION_TYPE_KEY);
		}

		#endregion

		#region Properties

	    public override ILogger Logger => LogManager.GetLog();

	    public bool IsSearching
	    {
		    get => _isSearching;
		    set => SetProperty(ref _isSearching, value);
	    }

	    public string SearchText
	    {
		    get => _searchText;
		    set => SetProperty(ref _searchText, value);
		}

		#endregion

		#region Commands

	    public ICommand FilterCommand => new Command(FilterItems);

		#endregion

	    #region Public Methods

	    public override void OnAppearing()
	    {
		    base.OnAppearing();

		    Title = _currentCategory.Name;
	    }

	    #endregion

		#region Protected Methods

		protected override void LoadItemsExecute()
	    {
		    using (ActivityContext.MakeContext(this))
		    {
			    var result = _violationService.GetViolationsAsync(_currentTypeClientModel.Name, _currentCategory.Name).ToList();

			    SetItemsListData(result, result.Count);
		    }
	    }

	    #endregion

		#region Private Methods

		private async void FilterItems()
	    {
		    Logger.Trace();

		    IsRefreshing = true;

			await Task.Run(() =>
		    {
				IList<ViolationClientModel> result;

			    if (!string.IsNullOrEmpty(SearchText))
			    {
				    result = _violationService.GetViolationsAsync(_currentTypeClientModel.Name, _currentCategory.Name)
					    .Where(category => category.Name.IndexOf(SearchText, StringComparison.CurrentCultureIgnoreCase) > -1).ToList();
			    }
			    else
			    {
				    result = _violationService.GetViolationsAsync(_currentTypeClientModel.Name, _currentCategory.Name).ToList();
			    }

				SetItemsListData(result, result.Count);			
			});

		    IsRefreshing = false;
		}

	    protected override async void SelectItemExecute(ViolationClientModel violation)
	    {
		    using (ActivityContext.MakeContext(this))
		    {
			    Logger.Trace();

				SessionStorage.Instance.Set(StorageConstants.VIOLATION_ID_KEY, violation.Id);

			    await NavigationManager.NavigateToAsync<ViolationDetailsViewModel>();
		    }
	    }

		#endregion
    }
}

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
using CityApp.Modules.Violations.Violations;
using CityApp.Services.Violation;
using CityApp.Utilities.ActivityContext;
using CityApp.Utilities.Logging;
using Xamarin.Forms;

namespace CityApp.Modules.Violations.Categories
{
	public class CategoriesListViewModel : ListViewModel<ViolationCategoryClientModel>
    {
		#region Fields

	    private string _searchText;
	    private readonly IViolationService _violationService;

		private readonly ViolationTypeClientModel _currentTypeClientModel;

	    #endregion

		#region Constructor

		public CategoriesListViewModel(INavigationManager navigationManager, IViolationService violationService) : base(navigationManager)
		{
			_violationService = violationService;

		    _currentTypeClientModel = SessionStorage.Instance.Get<ViolationTypeClientModel>(StorageConstants.CURRENT_VIOLATION_TYPE_KEY);

			Title = _currentTypeClientModel.Name;
		}

		#endregion

	    #region Properties

	    public override ILogger Logger => LogManager.GetLog();

	    public string SearchText
	    {
		    get => _searchText;
		    set => SetProperty(ref _searchText, value);
	    }

	    #endregion

		#region Commands

	    public ICommand FilterCommand => new Command(FilterItems);

		#endregion

		#region Protected Methods

		protected override void LoadItemsExecute()
		{
			using (ActivityContext.MakeContext(this))
			{
				var result = _violationService.GetCategories(_currentTypeClientModel.Name).ToList();

				SetItemsListData(result, result.Count);
			}
		}

	    protected override async void SelectItemExecute(ViolationCategoryClientModel category)
	    {
		    Logger.Trace();

		    using (ActivityContext.MakeContext(this))
		    {
			    SessionStorage.Instance.Set(StorageConstants.CURRENT_VIOLATION_CATEGORY_KEY, category);

			    var violations = _violationService.GetViolationsAsync(_currentTypeClientModel.Name, category.Name).ToList();

			    if (violations.Count > 1)
			    {
				    await NavigationManager.NavigateToAsync<ViolationsListViewModel>();
			    }
			    else
			    {
				    SessionStorage.Instance.Set(StorageConstants.VIOLATION_ID_KEY, violations.First().Id);

				    await NavigationManager.NavigateToAsync<ViolationDetailsViewModel>();
			    }
		    }
	    }

		#endregion

		#region Private Methods

		//need change
		private async void FilterItems()
		{
			Logger.Trace();

			IsRefreshing = true;

			await Task.Run(() =>
			{
				IList<ViolationCategoryClientModel> result;

				if (!string.IsNullOrEmpty(SearchText))
				{
					result = _violationService.GetCategories(_currentTypeClientModel.Name)
						.Where(category => category.Name.IndexOf(SearchText, StringComparison.CurrentCultureIgnoreCase) > -1).ToList();
				}
				else
				{
					result = _violationService.GetCategories(_currentTypeClientModel.Name).ToList();
				}

				SetItemsListData(result, result.Count);				
			});

			IsRefreshing = false;
		}

		#endregion
	}
}

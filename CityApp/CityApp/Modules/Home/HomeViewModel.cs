using CityApp.Infrastructure.NavigationManager;
using CityApp.Infrastructure.Storages;
using CityApp.Infrastructure.Storages.Constants;
using CityApp.Models.Models.Account;
using CityApp.Models.Models.Violation;
using CityApp.Modules.Menu;
using CityApp.Modules.Violations.Categories;
using CityApp.Modules.Violations.ViolationDetails;
using CityApp.Modules.Violations.Violations;
using CityApp.Resources;
using CityApp.Services.Citation;
using CityApp.Services.Violation;
using CityApp.Utilities.ActivityContext;
using CityApp.Utilities.Logging;
using CityApp.Modules.MediaPlayer;
using System.Collections.Generic;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using CityApp.Core.ViewModels.Abstractions;
using CityApp.Models.Enums;
using CityApp.Models.Models.Citation;
using CityApp.Modules.Home.SubmissionHistory;
using CityApp.Modules.Printing;

namespace CityApp.Modules.Home
{
    public class HomeViewModel : BaseViewModel
    {
        #region Fields

        private readonly IViolationService _violationService;
        private readonly ICitationsService _citationsService;
        private readonly AccountAssociationModel _accountAssociation;

        private IEnumerable<ViolationTypeClientModel> _violationTypes;
        private IEnumerable<CitationModel> _submissionList;

        private string _submissionsTitle;
        private bool _showSubmissionsPlaceholder;
        private bool _isNotEmptySubmissionList;
        private long _totalCount;


        #endregion

        #region Constructors

        public HomeViewModel(INavigationManager navigationManager, IViolationService violationService, ICitationsService citationsService) : base(navigationManager)
        {
            _violationService = violationService;

            _citationsService = citationsService;

            _accountAssociation = SessionStorage.Instance.Get<AccountAssociationModel>(StorageConstants.ACCOUNT_ASSOCIATION_KEY);

            Title = AppResources.txtHome;
        }

        #endregion

        #region Properties

        public override ILogger Logger => LogManager.GetLog();

        public IEnumerable<ViolationTypeClientModel> ViolationTypes
        {
            get => _violationTypes;
            set => SetProperty(ref _violationTypes, value);
        }

        public IEnumerable<CitationModel> SubmisisonList
        {
            get => _submissionList;
            set => SetProperty(ref _submissionList, value);
        }

        public string SubmissionsTitle
        {
            get => _submissionsTitle;
            set => SetProperty(ref _submissionsTitle, value);
        }

        public bool IsNotEmptySubmissionList
        {
            get => _isNotEmptySubmissionList;
            set => SetProperty(ref _isNotEmptySubmissionList, value);
        }

        public bool ShowSubmissionsPlaceholder
        {
            get => _showSubmissionsPlaceholder;
            set => SetProperty(ref _showSubmissionsPlaceholder, value);
        }

        public override Type DrawerMenuViewModelType => typeof(MenuViewModel);

        public long TotalCount
        {
            get => _totalCount;
            set => SetProperty(ref _totalCount, value);
        }

        #endregion

        #region Commands

        public ICommand ShowAllSubmissionsCommand => new Command(ShowAllSubmissions);

        public ICommand SubmitViolationCommand => new Command(SubmitViolation);

        public ICommand SelectSubmisisonCommand => new Command<CitationModel>(SelectSubmisisonExecute);

        public ICommand GoToCategoriesCommand => new Command<ViolationTypeClientModel>(GoToCategoriesExecute);

        #endregion

        #region Public Methods

        public override async void OnAppearing()
        {
            Logger.Trace();

            await LoadViolationTypes();

            await LoadSubmissionItems();

            base.OnAppearing();
        }

        public override async Task Init(object args)
        {
            await base.Init(args);

            if (SessionStorage.Instance.UserContext.Permission == SystemPermissions.Administrator || SessionStorage.Instance.UserContext.Permission == SystemPermissions.ParkingOfficer)
                await NavigationManager.NavigateToAsync<SelectPrinterViewModel>();
        }

        #endregion

        #region Private Methods

        private async Task LoadViolationTypes()
        {
            using (ActivityContext.MakeContext(this))
            {
                var violaions = await _violationService.GetViolationsAsync(_accountAssociation.AccountNumber);

                if (ValidateResponse(violaions))
                {
                    //need changes
                    ViolationService.Init(violaions.Data);

                    ViolationTypes = _violationService.GetTypes();
                };
            }
        }

        private async Task LoadSubmissionItems()
        {
            using (ActivityContext.MakeContext(this))
            {
                var citations = await _citationsService.GetCitationsAsync(_accountAssociation.AccountNumber, CommonConstants.SUBMISSIONS_PER_PAGE, 1);

                ; if (ValidateResponse(citations))
                {

                    if (citations != null)
                    {
                        TotalCount = citations.Data.TotalItems;

                        SubmisisonList = citations.Data.CitationModel;

                        IsNotEmptySubmissionList = SubmisisonList.Any();
                    }

                    if (IsNotEmptySubmissionList)
                    {
                        SubmissionsTitle = AppResources.txtSubmisionHistory;
                        ShowSubmissionsPlaceholder = false;
                    }
                    else
                    {
                        SubmissionsTitle = AppResources.txtNoSubmisionYet;
                        ShowSubmissionsPlaceholder = true;
                    }
                }
            }
        }

        private async void GoToCategoriesExecute(ViolationTypeClientModel violationTypeClientModel)
        {
            Logger.Trace();

            using (ActivityContext.MakeContext(this))
            {
                SessionStorage.Instance.Set(StorageConstants.CURRENT_VIOLATION_TYPE_KEY, violationTypeClientModel);

                var categories = _violationService.GetCategories(violationTypeClientModel.Name).ToList();
                if (categories.Count() > 1)
                {
                    await NavigationManager.NavigateToAsync<CategoriesListViewModel>();
                }
                else
                {
                    var currentCategory = categories.FirstOrDefault();

                    SessionStorage.Instance.Set(StorageConstants.CURRENT_VIOLATION_CATEGORY_KEY, currentCategory);

                    var violations = _violationService.GetViolationsAsync(violationTypeClientModel.Name, currentCategory.Name).ToList();

                    if (violations.Count() > 1)
                    {
                        await NavigationManager.NavigateToAsync<ViolationsListViewModel>();
                        return;
                    }

                    SessionStorage.Instance.Set(StorageConstants.VIOLATION_ID_KEY, violations.First().Id);
                    await NavigationManager.NavigateToAsync<ViolationDetailsViewModel>();
                }
            }
        }

        private async void SelectSubmisisonExecute(CitationModel model)
        {
            var videoKey = model.CitationAttachment.FirstOrDefault(x => x.AttachmentType == CitationAttachmentType.Video).Key;

            var videoSource = _citationsService.ReadAttachmentFileFromAmazon(videoKey);

            if (!string.IsNullOrWhiteSpace(videoSource))
            {
                SessionStorage.Instance.Set(StorageConstants.VIDEO_SOURCE_KEY, videoSource);

                await NavigationManager.NavigateToAsync<MediaPlayerViewModel>();
            }
        }

        private void SubmitViolation()
        {
            Logger.Trace();
        }

        private async void ShowAllSubmissions()
        {
            await NavigationManager.NavigateToAsync<SubmissionHistoryListViewModel>();
        }

        #endregion
    }
}

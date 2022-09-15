using System.Linq;
using CityApp.Core.ViewModels.Implementations;
using CityApp.Infrastructure.NavigationManager;
using CityApp.Infrastructure.Storages;
using CityApp.Infrastructure.Storages.Constants;
using CityApp.Models.Enums;
using CityApp.Models.Models.Account;
using CityApp.Models.Models.Citation;
using CityApp.Modules.MediaPlayer;
using CityApp.Resources;
using CityApp.Services.Citation;
using CityApp.Utilities.ActivityContext;
using CityApp.Utilities.Logging;

namespace CityApp.Modules.Home.SubmissionHistory
{
   public class SubmissionHistoryListViewModel : ListViewModel<CitationModel>
   {
	   #region Private Methods

	   private readonly ICitationsService _citationsService;

	   private readonly AccountAssociationModel _accountAssociation;

		#endregion

		#region Constructor

		public SubmissionHistoryListViewModel(INavigationManager navigationManager, ICitationsService citationsService) : base(navigationManager)
		{
			_citationsService = citationsService;

			_accountAssociation = SessionStorage.Instance.Get<AccountAssociationModel>(StorageConstants.ACCOUNT_ASSOCIATION_KEY);

			Title = AppResources.txtSubmisionHistory;
		}

		#endregion

		#region Properties

	    public override ILogger Logger => LogManager.GetLog();

		#endregion

		#region Protected Methods

	   public override void OnAppearing()
	   {
		   using (ActivityContext.MakeContext(this))
		   {
			   PageIndex = 1;
			   LoadItemsExecute();
			}
	   }

	   protected override async void LoadItemsExecute()
		{
			var attachments = await _citationsService.GetCitationsAsync(_accountAssociation.AccountNumber, PageSize, PageIndex);

			if (ValidateResponse(attachments))
			{
				var citation = attachments.Data.CitationModel.ToList();
				SetItemsListData(citation, attachments.Data.TotalItems);
			}
		}

	   protected override async void SelectItemExecute(CitationModel obj)
	   {
		   var videoKey = obj.CitationAttachment.FirstOrDefault(x => x.AttachmentType == CitationAttachmentType.Video).Key;

		   var videoSource = _citationsService.ReadAttachmentFileFromAmazon(videoKey);

		   if (!string.IsNullOrWhiteSpace(videoSource))
		   {
			   SessionStorage.Instance.Set(StorageConstants.VIDEO_SOURCE_KEY, videoSource);

			   await NavigationManager.NavigateToAsync<MediaPlayerViewModel>();
		   }
		}

		#endregion
	}
}

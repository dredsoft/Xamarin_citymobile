using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using CityApp.Core.ViewModels.Abstractions;
using CityApp.Infrastructure.NavigationManager;
using CityApp.Infrastructure.Storages;
using CityApp.Infrastructure.Storages.Constants;
using CityApp.Models.Enums;
using CityApp.Models.Extensions;
using CityApp.Models.Models.Account;
using CityApp.Models.Models.Base.JsonOperations;
using CityApp.Models.Models.Citation;
using CityApp.Models.Models.Printing;
using CityApp.Models.Models.Violation;
using CityApp.Modules.Home;
using CityApp.Modules.MediaPlayer;
using CityApp.Modules.Printing;
using CityApp.Resources;
using CityApp.Services;
using CityApp.Services.Violation;
using CityApp.Services.ZebraPrinting.Printer;
using CityApp.Utilities.ActivityContext;
using CityApp.Utilities.Logging;
using CityApp.Utilities.UserDialogs;
using CityApp.Utilities.UserDialogs.Components.Alert;
using CityApp.Utilities.UserDialogs.Components.Toast;
using CityApp.Utilities.Validation.Abstractions;
using CityApp.Utilities.Validation.Rules;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Position = Plugin.Geolocator.Abstractions.Position;

namespace CityApp.Modules.Violations.ViolationCreateEdit
{
    public class VirtualViolationCreateEditViewModel : BaseViewModel
    {
        #region Private Fields

        private readonly IViolationService _violationService;
        private readonly IVideoService _videoService;
        private readonly IPrinterService _printerService;
        private readonly ViolationModel _violation;
        private readonly AccountAssociationModel _currentAccount;
        private readonly MediaFile _video;
        private string _videoPath;
        private readonly Guid _videoId;
        private readonly byte[] _thumbnail;

        private int _selectedStateIndex;
        private string _selectedState;
        private int _selectedMakeIndex;
        private string _selectedMake;
        private CitationStatus _status;
        private string _vehicleMake;
        private string _vehicleModel;
        private string _vehicleColor;
        private string _vehicleType;
        private string _description;
        private string _locationDescription;
        private bool _isPrintingEnabled;

        private long _citationNumber;

        private ImageSource _videoThumbnailSource;

        private ViolationRequiredFields _displayRequiredFields;

        #endregion

        #region Constructors

        public VirtualViolationCreateEditViewModel(INavigationManager navigationManager, IViolationService violationService, IVideoService videoService, IPrinterService printerService) : base(navigationManager)
        {
            _printerService = printerService;
            _violationService = violationService;
            _videoService = videoService;
            _currentAccount = SessionStorage.Instance.Get<AccountAssociationModel>(StorageConstants.ACCOUNT_ASSOCIATION_KEY);
            _violation = SessionStorage.Instance.Get<ViolationModel>(StorageConstants.VIOLATION_ITEM_KEY);
            _video = SessionStorage.Instance.Get<MediaFile>(StorageConstants.VIOLATION_VIDEO_FILE_KEY);
            _videoId = SessionStorage.Instance.Get<Guid>(StorageConstants.VIDEO_FILE_ID_KEY);
            _thumbnail = videoService.GetVideoThumbnailFromFile(_video.Path);
        }

        #endregion

        #region Properties

        public override ILogger Logger => LogManager.GetLog();

        public CitationStatus Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public Position ViolationPosition => SessionStorage.Instance.Get<Position>(StorageConstants.POSITION_ITEM_KEY);

        public string LocationDescription
        {
            get => _locationDescription;
            set => SetProperty(ref _locationDescription, value);
        }

        public string VehicleMake
        {
            get => _vehicleMake;
            set => SetProperty(ref _vehicleMake, value);
        }

        public string VehicleModel
        {
            get => _vehicleModel;
            set => SetProperty(ref _vehicleModel, value);
        }

        public string VehicleColor
        {
            get => _vehicleColor;
            set => SetProperty(ref _vehicleColor, value);
        }

        public string VehicleType
        {
            get => _vehicleType;
            set => SetProperty(ref _vehicleType, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public List<string> LicenseStates => States.USA.Values.ToList();
        public List<string> VehicleMakes => Vehicles.Makes.Values.ToList();

        public string SelectedState
        {
            get => string.IsNullOrEmpty(_selectedState) ? LicenseStates[SelectedStateIndex] : _selectedState;
            set => SetProperty(ref _selectedState, value);
        }

        public string SelectedVehicleMake
        {
            get => string.IsNullOrEmpty(_selectedMake) ? VehicleMakes[SelectedMakeIndex] : _selectedMake;
            set => SetProperty(ref _selectedMake, value);
        }

        public int SelectedStateIndex
        {
            get => _selectedStateIndex;
            set
            {
                if (_selectedStateIndex != value)
                {
                    _selectedStateIndex = value;

                    SetProperty(ref _selectedStateIndex, value);
                }
            }
        }

        public int SelectedMakeIndex
        {
            get => _selectedMakeIndex;
            set
            {
                if (_selectedMakeIndex != value)
                {
                    _selectedMakeIndex = value;

                    SetProperty(ref _selectedMakeIndex, value);
                }
            }
        }

        public ValidatableObject<string> LicensePlate { get; set; }

        public ViolationRequiredFields DisplayRequiredFields
        {
            get => _displayRequiredFields;
            set => SetProperty(ref _displayRequiredFields, value);
        }

        public ImageSource VideoThumbnailSource
        {
            get => _videoThumbnailSource;
            set => SetProperty(ref _videoThumbnailSource, value);
        }

        #endregion

        #region Commands

        public ICommand SendViolationCommand => new Command(SendViolation);

        public ICommand WatchVideoCommand => new Command(WatchVideoExecute);
        #endregion

        #region Public Methods

        public override async Task Init(object args)
        {
            Logger.Trace();

            await base.Init(args).ConfigureAwait(false);

            LicensePlate = new ValidatableObject<string>();

            _isPrintingEnabled = _violation.CategoryName == "Parking Enforcement" ? true : false;

            Title = AppResources.txtViolationForm;

            DisplayRequiredFields = _violation.DisplayRequiredFields;

            AddValidations();

            VideoThumbnailSource = ImageSource.FromStream(() => new MemoryStream(_thumbnail));

            if (_isPrintingEnabled)
                if (SessionStorage.Instance.UserContext.Permission == SystemPermissions.Administrator || SessionStorage.Instance.UserContext.Permission == SystemPermissions.ParkingOfficer)
                {
                    if (SessionStorage.Instance.DiscoveredPrinterContext == null)
                        await GoToSelectPrinter();
                }
        }

        public override async void OnAppearing()
        {
            LocationDescription = await GetLocationDescription();
        }

        #endregion

        #region Private Methods

        private async Task GoToSelectPrinter()
        {
            await NavigationManager.NavigateToAsync<SelectPrinterViewModel>(CreatePrintModel());
        }

        private async void WatchVideoExecute()
        {
            Logger.Trace();

            if (!string.IsNullOrWhiteSpace(_video.Path))
            {
                var path = Device.RuntimePlatform == Device.iOS ?
                    $"{CommonConstants.LOCAL_IOS_FILE_EXTENSION}{_video.Path}" :
                        _video.Path;

                SessionStorage.Instance.Set(StorageConstants.VIDEO_SOURCE_KEY, path);

                await NavigationManager.NavigateToAsync<MediaPlayerViewModel>();
            }
        }

        private async void SendViolation()
        {
            await SendViolationExecute();

            if (_isPrintingEnabled)
                if (SessionStorage.Instance.UserContext.Permission == SystemPermissions.Administrator || SessionStorage.Instance.UserContext.Permission == SystemPermissions.ParkingOfficer)
                {
                    if (SessionStorage.Instance.DiscoveredPrinterContext != null)
                        await PrintAsync();
                    else
                    {
                        await GoToSelectPrinter();
                    }
                }

            ClearSessionValues();

            NavigationManager.SetMainViewModel<HomeViewModel>();
        }

        private async Task PrintAsync()
        {
            try
            {
                await _printerService.PrintAsync(SessionStorage.Instance.DiscoveredPrinterContext, BuildPrinterMessage(CreatePrintModel()));
                NotifyUser("Ticket has been printed", null);
            }
            catch (Exception e)
            {
                NotifyUser(e.Message, null);
            }
        }

        private string BuildPrinterMessage(PrintModel printModel)
        {
            return "^XA" +
                    "^MUD,200,200" +
                    "^PW603" +
                    "^LL1422" +
                    "^LH0,0" +
                    "^JMA^FS" +
                    "^CVN" +
                    "^FO37,138^GB518,1128,3,B,0^FS" +
                    "^FO40,220^GB512,0,3^FS" +
                    "^FO40,320^GB512,0,3^FS" +
                    "^FO40,420^GB512,0,3^FS" +
                    "^FO40,520^GB512,0,3^FS" +
                    "^FO40,620^GB512,0,3^FS" +
                    "^FO40,720^GB512,0,3^FS" +
                    "^FO40,870^GB512,0,3^FS" +
                    "^FO40,970^GB512,0,3^FS" +
                    "^FO40,1070^GB512,0,3^FS" +
                    "^FO40,1170^GB512,0,3^FS" +
                    "^FO310,220^GB0,100,3^FS" +
                    "^FO180,420^GB0,100,3^FS" +
                    "^FO310,420^GB0,100,3^FS" +
                    "^FO310,520^GB0,100,3^FS" +
                    "^FO310,620^GB0,100,3^FS" +
                    "^FO310,870^GB0,100,3^FS" +
                   $"^FO35,28^A0N,56,60^FD{printModel.City}^FS" +
                    "^FO35,77^A0N,60,63^FDPARKING VIOLATION^FS" +
                    "^FO50,155^A0N,40,45^FDCITATION#^FS" +
                   $"^FO260,175^A0N,30,30^FD{printModel.CitationNumber}^FS" +
                    "^FO50,235^A0N,40,45^FDOffence Date^FS" +
                    $"^FO50,280^A0N,30,30^FD{printModel.CreateUtc.Date.ToString("MM/dd/yyyy")}^FS" +
                    "^FO325,235^A0N,40,45^FDOffence Time^FS" +
                    $"^FO325,280^A0N,30,30^FD{printModel.CreateUtc.Hour.ToString("hh:mm tt")}^FS" +
                    "^FO50,335^A0N,40,45^FDLicense Number/VIN^FS" +
                    $"^FO50,380^A0N,30,30^FD{printModel.LicensePlate}^FS" +
                    "^FO50,435^A0N,40,45^FDState^FS" +
                    $"^FO50,480^A0N,30,30^FD{printModel.State}^FS" +
                    "^FO195,435^A0N,40,45^FDMonth^FS" +
                    $"^FO195,480^A0N,30,30^FD{printModel.CreateUtc.Month}^FS" +
                    "^FO325,435^A0N,40,45^FDYear^FS" +
                    $"^FO325,480^A0N,30,30^FD{printModel.CreateUtc.Year}^FS" +
                    "^FO50,535^A0N,40,45^FDMake^FS" +
                    $"^FO50,580^A0N,30,30^FD{printModel.VehicleMake}^FS" +
                    "^FO325,535^A0N,40,45^FDModel^FS" +
                    $"^FO325,580^A0N,30,30^FD{printModel.VehicleModel}FS" +
                    "^FO50,635^A0N,40,45^FDColor^FS" +
                    $"^FO50,680^A0N,30,30^FD{printModel.VehicleColor}^FS" +
                    "^FO325,635^A0N,40,45^FDType^FS" +
                    $"^FO325,680^A0N,30,30^FD{printModel.VehicleType}^FS" +
                    "^FO50,735^A0N,40,45^FDLocation^FS" +
                    $"^FO50,780^A0N,30,30^FD{printModel.Location}^FS" +
                    "^FO50,885^A0N,40,45^FDOfficer^FS" +
                    $"^FO50,930^A0N,30,30^FD{printModel.OfficerName}^FS" +
                    "^FO325,885^A0N,40,45^FDNumber^FS" +
                    $"^FO325,930^A0N,30,30^FD{printModel.OfficerBadgeNumber}^FS" +
                    "^FO50, 985^A0N,40,45^FDViolation Code^FS" +
                    $"^FO50,1030^A0N,30,30^FD{printModel.ViolationCode}^FS" +
                    "^FO50,1085^A0N,40,45^FDViolation^FS" +
                    $"^FO50,1130^A0N,30,30^FD{printModel.ViolationName}^FS" +
                    "^FO50,1205^A0N,45,40^FDAMOUNT DUE:^FS" +
                    $"^FO300,1206^A0N,40,40^FD{printModel.Fee}^FS" +
                    "^FO50,1282^A0N,70,70^FDF^FS" +
                    "^FO85,1293^A0N,50,50^FDine must be paid within^FS" +
                    "^FO50,1380^A0N,40,40^FDPayment instruction on the back^FS" +
                    "^XZ";
        }

        private async Task SendViolationExecute()
        {
            Logger.Trace();

            if (CheckIfDataValid(LicensePlate))
            {
                using (ActivityContext.MakeContext(this))
                {
                    var citation = CreateCitation();

                    Progress = 0.28;

                    var result = await _violationService.SendViolationAsync(_currentAccount.AccountNumber, citation);

                    if (ValidateResponse(result))
                    {
                        var citationId = result.Data.Id;

                        var citationNumber = result.Data.CitationNumber;
                        //
                        _citationNumber = citationNumber;

                        var thumbnailKey = $"accounts/{_currentAccount.AccountNumber}/citations/{citationNumber}/{_videoId}{CommonConstants.THUMBNAIL_EXTENSION}";
                        var videoKey = $"accounts/{_currentAccount.AccountNumber}/citations/{citationNumber}/{_videoId}{CommonConstants.VIDEO_EXTENSION}";

                        var attachments = CreateAttachments((Guid)citationId);

                        var videoAttachment = new CitationAttachment
                        {
                            FileName = $"{_videoId}{CommonConstants.VIDEO_EXTENSION}",
                            Key = videoKey,
                            MimeType = MimeTypes.Video.GetTextRepresentation(),
                            AttachmentType = CitationAttachmentType.Video

                        };

                        Progress = 0.68;

                        var thumbnailAttachment = new CitationAttachment
                        {
                            FileName = $"{_videoId}{CommonConstants.THUMBNAIL_EXTENSION}",
                            Key = thumbnailKey,
                            MimeType = MimeTypes.Image.GetTextRepresentation(),
                            AttachmentType = CitationAttachmentType.Image
                        };

                        attachments.Attachments.Add(videoAttachment);
                        attachments.Attachments.Add(thumbnailAttachment);

                        var attachmentResult = await _violationService.SendAttachmentsAsync(_currentAccount.AccountNumber, (Guid)citationId, attachments);

                        if (ValidateResponse(attachmentResult))
                        {
                            if (Device.RuntimePlatform == Device.Android)
                            {
                                var serviceManager = App.Container.Resolve<IGovAppServiceManager>();

                                serviceManager.UploadVideo(_video.Path, videoKey, thumbnailKey);

                                UserDialogs.Instance.ToastMessage.Show(new ToastConfig
                                {
                                    BackgroundColor = Color.FromHex($"#1277B0"),
                                    ToastMessage = AppResources.msgViolationsubmitted,
                                    MessageTextColor = Color.White,
                                    Duration = TimeSpan.FromSeconds(3),
                                });

                            }
                            else
                            {
                                await _violationService.SendViolationFilesToAmazonAsync(
                                    thumbnailKey,
                                    videoKey,
                                    _video.GetStream(),
                                    new MemoryStream(_thumbnail));

                                UserDialogs.Instance.Alert.Show(new AlertConfig
                                {
                                    Title = AppResources.txtMessage,
                                    Message = AppResources.msgViolationsubmitted,
                                    OkText = AppResources.txtOK,
                                });
                            }

                            Progress = 1;
                        }
                    }
                }
            }
        }

        private CitationRequestModel CreateCitation() => new CitationRequestModel
        {
            ViolationId = _violation.Id,
            Status = CitationStatus.Approved,
            Latitude = (decimal)ViolationPosition.Latitude,
            Longitude = (decimal)ViolationPosition.Longitude,
            LicensePlate = LicensePlate.Value,
            LicenseState = SelectedState,
            Description = Description,
            VehicleColor = VehicleColor,
            VehicleMake = SelectedVehicleMake,
            VehicleModel = VehicleModel,
            VehicleType = VehicleType,
            LocationDescription = LocationDescription
        };

        private PrintModel CreatePrintModel() => new PrintModel
        {
            CitationNumber = _citationNumber,
            OfficerName = SessionStorage.Instance.UserContext.FullName,
            State = SelectedState,
            CreateUtc = DateTime.UtcNow,
            ViolationCode = _violation.Code != null ? _violation.Code.ToString() : "No Code",
            ViolationName = _violation.Name,
            LicensePlate = LicensePlate.Value,
            VehicleColor = VehicleColor,
            VehicleMake = SelectedVehicleMake,
            VehicleModel = VehicleModel,
            VehicleType = VehicleType,
            Location = LocationDescription,
            Fee = _violation.Fee != null ? _violation.Fee.ToString() : "No Fee",
            City = _currentAccount.Name,

        };

        private AttachmentsModel CreateAttachments(Guid citationId) => new AttachmentsModel
        {
            CitationId = citationId,
            DevicePublicKey = SessionStorage.Instance.DeviceContext.DevicePublicKey,
            Attachments = new List<CitationAttachment>()
        };

        private void AddValidations()
        {
            if (_violation.DisplayRequiredFields.HasFlag(ViolationRequiredFields.VehicleInformation))
            {
                LicensePlate.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.msgLicensePlateRequired });
            }
        }

        private void ClearSessionValues()
        {
            SessionStorage.Instance.TryRemove(StorageConstants.VIOLATION_ID_KEY);
            SessionStorage.Instance.TryRemove(StorageConstants.POSITION_ITEM_KEY);
            SessionStorage.Instance.TryRemove(StorageConstants.VIOLATION_ITEM_KEY);
            SessionStorage.Instance.TryRemove(StorageConstants.VIOLATION_VIDEO_FILE_KEY);
            SessionStorage.Instance.TryRemove(StorageConstants.VIDEO_FILE_ID_KEY);
            SessionStorage.Instance.TryRemove(StorageConstants.THUMBNAIL_ITEM_KEY);
        }

        private async Task<string> GetLocationDescription()
        {
            var geoCoder = new Geocoder();
            var address = await geoCoder.GetAddressesForPositionAsync(new Xamarin.Forms.GoogleMaps.Position(ViolationPosition.Latitude, ViolationPosition.Longitude));

            return address.FirstOrDefault();
        }

        #endregion

        #region Temp

        private double _progress;

        private double _progressPercent;

        public double Progress
        {
            get => _progress;
            set
            {
                if (SetProperty(ref _progress, value))
                {
                    ProgressPercent = _progress * 100;
                }
            }
        }

        public double ProgressPercent
        {
            get => _progressPercent;
            set => SetProperty(ref _progressPercent, value);
        }

        #endregion
    }
}

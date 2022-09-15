using System.Reflection;
using System.Security.Cryptography;
using System.Xml.Linq;
using Autofac;
using CityApp.Infrastructure.ApiManager;
using CityApp.Infrastructure.ConnectivityManager.Implementations;
using CityApp.Infrastructure.NavigationManager;
using CityApp.Infrastructure.PageLocators.Abstractions;
using CityApp.Infrastructure.PageLocators.Implementations;
using CityApp.Infrastructure.Storages;
using CityApp.Infrastructure.Storages.Constants;
using CityApp.Models.Models.Account;
using CityApp.Models.Models.Authorization;
using CityApp.Models.Models.Base;
using CityApp.Modules.Account.Accounts;
using CityApp.Modules.Account.Login;
using CityApp.Modules.Home;
using CityApp.Services;
using CityApp.Services.Account;
using CityApp.Services.AmazonService;
using CityApp.Services.Citation;
using CityApp.Services.Violation;
using CityApp.Services.ZebraPrinting.Printer;
using CityApp.Shared.Extensions;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CityApp
{
	public partial class App : Application
    {
        public static IContainer Container { get; private set; }

        public App()
        {
            InitializeComponent();

            SetupDependencyInjection();

			SetStartPage();
        }

        protected override void OnStart()
        {
            base.OnStart();
            SetupAppCenter();
            GenerateRsaPublicKey();
        }

        private void SetupDependencyInjection()
        {
            if (Container != null)
            {
                return;
            }

            var builder = new ContainerBuilder();

            builder.Register<IPageResolver>(context => new AutofacPageResolver(Container)).SingleInstance();

            builder.RegisterMvvmComponents(typeof(App).GetTypeInfo().Assembly);

            builder.RegisterType<NavigationManager>().AsImplementedInterfaces().SingleInstance();

	        builder.RegisterType<CrossConnectivityManagerImplementation>().AsImplementedInterfaces();

			builder.RegisterType<ApiManager>().AsImplementedInterfaces();

	        builder.RegisterType<AWSS3Service>().AsImplementedInterfaces();

			builder.RegisterType<AccountService>().AsImplementedInterfaces();

			builder.RegisterType<CitationsService>().AsImplementedInterfaces();

			builder.RegisterType<ViolationService>().AsImplementedInterfaces();

            builder.RegisterType<PrinterService>().AsImplementedInterfaces();

            builder.RegisterXamDependency<IVideoService>();

	        builder.RegisterXamDependency<IGovAppServiceManager>();

			Container = builder.Build();
        }

        private void SetStartPage()
        {
            var navigationService = Container.Resolve<INavigationManager>();


	        SetSessionStorage();

			if (SessionStorage.Instance.UserContext == null)
            {
                navigationService.SetMainViewModel<LoginViewModel>();
            }
            else if(SettingStorage.Instance.GetValue<AccountAssociationModel>(StorageConstants.ACCOUNT_ASSOCIATION_KEY) == null)
            {
                navigationService.SetMainViewModel<AccountsListViewModel>();
            }
            else
            {
                navigationService.SetMainViewModel<HomeViewModel>();
            }
        }

	    private void SetSessionStorage()
	    {
		    var userContext = SettingStorage.Instance.GetValue<AuthorizatioUserModel>(StorageConstants.USER_CONTEXT_KEY);
		    var deviceContext = SettingStorage.Instance.GetValue<DeviceContextModel>(StorageConstants.DEVICE_CONTEXT_KEY);
		    var accountAssociation = SettingStorage.Instance.GetValue<AccountAssociationModel>(StorageConstants.ACCOUNT_ASSOCIATION_KEY);

		    if (userContext != null)
		    {
			    SessionStorage.Instance.UserContext = userContext;
		    }


		    if (deviceContext == null)
		    {
				deviceContext = new DeviceContextModel();

			    SettingStorage.Instance.AddOrUpdateValue(StorageConstants.DEVICE_CONTEXT_KEY, deviceContext);
		    }

		    SessionStorage.Instance.DeviceContext = deviceContext;

			if (accountAssociation != null)
		    {
			    SessionStorage.Instance.Set(StorageConstants.ACCOUNT_ASSOCIATION_KEY, accountAssociation);
		    }
		}

        private void SetupAppCenter()
        {
            AppCenter.Start($"{CommonConstants.APPCENTER_DROID_KEY}{CommonConstants.APPCENTER_IOS_KEY}", typeof(Crashes));
        }

        private void GenerateRsaPublicKey()
        {
            using (var rsa = RSA.Create())
            {
                var publicKeyXmlString = rsa.ToXmlString(false);
                var xDoc = XDocument.Parse(publicKeyXmlString);
				var devicePublicKey = ((XElement)xDoc.FirstNode).Value;

	            SettingStorage.Instance.AddOrUpdateValue(StorageConstants.DEVICE_PUBLIC_KEY , devicePublicKey);
            }
        }
    }
}
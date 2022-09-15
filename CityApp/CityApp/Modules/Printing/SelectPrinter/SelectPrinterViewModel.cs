using CityApp.Core.ViewModels.Abstractions;
using CityApp.Infrastructure.NavigationManager;
using CityApp.Infrastructure.Storages;
using CityApp.Resources;
using CityApp.Services.ZebraPrinting.Printer;
using CityApp.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Zebra.Sdk.Printer.Discovery;

namespace CityApp.Modules.Printing
{
    public class SelectPrinterViewModel : BaseViewModel
    {
        #region Fields

        private readonly IPrinterService _printerService;

        private IEnumerable<DiscoveredPrinter> _discoveredPrinters;

        private DiscoveredPrinter _highlightedPrinter;

        private bool _isPrinterListRefreshing;
        private bool _isSelectingPrinter;

        #endregion

        #region Constructors

        public SelectPrinterViewModel(INavigationManager navigationManager, IPrinterService printerService) : base(navigationManager)
        {
            _printerService = printerService;

            Title = AppResources.txtSelectPrinter;
        }

        #endregion

        #region Properties

        public override ILogger Logger => LogManager.GetLog();

        public IEnumerable<DiscoveredPrinter> DiscoveredPrinters
        {
            get => _discoveredPrinters;
            set => SetProperty(ref _discoveredPrinters, value);
        }

        public DiscoveredPrinter HighlightedPrinter
        {
            get => _highlightedPrinter;
            set => SetProperty(ref _highlightedPrinter, value);
        }

        public bool IsPrinterListRefreshing
        {
            get => _isPrinterListRefreshing;
            set => SetProperty(ref _isPrinterListRefreshing, value);
        }

        public bool IsSelectingPrinter
        {
            get => _isSelectingPrinter;
            set => SetProperty(ref _isSelectingPrinter, value);
        }

        #endregion

        #region Commands

        public ICommand SelectPrinterCommand => new Command(async () =>
        {
            SessionStorage.Instance.DiscoveredPrinterContext = HighlightedPrinter;
            await NavigationManager.PopAsync();
        });

        public ICommand SkipSelectPrinterCommand => new Command(async () =>
        { 
            await NavigationManager.PopAsync();
        });

        public ICommand RefreshIconCommand => new Command(async () => await RefreshPrinterListAsync());

        #endregion

        #region Public Methods

        public override async Task Init(object args)
        {
            await base.Init(args);

            Device.BeginInvokeOnMainThread(async () =>
            {
                await DiscoverPrintersAsync();
            });
        }

        #endregion

        #region Private Methods

        private async Task RefreshPrinterListAsync()
        {
            if (!IsPrinterListRefreshing)
            {
                await DiscoverPrintersAsync();
            }
        }

        private async Task ClearDiscoveredPrinterListAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                HighlightedPrinter = null;
            });

            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    DiscoveredPrinters = null; // View model operations must be done on UI thread due to iOS issues when clearing list while item is selected: https://forums.xamarin.com/discussion/19114/invalid-number-of-rows-in-section
                }
                catch (NotImplementedException)
                {
                    DiscoveredPrinters = null; // Workaround for Xamarin.Forms.Platform.WPF issue: https://github.com/xamarin/Xamarin.Forms/issues/3648
                }
            });
        }

        private async Task DiscoverPrintersAsync()
        {
            await ClearDiscoveredPrinterListAsync();

            await Task.Factory.StartNew(() =>
            {
                IsPrinterListRefreshing = true;
            });

            DiscoveredPrinters = await _printerService.DiscoverBluetoothPrintersAsync();

            await Task.Factory.StartNew(() =>
            {
                IsPrinterListRefreshing = false;
            });
        }

        #endregion
    }
}

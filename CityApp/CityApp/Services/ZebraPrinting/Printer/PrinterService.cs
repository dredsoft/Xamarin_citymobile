using CityApp.Utilities.ZebraPrinting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using Zebra.Sdk.Printer.Discovery;

namespace CityApp.Services.ZebraPrinting.Printer
{
    public class PrinterService : IPrinterService
    {
        private const string DeviceLanguagesSgd = "device.languages";
          
        public PrinterService()
        { 
        }

        #region Implementation of IPrinterService

        public async Task<IEnumerable<DiscoveredPrinter>> DiscoverBluetoothPrintersAsync()
        {
            IEnumerable<DiscoveredPrinter> discoveredPrinters = new List<DiscoveredPrinter>();

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    DiscoveryHandlerImplementation bluetoothDiscoveryHandler = new DiscoveryHandlerImplementation(discoveredPrinters);
                    DependencyService.Get<IConnectionManager>().FindBluetoothPrinters(bluetoothDiscoveryHandler);

                    while (!bluetoothDiscoveryHandler.IsFinished)
                    {
                        Thread.Sleep(100);
                    }
                }
                catch (Exception ex)
                {
                    // Do nothing
                }
            });

            return discoveredPrinters;
        }

        public async Task PrintAsync(DiscoveredPrinter printer, string message)
        {
            Connection connection = null;
            bool linePrintEnabled = false;

            try
            {
                await Task.Factory.StartNew(() =>
                {
                    connection = ConnectionCreator.Create(printer);
                    connection.Open();

                    string originalPrinterLanguage = SGD.GET(DeviceLanguagesSgd, connection);
                    linePrintEnabled = "line_print".Equals(originalPrinterLanguage, StringComparison.OrdinalIgnoreCase);

                    if (linePrintEnabled)
                    {
                        SGD.SET(DeviceLanguagesSgd, "zpl", connection);
                    }

                    ZebraPrinter zebraPrinter = ZebraPrinterFactory.GetInstance(connection);
                    ZebraPrinterLinkOs linkOsPrinter = ZebraPrinterFactory.CreateLinkOsPrinter(zebraPrinter);

                    string errorMessage = GetPrinterStatusErrorMessage(zebraPrinter.GetCurrentStatus());
                    if (errorMessage != null)
                    {
                        throw new PrinterNotReadyException($"{errorMessage}. Please check your printer and try again.");
                    }
                    else
                    {
                        connection.Write(Encoding.UTF8.GetBytes(message)); 
                    }
                });

                if (linePrintEnabled)
                {
                    await ResetPrinterLanguageToLinePrintAsync(connection);
                }
            }
            catch (PrinterNotReadyException e)
            {
                if (linePrintEnabled)
                {
                    await ResetPrinterLanguageToLinePrintAsync(connection);
                }
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                if (linePrintEnabled)
                {
                    await ResetPrinterLanguageToLinePrintAsync(connection);
                }
                throw new Exception(e.Message);
            }
            finally
            {
                await Task.Factory.StartNew(() =>
                {
                    try
                    {
                        connection?.Close();
                    }
                    catch (ConnectionException) { }
                });
            }
        }

        #endregion

        private async Task ResetPrinterLanguageToLinePrintAsync(Connection connection)
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    connection?.Open();
                    SGD.SET(DeviceLanguagesSgd, "line_print", connection);
                }
                catch (ConnectionException) { }
            });
        }

        private string GetPrinterStatusErrorMessage(PrinterStatus printerStatus)
        {
            if (printerStatus.isReadyToPrint)
            {
                if (printerStatus.isHeadCold)
                {
                    return "Printhead too cold";
                }
                else if (printerStatus.isPartialFormatInProgress)
                {
                    return "Partial format in progress";
                }
            }
            else
            {
                if (printerStatus.isHeadTooHot)
                {
                    return "Printhead too hot";
                }
                else if (printerStatus.isHeadOpen)
                {
                    return "Printhead open";
                }
                else if (printerStatus.isPaperOut)
                {
                    return "Media out";
                }
                else if (printerStatus.isReceiveBufferFull)
                {
                    return "Receive buffer full";
                }
                else if (printerStatus.isRibbonOut)
                {
                    return "Ribbon error";
                }
                else if (printerStatus.isPaused)
                {
                    return "Printer paused";
                }
                else
                {
                    return "Unknown error";
                }
            }
            return null;
        }
    }


    public class PrinterNotReadyException : Exception
    {
        public PrinterNotReadyException(string message) : base(message) { }
    }

    class DiscoveryHandlerImplementation : DiscoveryHandler
    {
        private List<DiscoveredPrinter> _discoveredPrinters;

        public bool IsFinished { get; private set; } = false;

        public DiscoveryHandlerImplementation(IEnumerable<DiscoveredPrinter> discoveredPrinters)
        {
            _discoveredPrinters = (List<DiscoveredPrinter>)discoveredPrinters;
        }

        public void DiscoveryError(string message)
        {
            IsFinished = true;
        }

        public void DiscoveryFinished()
        {
            IsFinished = true;
        }

        public void FoundPrinter(DiscoveredPrinter printer)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                _discoveredPrinters.Add(printer); // ListView view model operations must be done on UI thread due to iOS issues when clearing list while item is selected: https://forums.xamarin.com/discussion/19114/invalid-number-of-rows-in-section
            });
        }
    }
}

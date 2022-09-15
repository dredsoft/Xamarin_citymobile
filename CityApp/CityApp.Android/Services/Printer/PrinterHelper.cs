using CityApp.Droid.Services.Printer; 
using CityApp.Services.ZebraPrinting;
using Xamarin.Forms; 
using Zebra.Sdk.Printer.Discovery;

[assembly: Dependency(typeof(PrinterHelper))]
namespace CityApp.Droid.Services.Printer
{
    public class PrinterHelper : IPrinterHelper {
        public bool IsBluetoothPrinter(DiscoveredPrinter printer) {
            return printer is DiscoveredPrinterBluetooth;
        }

        public bool IsUsbDirectPrinter(DiscoveredPrinter printer) {
            return printer is DiscoveredPrinterUsb;
        }

        public bool IsUsbDriverPrinter(DiscoveredPrinter printer) {
            return false; // No implementation for USB driver printers in Android portion of Xamarin SDK
        }
    }
}
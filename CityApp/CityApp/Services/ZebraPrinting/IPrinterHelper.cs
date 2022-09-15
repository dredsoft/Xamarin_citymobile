using Zebra.Sdk.Printer.Discovery;

namespace CityApp.Services.ZebraPrinting
{
    public interface IPrinterHelper
    {
        bool IsBluetoothPrinter(DiscoveredPrinter printer);

        bool IsUsbDirectPrinter(DiscoveredPrinter printer);

        bool IsUsbDriverPrinter(DiscoveredPrinter printer);
    }
}

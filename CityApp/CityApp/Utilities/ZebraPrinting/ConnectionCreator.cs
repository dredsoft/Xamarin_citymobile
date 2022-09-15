using CityApp.Services.ZebraPrinting;
using Xamarin.Forms;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer.Discovery;

namespace CityApp.Utilities.ZebraPrinting
{
    public static class ConnectionCreator
    { 
        public static Connection Create(DiscoveredPrinter printer)
        {
             if (DependencyService.Get<IPrinterHelper>().IsBluetoothPrinter(printer))
            {
                return DependencyService.Get<IConnectionManager>().GetBluetoothConnection(printer.Address);
            }
            else
            {
                return new TcpConnection(printer.Address, 9100);
            }
        }
    }
}

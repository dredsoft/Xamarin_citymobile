using System.Collections.Generic;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer.Discovery;

namespace CityApp.Services.ZebraPrinting
{
    public interface IConnectionManager
    { 
        void FindBluetoothPrinters(DiscoveryHandler discoveryHandler);

        void FindUrlPrinters(string nfcData, DiscoveryHandler discoveryHandler);

        Connection GetBluetoothConnection(string macAddress);

        Connection GetUsbDirectConnection(string symbolicName);

        Connection GetUsbDriverConnection(string printerName);

        void GetZebraUsbDirectPrinters(DiscoveryHandler discoveryHandler);

        List<DiscoveredPrinter> GetZebraUsbDriverPrinters();

        bool IsBluetoothPrinter(DiscoveredPrinter printer);
    }
}

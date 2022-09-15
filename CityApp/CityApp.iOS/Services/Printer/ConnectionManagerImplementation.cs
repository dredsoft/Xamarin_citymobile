using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using XamarinPrintStation.iOS;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer.Discovery;

[assembly: Dependency(typeof(ConnectionManagerImplementation))]
namespace XamarinPrintStation.iOS {
    public class ConnectionManagerImplementation : IConnectionManager {
        public void FindBluetoothPrinters(DiscoveryHandler discoveryHandler) {
            BluetoothDiscoverer.FindPrinters(discoveryHandler);
        }

        public void FindUrlPrinters(string nfcData, DiscoveryHandler discoveryHandler) {
            throw new NotImplementedException();
        }

        public Connection GetBluetoothConnection(string macAddress) {
            return new BluetoothConnection(macAddress);
        }

        public Connection GetUsbDirectConnection(string symbolicName) {
            throw new NotImplementedException();
        }

        public Connection GetUsbDriverConnection(string printerName) {
            throw new NotImplementedException();
        }

        public void GetZebraUsbDirectPrinters(DiscoveryHandler discoveryHandler) {
            throw new NotImplementedException();
        }

        public List<DiscoveredPrinter> GetZebraUsbDriverPrinters() {
            throw new NotImplementedException();
        }

        public bool IsBluetoothPrinter(DiscoveredPrinter printer) {
            return printer is DiscoveredPrinterBluetooth;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics; 
using System.Threading;
using Android.App;
using Android.Content;
using Android.Hardware.Usb;
using CityApp.Droid.Services.Printer;
using CityApp.Services.ZebraPrinting;
using Xamarin.Forms;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer.Discovery;

[assembly: Dependency(typeof(ConnectionManager))]
namespace CityApp.Droid.Services.Printer
{
    public class ConnectionManager : IConnectionManager {
        
        private const string ActionUsbPermission = "com.text2ticket.texttoticket.USB_PERMISSION";
        private const int UsbPermissionTimeout = 30000;

        private static readonly object UsbConnectionLock = new object();

        private readonly IntentFilter filter = new IntentFilter("ACTION_USB_PERMISSION");

        #region IConnectionManager Implementation

        public void FindBluetoothPrinters(DiscoveryHandler discoveryHandler) {
            BluetoothDiscoverer.FindPrinters(Android.App.Application.Context, discoveryHandler);
        }

        public void FindUrlPrinters(string nfcData, DiscoveryHandler discoveryHandler) {
            UrlPrinterDiscoverer.FindPrinters(nfcData, discoveryHandler, Android.App.Application.Context);
        }

        public Connection GetBluetoothConnection(string macAddress) {
            return new BluetoothConnection(macAddress);
        }

        private UsbDevice GetUsbDevice(UsbManager usbManager, string deviceAddress) {
            IDictionary<string, UsbDevice> deviceList = usbManager.DeviceList;
            return deviceList != null && deviceList.ContainsKey(deviceAddress) ? deviceList[deviceAddress] : null;
        }

        public void GetZebraUsbDirectPrinters(DiscoveryHandler discoveryHandler) {
            UsbDiscoverer.FindPrinters(Android.App.Application.Context, discoveryHandler);
        }

        public List<DiscoveredPrinter> GetZebraUsbDriverPrinters() {
            throw new NotImplementedException();
        }

        public bool IsBluetoothPrinter(DiscoveredPrinter printer) {
            return printer is DiscoveredPrinterBluetooth;
        }

        public Connection GetUsbDirectConnection(string symbolicName) {
            lock (UsbConnectionLock) {
                try {
                    UsbManager usbManager = (UsbManager)Android.App.Application.Context.GetSystemService(Context.UsbService);
                    string deviceAddress = symbolicName.Substring(symbolicName.IndexOf(":") + 1);

                    UsbDevice usbDevice = GetUsbDevice(usbManager, deviceAddress);
                    if (usbDevice != null) {
                        if (!usbManager.HasPermission(usbDevice)) {
                            PendingIntent permissionIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, 0, new Intent(ActionUsbPermission), 0);
                            usbManager.RequestPermission(usbDevice, permissionIntent);

                            Stopwatch stopwatch = new Stopwatch();
                            stopwatch.Start();

                            do {
                                Thread.Sleep(10);
                                if (stopwatch.ElapsedMilliseconds > UsbPermissionTimeout) {
                                    throw new ConnectionException("Timed out waiting for USB permission.");
                                }
                            } while (UsbReceiver.Result != Result.Ok);

                            if (!UsbReceiver.HasPermission) {
                                throw new ConnectionException("USB permission denied.");
                            }
                        }

                        return new UsbConnection(usbManager, usbDevice);
                    } else {
                        throw new ConnectionException($"USB device '{deviceAddress}' was not found.");
                    }
                } finally {
                    UsbReceiver.Reset();
                }
            }
        }

        public Connection GetUsbDriverConnection(string printerName) {
            throw new NotImplementedException();
        }

        #endregion

        [BroadcastReceiver]
        [IntentFilter(new[] { ActionUsbPermission })]
        public class UsbReceiver : BroadcastReceiver {
            public UsbReceiver() {
                Reset();
            }

            public static bool HasPermission { get; private set; } = false;

            public static Result Result { get; private set; } = Result.Canceled;

            public static void Reset() {
                HasPermission = false;
                Result = Result.Canceled;
            }

            public override void OnReceive(Context context, Intent intent) {
                if (ActionUsbPermission.Equals(intent.Action)) {
                    UsbDevice device = (UsbDevice)intent.GetParcelableExtra(UsbManager.ExtraDevice);
                    if (intent.GetBooleanExtra(UsbManager.ExtraPermissionGranted, false)) {
                        if (device != null) {
                            HasPermission = true;
                        }
                    }
                    Result = Result.Ok;
                }
            }
        }
    }
}
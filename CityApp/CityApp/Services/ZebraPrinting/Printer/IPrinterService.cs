using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zebra.Sdk.Printer.Discovery;

namespace CityApp.Services.ZebraPrinting.Printer
{
    public interface IPrinterService
    {
        #region Methods

        Task<IEnumerable<DiscoveredPrinter>> DiscoverBluetoothPrintersAsync();

        Task PrintAsync(DiscoveredPrinter printer, string message);

        #endregion
    }
}

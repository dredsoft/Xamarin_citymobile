using System;
using System.Collections.Generic;
using System.Text;

namespace CityApp.Models.Enums
{
    [Flags]
    public enum SystemPermissions
    {
        None = 0,
        Administrator = 1,
        Government = 2,
        Vendor = 3,
        ParkingOfficer = 4,
        CodeEnforcementOfficers = 5
    }
}

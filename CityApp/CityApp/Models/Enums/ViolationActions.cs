using System;
using System.Diagnostics;

namespace CityApp.Models.Enums
{
    [Flags]
    public enum ViolationActions
    {
        [DebuggerDisplay("Towable")]
        Towable = 1 << 0,

        [DebuggerDisplay("Booting")]
        Booting = 1 << 1,

    }
}

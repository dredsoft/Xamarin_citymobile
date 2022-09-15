using System;
using System.Diagnostics;

namespace CityApp.Models.Enums
{
	[Flags]
	public enum AccountFeatures
	{
		[DebuggerDisplay("Info and Events")]
		Info = 1 << 0,
	}
}

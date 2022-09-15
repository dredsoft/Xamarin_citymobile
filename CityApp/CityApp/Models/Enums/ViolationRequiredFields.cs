using System;
using System.Diagnostics;

namespace CityApp.Models.Enums
{
	[Flags]
	public enum ViolationRequiredFields
	{
		[DebuggerDisplay("Vehicle Information")]
		VehicleInformation = 1 << 0,

		[DebuggerDisplay("VideoPath")]
		Video = 1 << 1,

		[DebuggerDisplay("Photo")]
		Photo = 1 << 2,

		[DebuggerDisplay("VideoPath Audio")]
		VideoAudio = 1 << 3,
	}
}

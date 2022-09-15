using System.Diagnostics;

namespace CityApp.Models.Models.Violation
{
	[DebuggerDisplay(nameof(Name))]
	public class ViolationTypeClientModel
	{
		public string Name { get; set; }

		//Use default icon for now
		public string Icon => "violation_icon";

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			return string.Equals(Name, ((ViolationTypeClientModel)obj).Name);
		}
	}
}
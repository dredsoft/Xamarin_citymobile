using System.Collections.Generic;

namespace CityApp.Resources
{
	public class States
	{
		#region Private Fields

		public static Dictionary<string, string> USA;

		#endregion

		#region Constructors

		static States()
		{
			USA = new Dictionary<string, string>();
			InitializeStates();
		}

		#endregion

		#region Private Fiedls

		private static void InitializeStates()
		{
			USA.Add("AL", "Alabama");
			USA.Add("AK", "Alaska");
			USA.Add("AZ", "Arizona");
			USA.Add("AR", "Arkansas");
			USA.Add("CA", "California");
			USA.Add("CO", "Colorado");
			USA.Add("CT", "Connecticut");
			USA.Add("DE", "Delaware");
			USA.Add("DC", "District of Columbia");
			USA.Add("FL", "Florida");
			USA.Add("GA", "Georgia");
			USA.Add("HI", "Hawaii");
			USA.Add("ID", "Idaho");
			USA.Add("IL", "Illinois");
			USA.Add("IN", "Indiana");
			USA.Add("IA", "Iowa");
			USA.Add("KS", "Kansas");
			USA.Add("KY", "Kentucky");
			USA.Add("LA", "Louisiana");
			USA.Add("ME", "Maine");
			USA.Add("MD", "Maryland");
			USA.Add("MA", "Massachusetts");
			USA.Add("MI", "Michigan");
			USA.Add("MN", "Minnesota");
			USA.Add("MS", "Mississippi");
			USA.Add("MO", "Missouri");
			USA.Add("MT", "Montana");
			USA.Add("NE", "Nebraska");
			USA.Add("NV", "Nevada");
			USA.Add("NH", "New Hampshire");
			USA.Add("NJ", "New Jersey");
			USA.Add("NM", "New Mexico");
			USA.Add("NY", "New York");
			USA.Add("NC", "North Carolina");
			USA.Add("ND", "North Dakota");
			USA.Add("OH", "Ohio");
			USA.Add("OK", "Oklahoma");
			USA.Add("OR", "Oregon");
			USA.Add("PA", "Pennsylvania");
			USA.Add("RI", "Rhode Island");
			USA.Add("SC", "South Carolina");
			USA.Add("SD", "South Dakota");
			USA.Add("TN", "Tennessee");
			USA.Add("TX", "Texas");
			USA.Add("UT", "Utah");
			USA.Add("VT", "Vermont");
			USA.Add("VA", "Virginia");
			USA.Add("WA", "Washington");
			USA.Add("WV", "West Virginia");
			USA.Add("WI", "Wisconsin");
			USA.Add("WY", "Wyoming");
		}

		#endregion
	}
}

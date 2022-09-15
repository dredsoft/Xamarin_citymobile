using CityApp.Utilities.UserDialogs.Abstractions;
using CityApp.Utilities.UserDialogs.Implementations;

namespace CityApp.Utilities.UserDialogs
{
	public class UserDialogs
	{
		#region Constructors

		static UserDialogs()
		{
			Instance = new UserDialogsImplementation();
		}

		private UserDialogs()
		{
		}

		#endregion

		#region Properties

		public static IUserDialogs Instance { get; private set; }

		#endregion
	}
}
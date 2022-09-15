using CityApp.Utilities.UserDialogs.Components.Alert.Abstraction;
using CityApp.Utilities.UserDialogs.Components.Confirm.Abstractions;
using CityApp.Utilities.UserDialogs.Components.Toast.Abstractions;

namespace CityApp.Utilities.UserDialogs.Abstractions
{

	public interface IUserDialogs
	{
		IAlert Alert { get; }

		IConfirm Confirm { get; }

		IToastMessage ToastMessage { get; }
	}
}
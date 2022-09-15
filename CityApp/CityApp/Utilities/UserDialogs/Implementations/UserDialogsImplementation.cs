using CityApp.Utilities.UserDialogs.Abstractions;
using CityApp.Utilities.UserDialogs.Components.Alert.Abstraction;
using CityApp.Utilities.UserDialogs.Components.Alert.Implementations;
using CityApp.Utilities.UserDialogs.Components.Confirm.Abstractions;
using CityApp.Utilities.UserDialogs.Components.Confirm.Implementations;
using CityApp.Utilities.UserDialogs.Components.Toast.Abstractions;
using CityApp.Utilities.UserDialogs.Components.Toast.Implementations;


namespace CityApp.Utilities.UserDialogs.Implementations
{
	public class UserDialogsImplementation : IUserDialogs
	{
		#region Private Fields

		private IAlert _alert;

		private IConfirm _confirm;

		private IToastMessage _toastMessage;

		#endregion

		#region Public Properties

		public IAlert Alert => _alert ?? (_alert = new AlertAcrImplementation());

		public IConfirm Confirm => _confirm ?? (_confirm = new ConfirmAcrImplementation());

		public IToastMessage ToastMessage
			=> _toastMessage ?? (_toastMessage = new ToastMessageImplementation());

		#endregion
	}
}

using Acr.UserDialogs;
using CityApp.Utilities.UserDialogs.Components.Alert.Abstraction;

namespace CityApp.Utilities.UserDialogs.Components.Alert.Implementations
{
	public class AlertAcrImplementation : IAlert
	{
		#region Private fields

		private readonly IUserDialogs _userDialogsInstance = Acr.UserDialogs.UserDialogs.Instance;

		#endregion

		#region Implementation of IAlert

		public void Show(AlertConfig config)
		{
			_userDialogsInstance.Alert(new Acr.UserDialogs.AlertConfig
			{
				Message = config.Message,
				OkText = config.OkText,
				OnAction = config.OnAction,
				Title = config.Title
			});
		}

		#endregion
	}
}
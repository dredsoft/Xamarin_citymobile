extern alias SplatAlias;
using Acr.UserDialogs;
using CityApp.Models.Extensions;
using CityApp.Utilities.UserDialogs.Components.Toast.Abstractions;

namespace CityApp.Utilities.UserDialogs.Components.Toast.Implementations
{
	public class ToastMessageImplementation : IToastMessage
	{

		#region Private fields


		private readonly IUserDialogs _userDialogsInstance = Acr.UserDialogs.UserDialogs.Instance;

		#endregion

		#region Implementation of IToastNotificator

		public void Show(ToastConfig config) => _userDialogsInstance.Toast(MapConfigurations(config));

		#endregion

		#region Private methods

		private Acr.UserDialogs.ToastConfig MapConfigurations(ToastConfig config)
		{
			var acrToastConfig = new Acr.UserDialogs.ToastConfig(config.ToastMessage){
				BackgroundColor = config.BackgroundColor.ToSystemColor(),
				Duration = config.Duration,
				MessageTextColor = config.MessageTextColor.ToSystemColor()
			};

			if (config.Action != null)
			{
				acrToastConfig.Action = new ToastAction
				{
					Action = config.Action,
					Text = config.ActionText,
					TextColor = config.ActionTextColor.ToSystemColor()
				};
			}

			return acrToastConfig;
		}

		#endregion
	}
}

using Acr.UserDialogs;
using CityApp.Utilities.UserDialogs.Components.Confirm.Abstractions;

namespace CityApp.Utilities.UserDialogs.Components.Confirm.Implementations
{
	public class ConfirmAcrImplementation : IConfirm
	{
		#region Private fields

		private readonly IUserDialogs _userDialogsInstance = Acr.UserDialogs.UserDialogs.Instance;

		#endregion

		#region Implementation of IConfirm

		public void Show(ConfirmConfig config)
		{
			_userDialogsInstance.Confirm(new Acr.UserDialogs.ConfirmConfig
			{
				CancelText = config.CancelText,
				OkText = config.OkText,
				Title = config.Title,
				Message = config.Message,
				OnAction = config.OnAction
			});
		}

		#endregion
	}
}
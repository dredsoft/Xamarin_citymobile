using System;

namespace CityApp.Utilities.UserDialogs.Components.Alert
{
	public class AlertConfig
	{
		#region Public Properties

		public string Message { get; set; }

		public string OkText { get; set; }

		public Action OnAction { get; set; }

		public string Title { get; set; }

		#endregion
	}
}
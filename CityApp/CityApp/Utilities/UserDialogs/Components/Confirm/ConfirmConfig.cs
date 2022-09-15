using System;

namespace CityApp.Utilities.UserDialogs.Components.Confirm
{
	public class ConfirmConfig
	{
		#region Public Properties

		public string CancelText { get; set; }

		public bool IsCancellable { get; set; }

		public int? MaxLength { get; set; }

		public string Message { get; set; }

		public string OkText { get; set; }

		public string Text { get; set; }

		public string Title { get; set; }

		public Action<bool> OnAction { get; set; }

		#endregion
	}
}
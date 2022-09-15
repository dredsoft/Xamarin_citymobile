using System;
using Xamarin.Forms;

namespace CityApp.Utilities.UserDialogs.Components.Toast
{
	public class ToastConfig
    {
	    public Color BackgroundColor { get; set; }

	    public Color MessageTextColor { get; set; }

	    public string ToastMessage { get; set; }

	    public TimeSpan Duration { get; set; }

	    public Action Action { get; set; }

	    public string ActionText { get; set; }

	    public Color ActionTextColor { get; set; }
	}
}

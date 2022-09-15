using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using PlatformEffects = CityApp.iOS.Effects;

[assembly: ResolutionGroupName("CityApp.Interactions.Effects")]
[assembly: ExportEffect(typeof(PlatformEffects.EntryCapitalizeKeyboard), "EntryCapitalizeKeyboard")]
namespace CityApp.iOS.Effects
{
	public class EntryCapitalizeKeyboard : PlatformEffect
	{
		#region Private Fields

		private UITextAutocapitalizationType _old;

		#endregion

		#region Protected Methods

		protected override void OnAttached()
		{
			if (!(Control is UITextField editText))
			{
				return;
			}

			_old = editText.AutocapitalizationType;
			editText.AutocapitalizationType = UITextAutocapitalizationType.AllCharacters;
		}

		protected override void OnDetached()
		{
			if (!(Control is UITextField editText))
			{
				return;
			}

			editText.AutocapitalizationType = _old;
		}

		#endregion
	}
}
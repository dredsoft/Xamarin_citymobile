using System.Linq;
using Android.Text;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using PlatformEffects = CityApp.Droid.Effects;

[assembly: ResolutionGroupName("CityApp.Interactions.Effects")]
[assembly: ExportEffect(typeof(PlatformEffects.EntryCapitalizeKeyboard), "EntryCapitalizeKeyboard")]
namespace CityApp.Droid.Effects
{
	public class EntryCapitalizeKeyboard : PlatformEffect
	{
		#region Private Fields

		private InputTypes _old;
		private IInputFilter[] _oldFilters;

		#endregion

		#region Protected Methods

		protected override void OnAttached()
		{
			if (!(Control is EditText editText))
			{
				return;
			}

			_old = editText.InputType;
			_oldFilters = editText.GetFilters().ToArray();

			editText.SetRawInputType(InputTypes.ClassText | InputTypes.TextFlagCapCharacters);

			var newFilters = _oldFilters.ToList();
			newFilters.Add(new InputFilterAllCaps());
			editText.SetFilters(newFilters.ToArray());
		}

		protected override void OnDetached()
		{
			if (!(Control is EditText editText))
			{
				return;
			}

			editText.SetRawInputType(_old);
			editText.SetFilters(_oldFilters);
		}

		#endregion
	}
}
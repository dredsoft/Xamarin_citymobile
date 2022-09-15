using System;
using CityApp.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CityApp.Interactions.MarkupExtensions
{
	[ContentProperty("ResourceKey")]
	public class TranslateExtension : IMarkupExtension
	{
		#region Properties

		public string ResourceKey { get; set; }

		#endregion

		#region Public Methods

		public object ProvideValue(IServiceProvider serviceProvider)
		{
			if (ResourceKey == null)
			{
				return string.Empty;
			}

			var translation = AppResources.ResourceManager.GetString(ResourceKey);

			if (translation == null)
			{
				throw new ArgumentException(string.Format("Hey was not found for culture", ResourceKey,
					AppResources.Culture.Name));
			}

			return translation;
		}

		#endregion
	}
}

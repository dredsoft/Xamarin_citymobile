using System;

namespace CityApp.Models.Attributes
{
	public class TextRepresentationAttribute : Attribute
	{
		#region Constructors

		public TextRepresentationAttribute(string text)
		{
			Representation = text;
		}

		#endregion

		#region Properties

		public string Representation { get; set; }

		#endregion
	}
}

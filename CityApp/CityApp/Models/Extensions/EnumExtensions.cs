using System;
using System.Reflection;
using CityApp.Models.Attributes;

namespace CityApp.Models.Extensions
{
    public static class EnumExtensions
    {
	    #region Methods

	    public static T GetAttribute<T>(this Enum en) where T : Attribute
	    {
		    var type = en.GetType();

		    return type.GetRuntimeField(en.ToString()).GetCustomAttribute<T>();
	    }

	    public static string GetTextRepresentation(this Enum en)
	    {
		    var textAttribute = en.GetAttribute<TextRepresentationAttribute>();

		    if (textAttribute == null)
		    {
			    return en.ToString();
		    }

		    return textAttribute.Representation;
	    }

	    #endregion
	}
}

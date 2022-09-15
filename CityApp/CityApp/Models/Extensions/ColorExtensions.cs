extern alias SplatAlias;

namespace CityApp.Models.Extensions
{
   public static class ColorExtensions
    {
	    #region Constants

	    private const int MAX_BYTE_VALUE = 255;

		#endregion

		#region Extension Methods

	    public static SplatAlias::System.Drawing.Color? ToSystemColor(this Xamarin.Forms.Color xamarinColor)
	    {
		    return SplatAlias::System.Drawing.Color.FromArgb(
			    (byte)xamarinColor.A * MAX_BYTE_VALUE,
			    (byte)(xamarinColor.R * MAX_BYTE_VALUE),
			    (byte)(xamarinColor.G * MAX_BYTE_VALUE),
			    (byte)(xamarinColor.B * MAX_BYTE_VALUE));
	    }

		#endregion
	}
}

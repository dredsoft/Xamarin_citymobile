using CityApp.Models.Attributes;

namespace CityApp.Models.Enums
{
    public enum MimeTypes
    {
	    [TextRepresentation("video/mp4")]
		Video = 1,
	    [TextRepresentation("image/png")]
		Image = 2
    }
}

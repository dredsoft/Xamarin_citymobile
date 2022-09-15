using CityApp.Models.Attributes;

namespace CityApp.Models.Enums
{
    public enum ViolationTypes
	{
		[TextRepresentation("Texting & Driving")]
		TextingAndDriving = 1,
		ServiceRequest = 2,
		ParkingEnforcement,
		CodeEnforcement
    }
}

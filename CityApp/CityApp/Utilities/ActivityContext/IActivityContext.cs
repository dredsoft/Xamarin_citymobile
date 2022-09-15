using System;

namespace CityApp.Utilities.ActivityContext
{
    public interface IActivityContext : IDisposable
    {
		#region Properties

		IActivityProvider ActivityProvider { get; }

		#endregion
	}

    public interface IActivityProvider
    {
		#region Properties

		bool IsBusy { get; set; }

		#endregion
	}
}

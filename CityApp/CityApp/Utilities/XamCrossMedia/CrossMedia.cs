namespace CityApp.Utilities.XamCrossMedia
{
    public class CrossMedia
	{
		#region Constructors

		static CrossMedia()
		{
			Instance = new CrossMediaImplementation();
		}

		private CrossMedia()
		{
		}

		#endregion

		#region Properties

		public static ICrossMedia Instance { get; private set; }

		#endregion
	}
}


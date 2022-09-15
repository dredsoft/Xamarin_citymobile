namespace CityApp.Utilities.ActivityContext
{
    /// <summary>
    ///     Convenient wrapper to avoid setting an activity indicator value by hand
    /// </summary>
    /// <example>
    /// using(new ActivityContext(/*instance of IActivityProvider implementation*/))
    /// {
    ///  //your background activity
    /// }
    /// </example>
    public class ActivityContext : IActivityContext
    {

		#region Properties

	    public IActivityProvider ActivityProvider { get; private set; }

		#endregion

		#region Constructors

		public static IActivityContext MakeContext(IActivityProvider activityProvider)
		{
			return new ActivityContext(activityProvider);
		}

		#endregion

		#region Public Methods

		public void Dispose()
		{
			ActivityProvider.IsBusy = false;
			ActivityProvider = null;
		}

		#endregion

		#region Private Methods

	    private ActivityContext(IActivityProvider activityProvider)
	    {
		    ActivityProvider = activityProvider;
		    InitActivity();
	    }

	    private void InitActivity()
	    {
		    ActivityProvider.IsBusy = true;
	    }

		#endregion

	}
}

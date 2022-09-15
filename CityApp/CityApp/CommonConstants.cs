namespace CityApp
{
    public static class CommonConstants
    {
        public const string ApiUrl = "https://cityappapiqa.azurewebsites.net";

        public const string PasswordLenghtRegex = @"^.{8,}";
        public const string PasswordCapitalRegex = @"^(?=.*[A-Z])";
        public const string PasswordLowercaseRegex = @"^(?=.*[a-z])";
        public const string PasswordSpecialSymbolRegex = @"^(?=.*[_!@#$%^&*-])";
        public const string PasswordDigitsRegex = @"^(?=.*\d)";
        public const string PasswordNewLineRegex = @"^(?!.*[.\n])";
        public const string EmailRegex = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

        public static string AmazonS3Bucket = "cityapp.qa.west";
        public static string AWSSecretKey = "+0ZYyJbwgS2yfnyjxVUF/62q3HlK7hNev2uq2Yhp";
        public static string FileTypes = ".mp3,.wav,.wmv,.mov,.mp4,.m4a,.3gp,.bmp,.gif,.jpg,.png,.webp";
        public static string AWSAccessKeyID = "AKIAI34B42MPVNTLLR6A";

	    public const string VIDEO_EXTENSION = ".mp4";
	    public const string THUMBNAIL_EXTENSION = "_thumb.png";
	    public const string VIDEO_DIRECTORY_NAME = "Violations";

		public const string LOCAL_IOS_FILE_EXTENSION = "file://";
		public const int SUBMISSIONS_PER_PAGE = 5;

	    public const double MAP_ZOOM_VALUE = 17;
	    public const double MAP_ZOOM_DISTANCE_VALUE = 0.1;
		public const double LOCATION_RESTRICT_VALUE = 0.076;

	    public const string INDEXER_NAME = "Item[]";

	    public const string APPCENTER_DROID_KEY = "android=5e45fade-c425-4204-9cd9-a5badee87cfc;";
	    public const string APPCENTER_IOS_KEY = "ios=74dc00ac-f4f0-4321-b772-0ee5ebd29aaa;";

	    public const string BACKGROUND_TASK = "backgrounTask";

	}
}

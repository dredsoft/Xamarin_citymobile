using System.Threading.Tasks;
using System.Windows.Input;
using CityApp.Core.ViewModels.Abstractions;
using CityApp.Infrastructure.NavigationManager;
using CityApp.Infrastructure.Storages;
using CityApp.Infrastructure.Storages.Constants;
using CityApp.Resources;
using CityApp.Utilities.ActivityContext;
using CityApp.Utilities.Logging;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;
using Plugin.MediaManager.Abstractions.Enums;
using Plugin.MediaManager.Abstractions.EventArguments;
using Xamarin.Forms;

namespace CityApp.Modules.MediaPlayer
{
	public class MediaPlayerViewModel : BaseViewModel
    {
        #region Private Fields

        private IPlaybackController PlaybackController => CrossMediaManager.Current.PlaybackController;

	    private double _progress;

	    private bool _isMoviePlaying;

        #endregion

        #region Constructors

        public MediaPlayerViewModel(INavigationManager navigationManager) : base(navigationManager)
        {
    
        }

	    #endregion

        #region Properties

        public override ILogger Logger => LogManager.GetLog();

        public string VideoSource => SessionStorage.Instance.Get<string>(StorageConstants.VIDEO_SOURCE_KEY);

	    public double Progress
	    {
			get => _progress;
		    set => SetProperty(ref _progress, value);
	    }

	    public bool IsMoviePlaying
	    {
		    get => _isMoviePlaying;
		    set => SetProperty(ref _isMoviePlaying, value);
	    }

		#endregion

		#region Commands

		public ICommand PlayCommand => new Command(PlayExecute);

		#endregion

		#region Public Methods

        public override async Task Init(object args)
		{
			using (ActivityContext.MakeContext(this))
			{
				await base.Init(args).ConfigureAwait(false);

				Title = AppResources.txtWatchVideo;

			    CrossMediaManager.Current.PlayingChanged += PlayingChangeExecute;
			    CrossMediaManager.Current.StatusChanged += CurrentStatusChangedExecute;
            }
		}

	    public override async void OnAppearing()
	    {
		    base.OnAppearing();

		    IsMoviePlaying = true;
			await PlaybackController.Play();
	    }

		public override async void OnDisappearing()
        {
             base.OnDisappearing();

			await PlaybackController.Stop();
        }

        public override void PageClosing()
        {
            base.PageClosing();

	        CrossMediaManager.Current.PlayingChanged -= PlayingChangeExecute;
	        CrossMediaManager.Current.StatusChanged -= CurrentStatusChangedExecute;
            CrossMediaManager.Current.MediaQueue.Clear();
           
            SessionStorage.Instance.TryRemove(StorageConstants.VIDEO_SOURCE_KEY);
        }

        #endregion

        #region Private Methods

        private async void PlayExecute()
        {
	       await PlaybackController.PlayPause();
		}

		private void PlayingChangeExecute(object sender, PlayingChangedEventArgs playingChangedEventArgs)
		{
			Progress = playingChangedEventArgs.Progress;	
		}

	    private void CurrentStatusChangedExecute(object sender, StatusChangedEventArgs statusChangedEventArgs)
	    {
	        IsMoviePlaying = statusChangedEventArgs.Status == MediaPlayerStatus.Playing;
	    }

		#endregion
	}
}

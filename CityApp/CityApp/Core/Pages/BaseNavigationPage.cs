using CityApp.Utilities.Logging;
using Xamarin.Forms;

namespace CityApp.Core.Pages
{
    public class BaseNavigationPage : NavigationPage, ILogTarget
    {
        public ILogger Logger => LogManager.GetLog();

        public BaseNavigationPage(Page root, bool presentToolbar = true) : base(root)
        {
            Logger.Trace();
            if (!presentToolbar)
            {
                SetHasNavigationBar(root, false);
            }
        }

        protected override void OnAppearing()
        {
            Logger.Trace();
            Popped += Page_Popped;
            base.OnAppearing();
        }

        public void Page_Popped(object sender, NavigationEventArgs e)
        {
            Logger.Trace();
            var page = e.Page;

            if (page is IBasePage)
            {
                ((IBasePage)page).OnPageClosing();
            }
        }

        protected override void OnDisappearing()
        {
            Logger.Trace();
            Popped -= Page_Popped;
            base.OnDisappearing();
        }

    }
}

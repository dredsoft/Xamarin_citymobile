using CityApp.Core.Pages;
using CityApp.Core.ViewModels.Abstractions;
using Xamarin.Forms;

namespace CityApp.Shared.Extensions
{
    public static class PageExtensions
    {
        public static void BindViewModel(this IBasePage page, IViewModel viewModel)
        {
            page.BindingContext = viewModel;
            page.SetBinding<IViewModel>(Page.IsBusyProperty, "IsBusy");
            page.SetBinding<IViewModel>(Page.TitleProperty, "Title");
            page.SetBinding<IViewModel>(Page.IconProperty, "Icon");

            page.Appearing += (sender, args) => viewModel.OnAppearing();
            page.Disappearing += (sender, args) => viewModel.OnDisappearing();
	        page.PageClosing += (sender, args) => viewModel.PageClosing();
		}
    }
}

using System;
using CityApp.Core.ViewModels.Abstractions;
using CityApp.Utilities.Logging;
using Xamarin.Forms;

namespace CityApp.Infrastructure.PageLocators.Abstractions
{
    public interface IPageLocator : ILogTarget
    {
        Page ResolvePageAndViewModel(Type viewModelType, object args = null);

        Page ResolvePage(IViewModel viewModel);

        Type ResolvePageType(Type viewmodel);
    }
}

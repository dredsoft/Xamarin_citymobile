using System;
using CityApp.Core.Pages;
using CityApp.Core.ViewModels.Abstractions;
using CityApp.Utilities.Logging;

namespace CityApp.Infrastructure.PageLocators.Abstractions
{
    public interface IPageResolver : ILogTarget
    {
        IBasePage CreatePage(Type pageType);

        IViewModel CreateViewModel(Type viewModelType);
    }
}
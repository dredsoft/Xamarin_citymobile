using System;
using CityApp.Core.Pages;
using CityApp.Core.ViewModels.Abstractions;
using CityApp.Infrastructure.PageLocators.Abstractions;
using CityApp.Utilities.Logging;

namespace CityApp.Infrastructure.PageLocators.Implementations
{
    public class DefaultPageResolver : IPageResolver
    {
        public ILogger Logger => LogManager.GetLog();

        public virtual IBasePage CreatePage(Type pageType)
        {
            Logger.Trace();
            return Activator.CreateInstance(pageType) as IBasePage;
        }

        public virtual IViewModel CreateViewModel(Type viewModelType)
        {
            Logger.Trace();
            return Activator.CreateInstance(viewModelType) as IViewModel;
        }
    }
}
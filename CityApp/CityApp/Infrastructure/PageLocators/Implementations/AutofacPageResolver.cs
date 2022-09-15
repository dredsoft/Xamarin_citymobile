using System;
using Autofac;
using CityApp.Core.Pages;
using CityApp.Core.ViewModels.Abstractions;
using CityApp.Infrastructure.PageLocators.Abstractions;
using CityApp.Utilities.Logging;

namespace CityApp.Infrastructure.PageLocators.Implementations
{
    public class AutofacPageResolver : IPageResolver
    {
        private readonly IContainer _container;
        public ILogger Logger => LogManager.GetLog();

        public AutofacPageResolver(IContainer container)
        {
            _container = container;
        }
        public IBasePage CreatePage(Type pageType)
        {
            Logger.Trace(pageType.Name);
            return (IBasePage)_container.Resolve(pageType);
        }

        public IViewModel CreateViewModel(Type viewModelType)
        {
            Logger.Trace(viewModelType.Name);
            return (IViewModel)_container.Resolve(viewModelType);
        }

    }
}

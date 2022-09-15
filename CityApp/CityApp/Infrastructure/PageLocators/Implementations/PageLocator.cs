using System;
using System.Diagnostics;
using System.Reflection;
using CityApp.Core.Pages;
using CityApp.Core.ViewModels.Abstractions;
using CityApp.Infrastructure.PageLocators.Abstractions;
using CityApp.Shared.Extensions;
using CityApp.Utilities.Logging;
using Xamarin.Forms;

namespace CityApp.Infrastructure.PageLocators.Implementations
{
    public class PageLocator : IPageLocator
    {
        private readonly IPageResolver _pageResolver;

        public ILogger Logger => LogManager.GetLog();

        public PageLocator(IPageResolver pageResolver)
        {
            _pageResolver = pageResolver;
        }

        protected virtual IBasePage CreatePage(Type pageType)
        {
            return _pageResolver.CreatePage(pageType);
        }

        protected virtual IViewModel CreateViewModel(Type viewModelType)
        {
            return _pageResolver.CreateViewModel(viewModelType);
        }

        protected virtual Type FindPageTypeForViewModel(Type viewModelType)
        {
            var pageTypeName = viewModelType
                .AssemblyQualifiedName
                .Replace("ViewModel", "Page");

            var pageType = Type.GetType(pageTypeName);

            if (pageType == null)
            {
                throw new ArgumentException("Can't find a page of type '" + pageTypeName + "' for ViewModel '" +
                                            viewModelType.Name + "'");
            }

            return pageType;
        }

        public Type ResolvePageType(Type viewModelType)
        {
            return FindPageTypeForViewModel(viewModelType);
        }

        public Page ResolvePageAndViewModel(Type viewModelType, object args)
        {
            Page page = null;

            var viewModel = CreateViewModel(viewModelType);
            try
            {
                var viewModelTypeInfo = viewModelType.GetTypeInfo();

                if (viewModel is ITabbedViewModel)
                {
                    var tabbedViewModel = viewModel as ITabbedViewModel;

                    var genericViewModels = viewModelTypeInfo.BaseType.GenericTypeArguments;

                    if (genericViewModels.Length < 2)
                    {
                        throw new Exception("TabbedViewModels need at least 2 tabs (ViewModels)");
                    }

                    page = ResolveTabbedPageForViewModels(tabbedViewModel, genericViewModels, args);
                }
                else
                {
                    page = ResolvePage(viewModel);
                }

                viewModel.Init(args);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex + "Failed to initialise ViewModel " + viewModel.GetType().Name);
            }

            if (viewModel.DrawerMenuViewModelType != null)
            {
                var masterDetailPage = new MasterDetailPage
                {
                    Master = ResolvePageAndViewModel(viewModel.DrawerMenuViewModelType, null),
                    Detail = page
                };

                masterDetailPage.IsPresentedChanged += (sender, eventArgs) =>
                {
                    if (masterDetailPage.IsPresented)
                    {
                        ((IViewModel)masterDetailPage.Master.BindingContext).OnAppearing();
                    }
                };

                return masterDetailPage;
            }

            return page;
        }

        public Page ResolvePage(IViewModel viewModel)
        {
            var pageType = viewModel.PageType ?? ResolvePageType(viewModel.GetType());
            var page = CreatePage(pageType);

            if (!(page is IBasePage))
            {
                throw new ArgumentException("Page for '" + viewModel.GetType().Name +
                                                            "' should be of type 'BasePage' instead of '" +
                                                            pageType.Name + "'");
            }

            page.BindViewModel(viewModel);

            return page as Page;
        }

        private Page ResolveTabbedPageForViewModels(ITabbedViewModel tabbedViewModel, Type[] types, object args = null)
        {
            var tabbedPage = new BaseTabbedPage { BindingContext = tabbedViewModel };

            tabbedPage.BindViewModel(tabbedViewModel);

            foreach (var type in types)
            {
                var childPage = ResolvePageAndViewModel(type, args);

                if (!(childPage.BindingContext is BaseViewModel childViewModel))
                {
                    throw new Exception("Tabbed Page BindingContext must be as ViewModel type");
                }

                childViewModel.ParentViewModel = tabbedViewModel;
                tabbedViewModel.ChildViewModels.Add(type.Name, childViewModel);

                tabbedPage.Children.Add(childPage);
            }

            tabbedViewModel.SelectedPageChange += (s, e) =>
            {
                foreach (var p in tabbedPage.Children)
                {
                    if (p.BindingContext.GetType() == e.SelectedViewModelType)
                    {
                        tabbedPage.CurrentPage = p;
                        break;
                    }
                }
            };

            tabbedPage.Title = tabbedPage.Children[0].Title;
            tabbedPage.CurrentPageChanged += (sender, eventArgs) =>
            {
                tabbedPage.Title = tabbedPage.CurrentPage.Title;
                tabbedViewModel.OnSelectedTabChanged((BaseViewModel)tabbedPage.CurrentPage.BindingContext);
            };

            return tabbedPage;
        }
    }
}

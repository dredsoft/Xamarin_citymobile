using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using CityApp.Core.Pages;
using CityApp.Core.ViewModels.Abstractions;
using CityApp.Infrastructure.PageLocators.Abstractions;
using Xamarin.Forms;

namespace CityApp.Infrastructure.NavigationManager
{
    public class NavigationManager : INavigationManager
    {
        protected INavigation Navigation { get; private set; }
        protected IPageLocator PageLocator { get; }
        protected MasterDetailPage MasterDetailPage { get; private set; }

        public NavigationManager(IPageLocator pageLocator)
        {
            PageLocator = pageLocator;
        }

        public void SetMainViewModel<T>(object args = null) where T : IViewModel
        {
            var page = ResolvePageFor<T>(args);

            if (page == null)
            {
                throw new Exception("Resolve page for " + typeof(T).Name + " returned null!");
            }

            if (page is MasterDetailPage masterDetailPage)
            {

                masterDetailPage.Detail = new BaseNavigationPage(masterDetailPage.Detail);
                Navigation = masterDetailPage.Detail.Navigation;
                Application.Current.MainPage = masterDetailPage;
                MasterDetailPage = masterDetailPage;
            }
            else
            {
                var navigationPage = new BaseNavigationPage(page, false);
                Navigation = navigationPage.Navigation;
                Application.Current.MainPage = navigationPage;
            }
        }

        public void NavigateToMenuItem<T>(object args = null) where T : IViewModel
        {
            var page = ResolvePageFor<T>(args);

            if (page == null)
            {
                throw new Exception("Resolve page for " + typeof(T).Name + " returned null!");
            }

            if (MasterDetailPage == null)
            {
                return;
            }

            if (page is MasterDetailPage masterDetailPage)
            {
                MasterDetailPage.Detail = new BaseNavigationPage(masterDetailPage.Detail);
                Navigation = masterDetailPage.Detail.Navigation;
            }

            else
            {
                throw new Exception(typeof(T).Name + "page type should be BasePage");
            }
        }

        public async Task NavigateToAsync(Type baseViewModelPage, object args = null)
        {
            if (!baseViewModelPage.IsAssignableTo<IViewModel>())
                return;

            var page = ResolvePageFor(baseViewModelPage, args);

            if (page == null)
            {
                var msg = args?.GetType() + " " + args;
                Debug.WriteLine(msg);
                return;
            }

            await Navigation.PushAsync(page, false);
        }

        public async Task NavigateToAsync<T>(object args = null) where T : IViewModel
        {
            var page = ResolvePageFor<T>(args);

            if (page == null)
            {
                var msg = args?.GetType() + " " + args;
                Debug.WriteLine(msg);
                return;
            }

            await Navigation.PushAsync(page, false);
        }

        public async Task NavigateToAsync<T>(T type, object args = null) where T : IViewModel
        {
            await NavigateToAsync<T>(args);
        }

        public Task NavigateToModalAsync<T>(object args = null) where T : IViewModel
        {
            var page = ResolvePageFor<T>(args);

            return Navigation.PushModalAsync(page);
        }

        public Task PopAsync(object args = null)
        {
            if (args != null)
            {
                var navigationStack = GetNavigationStack();
                var viewModelToPop = navigationStack.Count > 1 ? navigationStack[navigationStack.Count - 2] : navigationStack.Last();
                viewModelToPop?.Init(args);
            }

            return Navigation.PopAsync();
        }

        public Task PopModalAsync(object args = null)
        {
            if (args != null)
            {
                var modalStack = GetModalStack();
                var viewModelToPop = modalStack.Count > 1 ? modalStack[modalStack.Count - 2] : modalStack.Last();
                viewModelToPop?.Init(args);
            }

            return Navigation.PopModalAsync();
        }

        public Task PopToRootAsync()
        {
            return Navigation.PopToRootAsync();
        }

        public Page ResolvePageFor<T>(object args = null) where T : IViewModel
        {
            var page = PageLocator.ResolvePageAndViewModel(typeof(T), args);
            return page;
        }
        public Page ResolvePageFor(Type pageType, object args = null)
        {
            var page = PageLocator.ResolvePageAndViewModel(pageType, args);
            return page;
        }

        public void RemoveFromNavigationStack<T>(bool removeFirstOccurenceOnly = true) where T : IViewModel
        {
            var pageType = typeof(T).IsAssignableTo<ITabbedViewModel>()
                ? typeof(BaseTabbedPage)
                : PageLocator.ResolvePageType(typeof(T));

            var navigationStack = Navigation.NavigationStack.Reverse();

            foreach (var page in navigationStack)
            {
                if (page.GetType() == pageType)
                {
                    Navigation.RemovePage(page);

                    if (removeFirstOccurenceOnly)
                    {
                        break;
                    }
                }
            }
        }

        public IReadOnlyList<IViewModel> GetNavigationStack()
        {
            return Navigation.NavigationStack.Select(page => page.BindingContext as IViewModel).ToList();
        }

        public IReadOnlyList<IViewModel> GetModalStack()
        {
            return Navigation.ModalStack.Select(page => page.BindingContext as IViewModel).ToList();
        }

        public bool IsRootPage => Navigation.NavigationStack.Count == 1;

        public IViewModel CurrentViewModel => CurrentPage?.BindingContext as IViewModel;

        public Page CurrentPage => Navigation?.NavigationStack?.LastOrDefault();

        public void OpenDrawerMenu()
        {
            PresentDrawerMenu(true);
        }

        public void CloseDrawerMenu()
        {
            PresentDrawerMenu(false);
        }

        public void ToggleDrawerMenu()
        {
            if (Application.Current.MainPage is MasterDetailPage masterDetailPage)
            {
                masterDetailPage.IsPresented = !masterDetailPage.IsPresented;
            }
        }

        private void PresentDrawerMenu(bool isPresented)
        {
            if (Application.Current.MainPage is MasterDetailPage masterDetailPage)
            {
                masterDetailPage.IsPresented = isPresented;
            }
        }
    }
}

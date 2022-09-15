using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CityApp.Core.ViewModels.Abstractions;
using Xamarin.Forms;

namespace CityApp.Infrastructure.NavigationManager
{
    public interface INavigationManager
    {
        void SetMainViewModel<T>(object args = null) where T : IViewModel;

        Task NavigateToAsync<T>(object args = null) where T : IViewModel;

        Task NavigateToAsync(Type baseViewModelPage, object args = null);      

        Task NavigateToModalAsync<T>(object args = null) where T : IViewModel;

        void NavigateToMenuItem<T>(object args = null) where T : IViewModel;

        Task PopAsync(object args = null);

        Task PopModalAsync(object args = null);

        Task PopToRootAsync();

        Page ResolvePageFor<T>(object args = null) where T : IViewModel;

        void RemoveFromNavigationStack<T>(bool removeFirstOccurenceOnly = true) where T : IViewModel;

        IReadOnlyList<IViewModel> GetNavigationStack();

        bool IsRootPage { get; }

        IViewModel CurrentViewModel { get; }

        Page CurrentPage { get; }

        void OpenDrawerMenu();

        void CloseDrawerMenu();

        void ToggleDrawerMenu();
    }
}

using System;
using System.Collections.Generic;
using CityApp.Core.ViewModels.Abstractions;
using CityApp.Infrastructure.NavigationManager;
using CityApp.Services;

namespace CityApp.Core.ViewModels.Implementations
{
    public abstract class TabbedViewModel<TViewModel1, TViewModel2> : BaseViewModel, ITabbedViewModel
          where TViewModel1 : IViewModel
          where TViewModel2 : IViewModel
    {
        protected TabbedViewModel(INavigationManager navigationManager)
            : base(navigationManager)
        {
            ChildViewModels = new Dictionary<string, BaseViewModel>();
        }

        public IDictionary<string, BaseViewModel> ChildViewModels { get; set; }

        public event EventHandler<ViewModelSelectionArgs> SelectedPageChange;

        public void SelectTab(Type viewModelType)
        {
            SelectedPageChange?.Invoke(this, new ViewModelSelectionArgs { SelectedViewModelType = viewModelType });
        }

        public virtual void OnSelectedTabChanged(BaseViewModel selectedViewModel)
        {
        }
    }

    public abstract class TabbedViewModel<TViewModel1, TViewModel2, TViewModel3> : BaseViewModel, ITabbedViewModel
        where TViewModel1 : IViewModel
        where TViewModel2 : IViewModel
        where TViewModel3 : IViewModel
    {
        public TabbedViewModel(INavigationManager navigationManager)
          : base(navigationManager)
        {
            ChildViewModels = new Dictionary<string, BaseViewModel>();
        }

        public IDictionary<string, BaseViewModel> ChildViewModels { get; set; }

        public event EventHandler<ViewModelSelectionArgs> SelectedPageChange;

        public void SelectTab(Type viewModelType)
        {
            SelectedPageChange?.Invoke(this, new ViewModelSelectionArgs { SelectedViewModelType = viewModelType });
        }

        public virtual void OnSelectedTabChanged(BaseViewModel selectedViewModel)
        {
        }
    }

    public abstract class TabbedViewModel<TViewModel1, TViewModel2, TViewModel3, TViewModel4> : BaseViewModel, ITabbedViewModel
        where TViewModel1 : IViewModel
        where TViewModel2 : IViewModel
        where TViewModel3 : IViewModel
        where TViewModel4 : IViewModel
    {
        public TabbedViewModel(INavigationManager navigationManager)
            : base(navigationManager)
        {
            ChildViewModels = new Dictionary<string, BaseViewModel>();
        }

        public IDictionary<string, BaseViewModel> ChildViewModels { get; set; }

        public event EventHandler<ViewModelSelectionArgs> SelectedPageChange;

        public void SelectTab(Type viewModelType)
        {
            SelectedPageChange?.Invoke(this, new ViewModelSelectionArgs { SelectedViewModelType = viewModelType });
        }

        public virtual void OnSelectedTabChanged(BaseViewModel selectedViewModel)
        {
        }
    }

    public abstract class TabbedViewModel<TViewModel1, TViewModel2, TViewModel3, TViewModel4, TViewModel5> : BaseViewModel, ITabbedViewModel
        where TViewModel1 : IViewModel
        where TViewModel2 : IViewModel
        where TViewModel3 : IViewModel
        where TViewModel4 : IViewModel
        where TViewModel5 : IViewModel
    {
        public TabbedViewModel(INavigationManager navigationManager)
          : base(navigationManager)
        {
            ChildViewModels = new Dictionary<string, BaseViewModel>();
        }

        public IDictionary<string, BaseViewModel> ChildViewModels { get; set; }

        public event EventHandler<ViewModelSelectionArgs> SelectedPageChange;

        public void SelectTab(Type viewModelType)
        {
            SelectedPageChange?.Invoke(this, new ViewModelSelectionArgs { SelectedViewModelType = viewModelType });
        }

        public virtual void OnSelectedTabChanged(BaseViewModel selectedViewModel)
        {
        }
    }

    public abstract class TabbedViewModel<TViewModel1, TViewModel2, TViewModel3, TViewModel4, TViewModel5, TViewModel6> : BaseViewModel, ITabbedViewModel
        where TViewModel1 : IViewModel
        where TViewModel2 : IViewModel
        where TViewModel3 : IViewModel
        where TViewModel4 : IViewModel
        where TViewModel5 : IViewModel
        where TViewModel6 : IViewModel
    {
        public TabbedViewModel(INavigationManager navigationManager)
          : base(navigationManager)
        {
            ChildViewModels = new Dictionary<string, BaseViewModel>();
        }

        public IDictionary<string, BaseViewModel> ChildViewModels { get; set; }

        public event EventHandler<ViewModelSelectionArgs> SelectedPageChange;

        public void SelectTab(Type viewModelType)
        {
            SelectedPageChange?.Invoke(this, new ViewModelSelectionArgs { SelectedViewModelType = viewModelType });
        }

        public virtual void OnSelectedTabChanged(BaseViewModel selectedViewModel)
        {
        }
    }
}

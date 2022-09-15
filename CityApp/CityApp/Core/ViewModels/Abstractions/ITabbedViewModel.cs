using System;
using System.Collections.Generic;
using CityApp.Core.ViewModels.Implementations;

namespace CityApp.Core.ViewModels.Abstractions
{
    public interface ITabbedViewModel : IViewModel
    {
        event EventHandler<ViewModelSelectionArgs> SelectedPageChange;

        IDictionary<string, BaseViewModel> ChildViewModels { get; set; }

        void SelectTab(Type viewModelType);

        void OnSelectedTabChanged(BaseViewModel selectedViewModel);
    }
}

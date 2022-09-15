using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using CityApp.Utilities.ActivityContext;
using CityApp.Utilities.Logging;
using Xamarin.Forms;

namespace CityApp.Core.ViewModels.Abstractions
{
    public interface IViewModel : INotifyPropertyChanged, IActivityProvider, ILogTarget
    {
        Task Init(object args);

        void OnAppearing();

        void OnDisappearing();

	    void PageClosing();

        string Title { get; set; }

        string Icon { get; set; }

        ObservableCollection<ToolbarItem> ToolbarItems { get; set; }

        Type PageType { get; }

        Type DrawerMenuViewModelType { get; }
    }
}

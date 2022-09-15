using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CityApp.Core.Pages
{
    public interface IBasePage
    {
        void SetBinding<TSource>(BindableProperty targetProperty,
            string path,
            BindingMode mode = 0,
            IValueConverter converter = null,
            string stringFormat = null);

        IList<ToolbarItem> ToolbarItems { get; }

        string Title { get; set; }

        FileImageSource Icon { get; set; }

        object BindingContext { get; set; }

        event EventHandler Appearing;

        event EventHandler Disappearing;

        event PageClosedEventHandler PageClosing;

        void OnPageClosing();
    }
}

using System;
using CityApp.Core.ViewModels.Abstractions;
using Xamarin.Forms;

namespace CityApp.Core.Pages
{
    public class BaseTabbedPage : TabbedPage, IBasePage
    {
        public void SetBinding<TSource>(BindableProperty targetProperty, string path, BindingMode mode = BindingMode.Default,
            IValueConverter converter = null, string stringFormat = null)
        {
            this.SetBinding(targetProperty, path, mode,
                converter, stringFormat);
        }

        public event PageClosedEventHandler PageClosing;

        public void OnPageClosing()
        {
            PageClosing?.Invoke(this, new EventArgs());
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext is ITabbedViewModel viewModel && viewModel.ToolbarItems != null && viewModel.ToolbarItems.Count > 0)
            {
                foreach (var toolBarItem in viewModel.ToolbarItems)
                {
                    if (!(ToolbarItems.Contains(toolBarItem)))
                    {
                        ToolbarItems.Add(toolBarItem);
                    }
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            OnPageClosing();
            return base.OnBackButtonPressed();
        }
    }
}

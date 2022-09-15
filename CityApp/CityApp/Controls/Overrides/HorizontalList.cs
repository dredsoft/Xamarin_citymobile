using System;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace CityApp.Controls.Overrides
{
    public class HorizontalList : ScrollView
    {
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(HorizontalList), default(IEnumerable));

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(HorizontalList), default(DataTemplate));

        public static readonly BindableProperty SelectedCommandProperty =
            BindableProperty.Create("SelectedCommand", typeof(ICommand), typeof(HorizontalList));

        public static readonly BindableProperty SelectedCommandParameterProperty =
            BindableProperty.Create("SelectedCommandParameter", typeof(object), typeof(HorizontalList));

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public event EventHandler<ItemTappedEventArgs> ItemSelected;

        public ICommand SelectedCommand
        {
            get => (ICommand)GetValue(SelectedCommandProperty);
            set => SetValue(SelectedCommandProperty, value);
        }

        public object SelectedCommandParameter
        {
            get => GetValue(SelectedCommandParameterProperty);
            set => SetValue(SelectedCommandParameterProperty, value);
        }

        public void BuildCells()
        {
            if (ItemTemplate == null || ItemsSource == null)
                return;

            var layout = new StackLayout
            {
                Orientation = Orientation == ScrollOrientation.Vertical
                    ? StackOrientation.Vertical
                    : StackOrientation.Horizontal
            };

            foreach (var item in ItemsSource)
            {
                var command = SelectedCommand ?? new Command((obj) =>
                {
                    var args = new ItemTappedEventArgs(ItemsSource, item);
                    ItemSelected?.Invoke(this, args);
                });

                var commandParameter = SelectedCommandParameter ?? item;

                if (ItemTemplate.CreateContent() is ViewCell viewCell)
                {
                    viewCell.View.BindingContext = item;
                    viewCell.View.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = command,
                        CommandParameter = commandParameter,
                        NumberOfTapsRequired = 1
                    });

                    layout.Children.Add(viewCell.View);
                }
            }

            Content = layout;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (string.Equals(propertyName, ItemsSourceProperty.PropertyName)|| string.Equals(propertyName, ItemTemplateProperty.PropertyName))
            {
                BuildCells();
            }
        }
    }
}

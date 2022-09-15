using System.Windows.Input;
using Xamarin.Forms;

namespace CityApp.Controls.Overrides.Cells
{
    public class CustomTextCell : ViewCell
    {
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(CustomTextCell));

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CustomTextCell));

        protected override void OnTapped()
        {
            if (Command != null && Command.CanExecute(CommandParameter))
                Command.Execute(CommandParameter);

            base.OnTapped();
        }
    }
}

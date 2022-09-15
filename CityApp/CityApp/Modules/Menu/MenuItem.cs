using System.Windows.Input;

namespace CityApp.Modules.Menu
{
    public class MenuItem
    {
        public MenuItem()
        {
            IsEnabled = true;
            IsVisible = true;
        }   

        public string Icon { get; set; }

        public string Title { get; set; }

        public ICommand Command { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsVisible { get; set; }

    }
}

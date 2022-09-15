using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CityApp.Shared.Extensions
{
    public static class TaskExtensions
    {
        public static void EnsureCompleted(this Task task)
        {
            task.ContinueWith(task1 =>
            {
                Debug.WriteLine(task1.Exception);
                Device.BeginInvokeOnMainThread(() => throw task1.Exception);

            }, TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}

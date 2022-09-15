using System.Collections;

namespace CityApp.Models.Extensions
{
	public static class EnumerableExtensions
	{
		#region Public Methods

		public static int IndexOf(this IEnumerable self, object obj)
		{
			var index = -1;

			var enumerator = self.GetEnumerator();
			enumerator.Reset();

			var i = 0;

			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Equals(obj))
				{
					index = i;
					break;
				}

				i++;
			}

			return index;
		}

		#endregion
	}
}

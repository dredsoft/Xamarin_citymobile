using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CityApp.Core.ViewModels.Implementations
{
	public class NotifyPropertyChangedImplementation : INotifyPropertyChanged
	{
		#region Events

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region OnPropertyChanged implementation

		protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
		{
			if (Equals(field, value))
			{
				return false;
			}

			field = value;
			SendPropertyChanged(propertyName);

			return true;
		}

		protected bool SetProperty<T>(ref T field, T value, Expression<Func<object>> propertyExpression)
		{
			if (Equals(field, value))
			{
				return false;
			}

			field = value;
			SendPropertyChanged(propertyExpression);

			return true;
		}

		protected void SendPropertyChanged(Expression<Func<object>> propertyExpression)
		{
			var lambda = propertyExpression as LambdaExpression;
			var body = lambda.Body as UnaryExpression;

			var memberExpression = body != null
				? body.Operand as MemberExpression
				: lambda.Body as MemberExpression;

			var propertyInfo = memberExpression?.Member as PropertyInfo;

			if (propertyInfo == null)
			{
				return;
			}

			var propertyName = propertyInfo.Name;

			SendPropertyChanged(propertyName);
		}

		protected void SendPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace CityApp.Models.Models.Common
{
	public class ObservableRangeCollection<T> : ObservableCollection<T>
	{
		#region Constructors

		public ObservableRangeCollection()
		{
		}

		public ObservableRangeCollection(IEnumerable<T> collection)
		  : base(collection)
		{
		}

		#endregion

		#region Public methods

		public void AddRange(IEnumerable<T> collection, NotifyCollectionChangedAction notificationMode = NotifyCollectionChangedAction.Add)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("Can't be null");
			}

			CheckReentrancy();

			if (notificationMode == NotifyCollectionChangedAction.Reset)
			{
				foreach (T obj in collection)
				{
					Items.Add(obj);
				}

				OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));

				OnPropertyChanged(new PropertyChangedEventArgs(CommonConstants.INDEXER_NAME));

				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
			}
			else
			{
				int count = Count;

				var objList = !(collection is List<T>) ? new List<T>(collection) : (List<T>)collection;

				foreach (var obj in objList)
				{
					Items.Add(obj);
				}

				OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));

				OnPropertyChanged(new PropertyChangedEventArgs(CommonConstants.INDEXER_NAME));

				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, objList, count));
			}
		}

		public void RemoveRange(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("Can't be null");
			}

			foreach (var obj in collection)
			{
				Items.Remove(obj);
			}

			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		public void Replace(T item)
		{
			ReplaceRange(new[] { item });
		}


		public void ReplaceRange(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("Can't be null");
			}

			Items.Clear();

			AddRange(collection, NotifyCollectionChangedAction.Reset);
		}

		#endregion
	}
}

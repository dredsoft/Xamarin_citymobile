using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CityApp.Controls;
using CityApp.Core.ViewModels.Abstractions;
using CityApp.Infrastructure.NavigationManager;
using CityApp.Models.Models.Base.JsonOperations;
using CityApp.Models.Models.Common;
using Plugin.MediaManager.Abstractions.Implementations;
using Xamarin.Forms;

namespace CityApp.Core.ViewModels.Implementations
{
	public abstract class ListViewModel<T> : BaseViewModel
	{
		#region Fields

		private ObservableCollection<T> _itemsList;

		private bool _isEmptyList;

		private bool _isRefreshing;

		private long _pageIndex;

		private bool _isLoadingMore;


		#endregion

		#region Constructor

		protected ListViewModel(INavigationManager navigationManager) : base(navigationManager)
		{
			ItemsList = new ObservableRangeCollection<T>();

			PageSize = ControlConstants.PAGE_STANDARD_SIZE;
		}

		#endregion

		#region Properties

		public ObservableCollection<T> ItemsList
		{
			get => _itemsList;
			set => SetProperty(ref _itemsList, value);
		}

		public bool IsEmptyList
		{
			get => _isEmptyList;
			set
			{
				if (SetProperty(ref _isEmptyList, value))
				{
					SendPropertyChanged(() => IsNotEmptyList);
				}		
			}
		}

		public bool IsNotEmptyList => !IsEmptyList;

		public bool IsRefreshing
		{
			get => _isRefreshing;
			set
			{
				if (SetProperty(ref _isRefreshing, value))
				{
					IsEmptyList = !ItemsList.Any();
				}
			}
		}

		public long TotalCount { get; set; }

		public long PageIndex
		{
			get => _pageIndex;
			set => SetProperty(ref _pageIndex, value);
		}

		public int PageSize { get; set; }

		public bool IsLoadingMore
		{
			get => _isLoadingMore;
			set => SetProperty(ref _isLoadingMore, value);
		}

		#endregion

		#region Commands

		public ICommand RefreshCommand => new Command(RefreshListExectute, () => !IsRefreshing);

		public ICommand SelectItemCommand => new Command<T>(SelectItemExecute);

		public ICommand LoadMoreCommand => new Command(LoadMoreItemsExecute, CanExecuteLoadMoreCommand);

		#endregion

		#region Public Methods

		public override void OnAppearing()
		{
			PageIndex = 1;

			LoadItemsExecute();
		}

		public bool CanExecuteLoadMoreCommand() => !IsLoadingMore
				   && ItemsList.Count != 0
				   && ItemsList.Count != TotalCount;

		#endregion

		#region Protected Methods

		protected virtual void SelectItemExecute(T obj)
		{

		}

		protected abstract void LoadItemsExecute();

		protected virtual void SetItemsListData(IJsonOperationResult<IEnumerable<T>> itemsData, long? totalCount = null)
		{
			if (totalCount == null)
			{
				totalCount = ItemsList.Count;
			}

			if (ValidateResponse(itemsData))
			{
				SetItemsListData(itemsData.Data, (long) totalCount);
			}
		}

		protected virtual void SetItemsListData(IEnumerable<T> items, long totalCount)
		{
			if (PageIndex == 1)
			{
				ItemsList.Clear();
			}

			if (items != null)
			{
				TotalCount = totalCount;

				ItemsList.AddRange(items); ;

				IsEmptyList = !ItemsList.Any();
			}
		}

		protected virtual void LoadMoreItemsExecute()
		{
			IsLoadingMore = true;
			PageIndex++;

			LoadItemsExecute();

			IsLoadingMore = false;
		}

		#endregion

		#region Private Methods

		private void RefreshListExectute()
		{
			IsRefreshing = true;

			PageIndex = 1;

			LoadItemsExecute();

			IsRefreshing = false;
		} 

		#endregion
	}
}

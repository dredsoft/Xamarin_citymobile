using System;
using Autofac;
using CityApp.Infrastructure.NavigationManager;
using CityApp.Infrastructure.Storages;
using CityApp.Infrastructure.Storages.Constants;
using CityApp.Models.Models.Account;
using CityApp.Modules.Account.Accounts;
using CityApp.Shared.Extensions;
using CityApp.Utilities.Logging;

namespace CityApp.Controls.ViewElements
{
    public partial class UserLocationView
    {
        private static readonly ILogger Logger = LogManager.GetLog();

        public UserLocationView()
        {
            InitializeComponent();
            LocationLabel.Text = SessionStorage.Instance.Get<AccountAssociationModel>(StorageConstants.ACCOUNT_ASSOCIATION_KEY).Name;
        }

        private void OnLocationBarTapped(object sender, EventArgs e)
        {
            Logger.Trace();

            var navService = App.Container.Resolve<INavigationManager>();
            navService.NavigateToAsync<AccountsListViewModel>().EnsureCompleted();
        }
    }
}
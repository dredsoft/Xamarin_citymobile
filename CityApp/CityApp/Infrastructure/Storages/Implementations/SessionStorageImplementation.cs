using System.Collections.Concurrent;
using CityApp.Infrastructure.Storages.Abstractions;
using CityApp.Infrastructure.Storages.Constants;
using CityApp.Models.Models.Authorization;
using CityApp.Models.Models.Base;
using Zebra.Sdk.Printer.Discovery;

namespace CityApp.Infrastructure.Storages.Implementations
{
    public class SessionStorageImplementation : ISessionStorage
    {
        #region Private Fields

        private readonly ConcurrentDictionary<string, object> _inMemoryStorage = new ConcurrentDictionary<string, object>();

        #endregion

        #region Properties

        public AuthorizatioUserModel UserContext
        {
            get => Get<AuthorizatioUserModel>(StorageConstants.USER_CONTEXT_KEY);
            set => Set(StorageConstants.USER_CONTEXT_KEY, value);
        }
         
        public DeviceContextModel DeviceContext
        {
            get => Get<DeviceContextModel>(StorageConstants.DEVICE_CONTEXT_KEY);
            set => Set(StorageConstants.DEVICE_CONTEXT_KEY, value);
        }

        public DiscoveredPrinter DiscoveredPrinterContext
        {
            get => Get<DiscoveredPrinter>(StorageConstants.DISCOVERED_PRINTER_CONTEXT_KEY);
            set => Set(StorageConstants.DISCOVERED_PRINTER_CONTEXT_KEY, value);
        }

        #endregion

        #region Implementation of ISessionStorage

        public void Set<T>(string key, T value)
        {
            _inMemoryStorage[key] = value;
        }

        public T Get<T>(string key)
        {
            var valueFromCache = default(T);

            if (_inMemoryStorage.ContainsKey(key))
            {
                valueFromCache = (T)_inMemoryStorage[key];
            }

            return valueFromCache;
        }

        public bool TryGet<T>(string key, out T value)
        {
            object valueToGet;

            var result = _inMemoryStorage.TryGetValue(key, out valueToGet);

            value = (T)valueToGet;

            return result;
        }

        public bool Contains(string key)
        {
            return _inMemoryStorage.ContainsKey(key);
        }

        public bool TryRemove(string key)
        {
            object value;

            return _inMemoryStorage.TryRemove(key, out value);
        }

        public void Clear()
        {
            _inMemoryStorage.Clear();
        }

        public bool TryGetAndRemove<T>(string key, out T value)
        {
            return TryGet(key, out value) && TryRemove(key);
        }

        #endregion
    }

}

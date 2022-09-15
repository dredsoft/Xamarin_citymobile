using CityApp.Infrastructure.Storages.Abstractions;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace CityApp.Infrastructure.Storages.Implementations
{
	public class CrossSettingsStorageImplementation : ISettingStorage
    {
		#region Private Fields

	    private ISettings _appSettings => CrossSettings.Current;

		#endregion

		#region Implementation of ISettingStorage

		public void AddOrUpdateValue(string key, object value)
	    {
		    var strinObject = JsonConvert.SerializeObject(value);

		    _appSettings.AddOrUpdateValue(key, strinObject);
	    }

	    public T GetValue<T>(string key)
	    {
		    var stringObject = _appSettings.GetValueOrDefault(key, string.Empty);

		    return JsonConvert.DeserializeObject<T>(stringObject);
	    }

	    public bool Contains(string key)
	    {
		    return _appSettings.Contains(key);
	    }

	    public void Remove(string key)
	    {
		    _appSettings.Remove(key);
	    }

	    public void Clear()
	    {
		    _appSettings.Clear();
	    }

		#endregion
	}
}

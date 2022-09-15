namespace CityApp.Infrastructure.Storages.Abstractions
{
	public interface ISettingStorage
	{
		void AddOrUpdateValue(string key, object value);

		T GetValue<T>(string key);

		bool Contains(string key);

		void Remove(string key);

		void Clear();
	}
}

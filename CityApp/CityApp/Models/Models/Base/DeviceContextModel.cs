using CityApp.Infrastructure.Storages;
using CityApp.Infrastructure.Storages.Constants;
using Plugin.DeviceInfo;

namespace CityApp.Models.Models.Base
{
	public class DeviceContextModel
    {
	    public string Email { get; set; }
	    public string Password { get; set; }
	    public string DeviceType => $"{CrossDeviceInfo.Current.Idiom.ToString()} {CrossDeviceInfo.Current.Platform}";
        public string DevicePublicKey => "placeholder";//SettingStorage.Instance.GetValue<string>(StorageConstants.DEVICE_PUBLIC_KEY);
	    public string DeviceName => CrossDeviceInfo.Current.Model;
	    public string DeviceToken => CrossDeviceInfo.Current.Id;
	}
}

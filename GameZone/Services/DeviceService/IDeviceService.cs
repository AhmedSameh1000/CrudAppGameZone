namespace GameZone.Services.DeviceService
{
    public interface IDeviceService
    {
        IEnumerable<SelectListItem> GetSelectListDevices();

        GameDevice GetDeviceById(int id);
    }
}
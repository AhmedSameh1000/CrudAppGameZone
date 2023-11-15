using GameZone.Data;

namespace GameZone.Services.DeviceService
{
    public class DeviceService : IDeviceService
    {
        private readonly AppDbContext _appDbContext;

        public DeviceService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public GameDevice GetDeviceById(int id)
        {
            var Device = _appDbContext.GameDevices.FirstOrDefault(c => c.DeviceId == id);
            return Device;
        }

        public IEnumerable<SelectListItem> GetSelectListDevices()
        {
            var Devices = _appDbContext.Devices.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            })
            .OrderBy(c => c.Text)
            .AsNoTracking()
            .ToList();

            return Devices;
        }
    }
}
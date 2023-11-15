using GameZone.Services.DeviceService;

namespace GameZone.Services.GameService
{
    public class GameService : IGameService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IDeviceService _deviceService;

        public GameService(AppDbContext appDbContext,
            IWebHostEnvironment webHostEnvironment,
            IDeviceService deviceService)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
            _deviceService = deviceService;
        }

        public async Task AddGame(CreateGameVM game)
        {
            var CoverName = $"{Guid.NewGuid()}.{Path.GetExtension(game.Cover.FileName)}";
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "GameImages", CoverName);

            /*
            using (var Stream = new FileStream(path, FileMode.Create))
            {
                await createGame.Cover.CopyToAsync(Stream);
                Stream.Dispose();
            }
             */

            using var Stream = File.Create(path);
            await game.Cover.CopyToAsync(Stream);

            var Game = new Game()
            {
                CategoryId = game.CategoryId,
                Name = game.Name,
                Cover = CoverName,
                Description = game.Description,
                Devices = game.SelectedDevices.Select(c => new GameDevice { DeviceId = c }).ToList(),
            };
            _appDbContext.Games.Add(Game);
            _appDbContext.SaveChanges();
        }
    }
}
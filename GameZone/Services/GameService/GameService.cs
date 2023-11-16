using GameZone.Models;
using GameZone.Services.DeviceService;
using GameZone.Settings;
using Microsoft.AspNetCore.Hosting;
using Dir = System.IO;

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

        public IEnumerable<Game> GetGames()
        {
            var Games = _appDbContext.Games
                .Include(c => c.Category)
                .Include(c => c.Devices)
                .ThenInclude(c => c.Device)
                .AsNoTracking().ToList();
            return Games;
        }

        public async Task AddGame(CreateGameVM game)
        {
            var CoverName = await SaveCover(game.Cover);
            //var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "GameImages", CoverName);

            /*
            using (var Stream = new FileStream(path, FileMode.Create))
            {
                await createGame.Cover.CopyToAsync(Stream);
                Stream.Dispose();
            }
             */

            //using var Stream = File.Create(path);
            //await game.Cover.CopyToAsync(Stream);

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

        public Game? GetGameById(int Id)
        {
            return _appDbContext.Games
                .Include(c => c.Category)
                .Include(c => c.Devices)
                .ThenInclude(c => c.Device)
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == Id);
        }

        public async Task<Game?> Update(UpdateGameVM Model)
        {
            var Game = _appDbContext
                .Games.Include(c => c.Devices)
                .SingleOrDefault(c => c.Id == Model.Id);

            if (Game is null)
                return null;
            var OldPath = Game.Cover;
            Game.Name = Model.Name;
            Game.Description = Model.Description;
            Game.CategoryId = Model.CategoryId;
            Game.Devices = Model.SelectedDevices.Select(c => new GameDevice { DeviceId = c }).ToList();

            var hasnewCover = Model.Cover is not null;
            if (hasnewCover)
            {
                Game.Cover = await SaveCover(Model.Cover!);
            }

            var RowsEfected = _appDbContext.SaveChanges();

            if (RowsEfected > 0)
            {
                if (hasnewCover)
                {
                    var Cover = Path.Combine(_webHostEnvironment.WebRootPath, "Images/GameImages", OldPath);
                    if (Path.Exists(Cover))
                    {
                        Dir.File.Delete(Cover);
                    }
                }
                return Game;
            }
            else
            {
                var Cover = Path.Combine(_webHostEnvironment.WebRootPath, "Images/GameImages", Game.Cover);
                if (Path.Exists(Cover))
                {
                    Dir.File.Delete(Cover);
                }
                return null;
            }
        }

        public async Task<string> SaveCover(IFormFile Cover)
        {
            var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(Cover.FileName)}";
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images/GameImages", CoverName);

            /*
            using (var Stream = new FileStream(path, FileMode.Create))
            {
                await createGame.Cover.CopyToAsync(Stream);
                Stream.Dispose();
            }
             */

            using var Stream = File.Create(path);
            await Cover.CopyToAsync(Stream);

            return CoverName;
        }

        public bool Delete(int Id)
        {
            var isDeleted = false;

            var GameToDelete = _appDbContext.Games.Find(Id);

            if (GameToDelete is null)
                return isDeleted;

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images/GameImages", GameToDelete.Cover);
            _appDbContext.Games.Remove(GameToDelete);
            var rowsEfected = _appDbContext.SaveChanges();

            if (rowsEfected > 0)
            {
                if (Path.Exists(path))
                {
                    isDeleted = true;
                    Dir.File.Delete(path);
                }
            }
            return isDeleted;
        }
    }
}
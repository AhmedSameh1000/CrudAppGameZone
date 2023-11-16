using GameZone.Services.DeviceService;
using GameZone.Services.GameService;
using GameZone.Services.NewFolder;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IDeviceService _deviceService;
        private readonly IGameService _gameService;

        public GamesController(
            ICategoriesService categoriesService
            , IDeviceService deviceService,
            IGameService gameService)
        {
            _categoriesService = categoriesService;
            _deviceService = deviceService;
            _gameService = gameService;
        }

        [Route("Games")]
        public IActionResult Index()
        {
            var Games = _gameService.GetGames();
            return View(Games);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var Category = new CreateGameVM()
            {
                Categories = _categoriesService.GetSelectListCategories(),
                Devices = _deviceService.GetSelectListDevices(),
            };

            return View(Category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameVM createGame)
        {
            if (!ModelState.IsValid)
            {
                createGame.Categories = _categoriesService.GetSelectListCategories();
                createGame.Devices = _deviceService.GetSelectListDevices();
                return View(createGame);
            }

            await _gameService.AddGame(createGame);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var Game = _gameService.GetGameById(id);

            if (Game is null)
                return NotFound();
            return View(Game);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var GameToUpdate = _gameService.GetGameById(id);
            if (GameToUpdate is null)
                return NotFound();

            var ViewModelToUpdate = new UpdateGameVM()
            {
                Id = id,
                Description = GameToUpdate.Description,
                CategoryId = GameToUpdate.CategoryId,
                SelectedDevices = GameToUpdate.Devices.Select(d => d.DeviceId).ToList(),
                Name = GameToUpdate.Name,
                Devices = _deviceService.GetSelectListDevices(),
                Categories = _categoriesService.GetSelectListCategories(),
                ImageUrl = GameToUpdate.Cover
            };
           
            return View(ViewModelToUpdate);
        }
    }
}
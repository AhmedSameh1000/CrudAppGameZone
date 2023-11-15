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

        [HttpGet]
        public IActionResult Index()
        {
            return View();
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
    }
}

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        
        private readonly ICategoriesService _categoriesService;
        private readonly IDevicesServices _devicesServices;
        private readonly IGameServices _gameServices;
        public GamesController( ICategoriesService categoriesService, IDevicesServices devicesServices, IGameServices gameServices)
        {
            _categoriesService = categoriesService;
            _devicesServices = devicesServices;
            _gameServices = gameServices;
        }

        public IActionResult Index()
        {
            var games = _gameServices.GetGames();
            return View(games);
        }

        public IActionResult Details(int id)
        {
            var game = _gameServices.GetGameById(id);
            if (game is null)
                return NotFound();
            return View(game);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new() {

                Categories = _categoriesService.GetSelectLists(),

                Devices = _devicesServices.GetSelectLists()

        };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid) 
            {
                model.Categories = _categoriesService.GetSelectLists();
                model.Devices = _devicesServices.GetSelectLists();  
                return View(model);
            }
            await _gameServices.create(model);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Edit(int id)  
        {
            var game = _gameServices.GetGameById(id);
            if (game is null)
                return NotFound();
            EditGameFormViewModel viewModel = new()
            {
                  id = id,
                  Name = game.Name,
                  Description = game.Description,
                  categoryId = game.categoryId,
                  SelectedDevices= game.Devices.Select(d => d.deviceID).ToList(),
                  Categories = _categoriesService.GetSelectLists(),
                  Devices = _devicesServices.GetSelectLists(),
                  CurrentCover = game.Cover,
            };
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetSelectLists();
                model.Devices = _devicesServices.GetSelectLists();
                return View(model);
            }
            var game = _gameServices.Edit(model);

            if (game is null)
                return BadRequest();

            

            return RedirectToAction(nameof(Index));
        }


        [HttpDelete]
        public IActionResult Delete(int id) 
        {
            var isDeleted = _gameServices.Delete(id);    
            return  isDeleted?  Ok() : BadRequest();
        
        }

    }
}

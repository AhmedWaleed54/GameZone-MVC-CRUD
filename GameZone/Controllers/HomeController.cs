
using System.Diagnostics;

namespace GameZone.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameServices _gameService;

        public HomeController(IGameServices gameServices)
        {
            _gameService = gameServices;
        }

        public IActionResult Index()
        {
            var games = _gameService.GetGames();    
            return View(games);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


namespace GameZone.Services
{
    public class GameServices : IGameServices
    {

        private readonly ApplicationDBContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagePath;


        public GameServices(ApplicationDBContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _imagePath = $"{_webHostEnvironment.WebRootPath}{FileSetting.ImagePath}";
        }
        public IEnumerable<Game> GetGames()
        {
            var games =_dbContext.Games.Include(g => g.category).Include(g=> g.Devices).ThenInclude(d=>d.device)
                .AsNoTracking().ToList();
            return games;
        }

        public Game? GetGameById(int id)
        {
            var games = _dbContext.Games.Include(g => g.category).Include(g => g.Devices).ThenInclude(d => d.device)
                   .AsNoTracking().SingleOrDefault(g=>g.Id ==id);
            return games;
        }
        public async Task create(CreateGameFormViewModel model)
        {
            var CoverName = await saveCover(model.Cover);
            

            Game game = new()

            {
                Name= model.Name,
                Description=model.Description,
                categoryId =model.categoryId,
                Cover = CoverName,
                Devices = model.SelectedDevices.Select(d => new GameDevice { deviceID = d}).ToList()
            };

            _dbContext.Add(game);
            _dbContext.SaveChanges();

        }

        public  async Task<Game?> Edit(EditGameFormViewModel model)
        {
            var game = _dbContext.Games.Include(g => g.Devices).SingleOrDefault(g => g.Id == model.id);
            if (game is null)
                return null;
            var hasNewCover =model.Cover is not null;
            var oldCover =game.Cover;

            game.Name = model.Name;
            game.Description = model.Description;
            game.categoryId = model.categoryId;
            game.Devices=model.SelectedDevices.Select(d => new GameDevice { deviceID = d}).ToList();

            if (hasNewCover) 
            {
                game.Cover = await saveCover(model.Cover!);
            
            }
            var EffectedRows =_dbContext.SaveChanges();
            if (EffectedRows > 0) 
            {
                if (hasNewCover) 
                {
                    var cover = Path.Combine(_imagePath, oldCover);
                    File.Delete(cover); 
                }

                return game;
            }
            else
            {
                var cover = Path.Combine(_imagePath, game.Cover);
                File.Delete(cover);
                return null;
            }

        }

        public bool Delete(int id)
        {
            var isDeleted = false;

            var game = _dbContext.Games.Find(id);

            if (game is null)
                return isDeleted;

            _dbContext.Games.Remove(game);
            var effectedRows =_dbContext.SaveChanges();
            if (effectedRows > 0) 
            {
                isDeleted = true;
                var Cover =Path.Combine(_imagePath, game.Cover);
                File.Delete(Cover);
            }
            return isDeleted;
        }
        private async Task<string> saveCover(IFormFile Cover) 
        {
            var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(Cover.FileName)}";
            var path = Path.Combine(_imagePath, CoverName);
            using var stream = File.Create(path);
            await Cover.CopyToAsync(stream);
            return CoverName;
        }

        
    }
}

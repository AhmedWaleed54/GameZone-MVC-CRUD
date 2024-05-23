namespace GameZone.Services
{
    public interface IGameServices
    {
        IEnumerable<Game> GetGames();

        Game? GetGameById(int id);
        Task create(CreateGameFormViewModel model); 
        Task<Game?> Edit(EditGameFormViewModel model);
        bool Delete(int id);
    }
}

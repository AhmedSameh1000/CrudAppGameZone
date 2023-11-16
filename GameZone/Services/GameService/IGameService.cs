namespace GameZone.Services.GameService
{
    public interface IGameService
    {
        Task AddGame(CreateGameVM createGame);

        IEnumerable<Game> GetGames();

        Game? GetGameById(int Id);

        Task<Game?> Update(UpdateGameVM Model);

        bool Delete(int Id);
    }
}
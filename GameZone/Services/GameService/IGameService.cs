namespace GameZone.Services.GameService
{
    public interface IGameService
    {
        Task AddGame(CreateGameVM createGame);

        IEnumerable<Game> GetGames();

        Game? GetGameById(int Id);
    }
}
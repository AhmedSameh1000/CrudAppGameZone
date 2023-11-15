namespace GameZone.Services.GameService
{
    public interface IGameService
    {
        Task AddGame(CreateGameVM createGame);
    }
}
using Domain.entities.GamePart.Game;
using Domain.entities.GamePart.Genre;
using Domain.entities.GamePart.Platform;


namespace Domain.IRepository.GamePart;

public interface IGameRepository
{
    #region General
    Task<List<Game>> GamesAsync();
    Task<Game?> GetGameById(int id);
    Task<List<Game>> GetRelatedGamesBtGenre(Game game);
    #endregion


    #region Admin Side
    public int GameCount();
    Task<List<Game>> AdminGames();
    Task SaveChanges();

    Task AddNewGame(Game game);
    Task DeleteScrrenshot(int screenshotId);
    void UpdateGame(Game game);

    Task DeleteGame(int id);
    #endregion

}

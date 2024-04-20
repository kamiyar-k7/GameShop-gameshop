using Data.ShopDbcontext;
using Domain.entities.GamePart.Game;
using Domain.IRepository.GamePart;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.GamePart;

public class GameRepository : IGameRepository
{
    #region Ctor
    private readonly GameShopDbContext _dbContext;
    public GameRepository(GameShopDbContext gameShopDbContext)
    {
        _dbContext = gameShopDbContext;
    }
    #endregion

    #region General
    public async Task<List<Game>> GamesAsync()
    {
        return await _dbContext.Games.Include(x => x.Screenshots).Include(x => x.gameSelectedPlatforms).Include(x => x.gemeSelectedGenres).Where(x => x.IsDelete == false && x.GamesStatus == GameStatus.Active).ToListAsync();
    }
    public async Task<List<Game>> AdminGames()
    {
        return await _dbContext.Games.Include(x => x.Screenshots).Include(x => x.gameSelectedPlatforms).Include(x => x.gemeSelectedGenres).Where(x => x.IsDelete == false).ToListAsync();
    }

    public async Task<Game?> GetGameById(int Id)
    {
        return await _dbContext.Games.Include(x => x.Screenshots).Include(x => x.gemeSelectedGenres).Include(x => x.Comments).FirstAsync(x => x.Id == Id);
    }

    public async Task<List<Game>> GetRelatedGamesBtGenre(Game game)
    {

        return await _dbContext.Games.
            Include(x => x.Screenshots).Include(x => x.gemeSelectedGenres)
            .Where(g => g.gemeSelectedGenres
             .Any(genre => genre.GenreId == game.gemeSelectedGenres.Select(g => g.GenreId).FirstOrDefault()))
             .ToListAsync();
    }


    #endregion

    //----------------------------------
    #region Admin Side
    public  async Task SaveChanges()
    {
       await  _dbContext.SaveChangesAsync();
    }

    public int GameCount()
    {
        var num = _dbContext.Games.Where(x=> x.IsDelete == false).Count();
        return num;
    }

    public async Task AddNewGame(Game game)
    {
       await _dbContext.Games.AddAsync(game);
       await SaveChanges();
    }
    public async Task DeleteScrrenshot(int screenshotId)
    {
        var screenshot = await _dbContext.Screenshots.FindAsync(screenshotId);
      
        
            _dbContext.Screenshots.Remove(screenshot);
          
        
    }

    public   void UpdateGame(Game game)
    {
         _dbContext.Games.Update(game);
       
    }

    public async Task DeleteGame(int id)
    {
        var game = await _dbContext.Games.FindAsync(id);
        game.IsDelete = true;
        game.GamesStatus = GameStatus.InActive;
        game.Quantitiy = 0;
        var deletegamefromcart = _dbContext.CartDeatails.Where(gs => gs.GameId == game.Id);
        _dbContext.CartDeatails.RemoveRange(deletegamefromcart);

        var selectedgenres = _dbContext.SelectedGenres.Where(gs => gs.GameId == game.Id);
        _dbContext.SelectedGenres.RemoveRange(selectedgenres);

        var selexctedplatforms = _dbContext.SelectedPlatforms.Where(ps=> ps.GameId == game.Id);
        _dbContext.SelectedPlatforms.RemoveRange(selexctedplatforms);

        var comments = _dbContext.Comments.Where(x=> x.GameId == game.Id);
        _dbContext.Comments.RemoveRange(comments);

        await SaveChanges();
    }
    #endregion
}

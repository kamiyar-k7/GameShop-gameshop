using Data.ShopDbcontext;
using Domain.entities.GamePart.Game;
using Domain.entities.GamePart.Genre;
using Domain.IRepository.GamePart;
using Microsoft.EntityFrameworkCore;


namespace Data.Repository.GamePart;

public class GenreRepository : IGenreRepository
{
    #region Ctor
    private readonly GameShopDbContext _gameShopDbContext;
    public GenreRepository(GameShopDbContext gameShopDbContext)
    {
        _gameShopDbContext = gameShopDbContext;
    }
    #endregion

   
    public async Task saveChanges()
    {
        await _gameShopDbContext.SaveChangesAsync();    
    }

    public async Task<List<Genre>> GetListOfGenres()
    {
        return await _gameShopDbContext.Genres.ToListAsync();

    }
    public async Task<List<Genre>> GetGenresById(int Id)
    {
        return await _gameShopDbContext.SelectedGenres.Where(x => x.GameId == Id).Select(x => x.Genre).ToListAsync();
    }
    public async Task<List<Game>> GetGamesByGenres(List<Genre> genres)
    {
        var games = await _gameShopDbContext.Games.Where(game => game.gemeSelectedGenres.Any(selectedGenre => genres.Select(g => g.Id).Contains(selectedGenre.GenreId)))
       .ToListAsync();
        return games;
    }
    public async Task AddSelectedGenres(GemeSelectedGenre gemeSelectedGenre)
    {
        await _gameShopDbContext.AddAsync(gemeSelectedGenre);
    }

    public async Task<List<GemeSelectedGenre>> GameSelectedGenre(int id)
    {
        return await _gameShopDbContext.SelectedGenres.Where(x => x.GameId == id).ToListAsync();

    }

    public void DeleteGameGenres(List<GemeSelectedGenre> gemeSelectedGenres)
    {
        _gameShopDbContext.RemoveRange(gemeSelectedGenres);
    }

    public async Task<Genre?> GetGenreById(int id)
    {
        return await _gameShopDbContext.Genres.FirstOrDefaultAsync(x => x.Id == id);
    }
    
   public async Task AddNewGenre(Genre genre)
    {
       await  _gameShopDbContext.Genres.AddAsync(genre);
        await saveChanges();
    }

    public async Task UpdateGenre(Genre genre)
    {
         _gameShopDbContext.Genres.Update(genre);
        await saveChanges();
    }

    public async Task RemoveGenre(Genre genre)
    {
         _gameShopDbContext.Genres.Remove(genre);
        var genreAssociations = _gameShopDbContext.SelectedGenres.Where(gs => gs.GenreId == genre.Id);
        _gameShopDbContext.SelectedGenres.RemoveRange(genreAssociations);
        await saveChanges();
    }
}

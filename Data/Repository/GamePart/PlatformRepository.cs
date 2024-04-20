using Data.ShopDbcontext;
using Domain.entities.GamePart.Platform;
using Domain.IRepository.GamePart;
using Microsoft.EntityFrameworkCore;


namespace Data.Repository.GamePart;

public class PlatformRepository : IPlatformRepository
{
    #region Ctor
    private readonly GameShopDbContext _gameShopDbContext;
    public PlatformRepository(GameShopDbContext gameShopDbContext)
    {
        _gameShopDbContext = gameShopDbContext;
    }
    #endregion

    #region Genreal 
    public async Task SaveChanges()
    {
        await _gameShopDbContext.SaveChangesAsync();
    }
    public async Task<List<Platform>> GetPlatforms()
    {
        return await _gameShopDbContext.Platforms.ToListAsync();
    }

    public async Task<List<Platform>> GetPlatformsById(int Id)
    {
        var plats = await _gameShopDbContext.SelectedPlatforms.Where(x => x.GameId == Id).Select(x => x.Platform).ToListAsync();
        return plats;
    }
    public async Task<Platform?> GetSelectedPlatform(int id)
    {
        return await _gameShopDbContext.SelectedPlatforms.Where(x => x.PlatformId == id).Select(x => x.Platform).FirstOrDefaultAsync();
    }

    public async Task AddselectedPlats(GameSelectedPlatform platforms)
    {
        await _gameShopDbContext.AddAsync(platforms);
    }
    public async Task<List<GameSelectedPlatform>> GameSelectedPlatforms(int id)
    {
        return await _gameShopDbContext.SelectedPlatforms.Where(x => x.GameId == id).ToListAsync();
    }


    public void DeleteGamePlatforms(List<GameSelectedPlatform> gameSelectedPlatforms)
    {
        _gameShopDbContext.RemoveRange(gameSelectedPlatforms);
    }
    #endregion

    public async Task AddNewPlatform(Platform newPlatform)
    {
        await _gameShopDbContext.Platforms.AddAsync(newPlatform);
      await  SaveChanges();
    }

    public async Task<Platform?> GetPlatformById(int id)
    {
        return await _gameShopDbContext.Platforms.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdatePlatform(Platform newPlatform)
    {
         _gameShopDbContext.Platforms.Update(newPlatform);
        await SaveChanges();
    }
    public async Task RemovePlatform(Platform platform)
    {
         _gameShopDbContext.Platforms.Remove(platform);
        var selectedplats = _gameShopDbContext.SelectedPlatforms.Where(x => x.PlatformId == platform.Id);
         _gameShopDbContext.RemoveRange(selectedplats);
        await SaveChanges();
    }
}

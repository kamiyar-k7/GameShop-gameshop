
using Data.ShopDbcontext;
using Domain.entities.WebSite;
using Domain.IRepository.HomeRepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.HomeRepsitory;

public class HomeRepository : IHomeRepository
{
    #region Ctor
    private readonly GameShopDbContext _dbContext;
    public HomeRepository(GameShopDbContext gameShopDbContext)
    {
        _dbContext = gameShopDbContext;
    }
    #endregion

    #region General

  
    public async Task<AboutUs?> AboutUs()
    {
         var a =  await _dbContext.AboutUs.FirstOrDefaultAsync();
        return a;
    }

    public async Task ContactUs(ContactUs contactUs)
    {
        await _dbContext.ContactUs.AddAsync(contactUs);
        await _dbContext.SaveChangesAsync();
    }
    #endregion

    //-------------------------------
    #region Admin Side

    #region Contact
    public async Task<List<ContactUs>> Messages()
    {
        return await _dbContext.ContactUs.OrderByDescending(x => x.DateTime).ToListAsync();
    }
    
    public async Task<bool> DeleteMessage(int id )
    {
        var messageToDelete = await _dbContext.ContactUs.FindAsync(id);
        if (messageToDelete != null)
        {
            _dbContext.ContactUs.Remove(messageToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }
    #endregion

    #region About US

    public async Task UpdateAboutUs(AboutUs aboutUs)
    {
         _dbContext.AboutUs.Update(aboutUs);
        await _dbContext.SaveChangesAsync();
    }

    #endregion

    #endregion

}


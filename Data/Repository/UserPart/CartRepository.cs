
using Data.ShopDbcontext;
using Domain.entities.Cart;
using Domain.entities.UserPart.User;
using Domain.IRepository.UserPart;
using Microsoft.EntityFrameworkCore;


namespace Data.Repository.UserPart;

public class CartRepository : ICartRepository
{
    #region Ctor
    private readonly GameShopDbContext _dbContext;

    public CartRepository(GameShopDbContext gameShop)
    {
        _dbContext = gameShop;
    }
    #endregion

    #region General

    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }
 
    public async Task AddUserCartToCarts(Carts carts)
    {
        await _dbContext.Cart.AddAsync(carts);
        await SaveChanges();

    }
    public async Task AddToCart(CartDeatails cartDeatails)
    {
        await _dbContext.CartDeatails.AddAsync(cartDeatails);
        await SaveChanges();
    }
    public async Task AddOneMoreToCart(CartDeatails cartDeatails)
    {
        _dbContext.CartDeatails.Update(cartDeatails);
        await SaveChanges();
    }
    public CartDeatails? IsGameExistInCart(int cartid, int? id, string? platform)
    {
        return _dbContext.CartDeatails.FirstOrDefault(x => x.CartId == cartid && x.Game.Id == id && x.Platform == platform);
    }

    public async Task<CartDeatails?> FindCartDetailsById(int id)
    {
        return await _dbContext.CartDeatails.FirstOrDefaultAsync(x => x.CartDetailsId == id);

    }

    public async  Task<Carts?> GetCArtByUserId(int id)
    {
        return await _dbContext.Cart.Include(x=> x.CartDeatails).Where(x=> x.UserId == id && x.IsFinally == false).AsNoTracking().AsQueryable().FirstOrDefaultAsync();
     

    }
    // my cart  & checkout 
    public async Task<List<CartDeatails>> ShowCartDetails (int id)
    {
        return  await _dbContext.CartDeatails.Include(x=> x.Game).ThenInclude(x=> x.Screenshots).Where(x => x.Cart.IsFinally == false && x.Cart.UserId == id)
            .AsNoTracking().AsQueryable().ToListAsync();
        
    } 

    // admin and finally carts
    public async Task<List<CartDeatails>> OrderDeatails(int id)
    {
        return await _dbContext.CartDeatails.Include(x => x.Game).ThenInclude(x => x.Screenshots).Where(x => x.Cart.IsFinally == true && x.Cart.CartId == id)
       .AsNoTracking().AsQueryable().ToListAsync();
    }
   

    public async Task AddLocation(Location location)
    {
        await _dbContext.Locations.AddAsync(location);
        _dbContext.SaveChanges();
    }

    public void DeleteCart(CartDeatails cartDeatails)
    {
        _dbContext.CartDeatails.Remove(cartDeatails);
    }

    public async Task FinalOrder(Carts cart)
    {
         _dbContext.Update(cart);
      
    }
   
    public async Task<Location?> LocationAsync(int orderir) 
    {
        var l = await _dbContext.Locations.Where(x => x.CartId == orderir).FirstOrDefaultAsync();
        return l;
    }
    #endregion

    #region Admin
    public  int CountOfOrders()
    {
        return  _dbContext.Cart.Where(x=> x.IsFinally == true).Count();
    }
    public async Task<List<Carts>> GetListOfORders()
    {
        return await _dbContext.Cart.Where(x=> x.IsFinally == true).OrderByDescending(x=> x.RegestredDate).ToListAsync();
    }
  
    public async Task<Carts?> GetOrderById(int id)
    {
        return await _dbContext.Cart.Include(x => x.Location).Where(x => x.CartId == id).FirstOrDefaultAsync();
    }

    public async Task UpdateOrderStatus(Carts carts)
    {
         _dbContext.Update(carts);
        await SaveChanges();
    }
    #endregion



}

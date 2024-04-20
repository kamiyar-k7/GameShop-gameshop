using Domain.entities.Cart;



namespace Domain.IRepository.UserPart;

public interface ICartRepository
{
    Task SaveChanges();
    Task AddToCart(CartDeatails cartDeatails);
    Task AddUserCartToCarts(Carts carts);

    Task<CartDeatails?> FindCartDetailsById(int id);
    Task<Carts?> GetCArtByUserId(int id);
    Task<List<CartDeatails>> ShowCartDetails(int id);
    void DeleteCart(CartDeatails cartDeatails);
     Task AddLocation(Location location);
    CartDeatails? IsGameExistInCart(int cartid, int? id, string? platform);
    Task AddOneMoreToCart(CartDeatails cartDeatails);
    Task FinalOrder(Carts cart);
    Task<Location?> LocationAsync(int orderir);
    Task<List<CartDeatails>> OrderDeatails(int id);
    #region Admin 
    int CountOfOrders();
    Task<List<Carts>> GetListOfORders();
   
     Task<Carts?> GetOrderById(int id);
    Task UpdateOrderStatus(Carts carts);
    #endregion
}

using Application.DTOs.UserSide.Account;
using Application.ViewModel.UserSide;

namespace Application.Services.Interfaces.UserSide;

public interface ICartService
{

    Task<List<CartDto>> ShowProductIncart(int userid);
    Task AddToCart(ProductViewModel model, int userid);
    Task<bool> DeleteCartProduct(int id);
    Task<CheckOutViewModel> ShowCheckOut(int user);

    Task<bool> SubmitOrder(CheckOutViewModel model, int userid);
    Task<CheckOutViewModel> GetOrderDetails(int orderid);
}

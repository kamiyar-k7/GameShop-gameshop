

using Application.ViewModel.AdminSide;
using Domain.entities.Cart;

namespace Application.Services.Interfaces.AdminSide;

public interface IOrdersService
{
    Task<AdminOrdersViewModel> GetListOFOrders(int adminid);

    Task<AdminOrdersViewModel> GetDetailOfOrder(int orderid, int adminid);
    //Task UpdateOrderStatus(int orderid, OrderStatus status);
    Task UpdateOrderStatus(OrdersViewModel model);
}

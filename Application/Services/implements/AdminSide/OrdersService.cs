

using Application.Services.Interfaces.AdminSide;
using Application.ViewModel.AdminSide;
using Domain.entities.Cart;
using Domain.IRepository.UserPart;

namespace Application.Services.implements.AdminSide;

public class OrdersService : IOrdersService
{
    #region ctor
    private readonly ICartRepository _cart;
    private readonly ILayoutService _layoutService;
    public OrdersService(ICartRepository cart , ILayoutService layoutService)
    {
            _cart = cart;
        _layoutService = layoutService; 
    }
    #endregion

  public async Task<AdminOrdersViewModel> GetListOFOrders(int adminid)
   {
      var orders = await _cart.GetListOfORders();

     List<OrdersViewModel> model = new List<OrdersViewModel>();

        foreach (var order in orders)
        {
            OrdersViewModel childModel = new OrdersViewModel()
            {
                CartId = order.CartId,
                IsFinally = order.IsFinally,
                Price = order.Price,
                Status = (OrderStatus)order.Status ,
                UserId = order.UserId ,
                
            };

            model.Add(childModel);  


        }

        var admininfo = await _layoutService.AdminInfo(adminid);

        AdminOrdersViewModel adminOrdersView = new AdminOrdersViewModel()
        {
            Admin = admininfo,
           ListOfOrders = model
        };

        return adminOrdersView;


   }

   public async Task<AdminOrdersViewModel> GetDetailOfOrder(int orderid , int adminid)
    {
        var OrderItems = await _cart.OrderDeatails(orderid);
        var order = await _cart.GetOrderById(orderid);
        List<CartDeatailsViewModel> detailsmodel = new List<CartDeatailsViewModel>();

        foreach (var item in OrderItems)
        {
            CartDeatailsViewModel childmodel = new CartDeatailsViewModel()
            {
                CartDetailsId = item.CartDetailsId,
                GameId = item.GameId,
                GameName = item.Game.Name,
                ItemImage = item.Game.Screenshots.First().AvararUrl,
                Platform = item.Platform,
                Price = item.Price,
                Quantity = item.Quantity,
               
              
            };
            detailsmodel.Add(childmodel);
        }

        var loc = order.Location;

        AdminLocationViewModel locationmodel = new AdminLocationViewModel()
        {
            FirstName = loc.FirstName,
            LastName = loc.LastName,
            Address = loc.Address,
            City = loc.City,
            Email = loc.Email,
            OrderNote = loc.OrderNote   ,
            PhoneNumber = loc.PhoneNumber,
            PosstCode = loc.PostCode
        };

        OrdersViewModel cartid = new OrdersViewModel()
        {
            CartId = orderid,
            TrackingCode = order.TrackingPostCode,
        };

        var admin = await _layoutService.AdminInfo(adminid);
        AdminOrdersViewModel model = new AdminOrdersViewModel()
        {
            Admin = admin,
            cartDeatails = detailsmodel
            , OneOrder = cartid ,
            Location = locationmodel
            
        };



        return model;

    }
    

    public async Task UpdateOrderStatus(OrdersViewModel model)
    {
        var order = await _cart.GetOrderById(model.CartId);
        order.Status = model.Status;
        order.TrackingPostCode = model.TrackingCode;
        order.RegestredDate = DateTime.Now;
        await _cart.UpdateOrderStatus(order);
    }
}

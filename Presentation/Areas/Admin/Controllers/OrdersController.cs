
using Application.Helpers;
using Application.Services.Interfaces.AdminSide;
using Application.ViewModel.AdminSide;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers;

public class OrdersController : BaseController
{

    #region Ctor

    private readonly IOrdersService _Order;
    public OrdersController(IOrdersService ordersService)
    {
        _Order = ordersService;
    }
    #endregion

    public async Task<IActionResult> ListOfOrders()
    {
        var adminid = (int)HttpContext.User.GetUserId();
       var model = await _Order.GetListOFOrders(adminid);
        return View(model);
    }

    public async Task<IActionResult> OrderDetails(int id)
    {
        var model = await _Order.GetDetailOfOrder(id , (int)HttpContext.User.GetUserId());
        return View(model);
    }

    [HttpPost , ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeStatus(AdminOrdersViewModel ordermoel)
    {
   
       var model =  ordermoel.OneOrder;
            
        await _Order.UpdateOrderStatus(model );
        return RedirectToAction("ListOfOrders", "Orders" );
    }
    
}

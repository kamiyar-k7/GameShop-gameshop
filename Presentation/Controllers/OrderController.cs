using Application.Helpers;
using Application.Services.Interfaces.UserSide;
using Application.ViewModel.UserSide;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Authorize]
public class OrderController : Controller
{
    #region Ctor
    private readonly ICartService _cartService;
    public OrderController(ICartService cartService)
    {
           _cartService = cartService;
    }
    #endregion

    #region My Cart
     
    public async Task<IActionResult> MyCart()
    {
    
        var cart = await _cartService.ShowProductIncart((int)HttpContext.User.GetUserId());
        if(cart != null)
        {

            return View(cart);
        }
        return View();
        
    }
    #endregion

    #region Add Cart
     
    [HttpPost]
    public async Task<IActionResult> AddToCart(ProductViewModel model)
    {

            await _cartService.AddToCart(model, (int)HttpContext.User.GetUserId());
      
        return RedirectToAction("Product", "Store", new { id = model.Game.Id });



    }
    #endregion

    #region CartDetails 

    public async Task<IActionResult> CartDetails(int id)
    {
        var model = await _cartService.GetOrderDetails(id);
        if (model != null)
        {
            return View(model);
        }
        return View();
    }
    #endregion

    #region Delete Cart


    public async Task<IActionResult> DeleteCart(int Id)
    {

        var del = await _cartService.DeleteCartProduct(Id);
        if (del)
        {
            return RedirectToAction(nameof(MyCart));
        }
        TempData["error"] = "error";
        return RedirectToAction(nameof(MyCart)); ;
    }
    #endregion

    #region CheckOut
     
    [HttpGet]
    public async Task<IActionResult> CheckOut()
    {
        
            var model = await _cartService.ShowCheckOut((int)HttpContext.User.GetUserId());
            return View(model);
        

    }
     
    [HttpPost , ValidateAntiForgeryToken]
    public async Task<IActionResult> CheckOut(CheckOutViewModel viewModel)
    {
      var check =  await _cartService.SubmitOrder(viewModel , (int)HttpContext.User.GetUserId());
        
        if(check == true)
        {
            return RedirectToAction("index", "home");
        }
        else
        {
            TempData["error"] = "error";
            return RedirectToAction("index", "home");
           
        }

    }
    #endregion

    
}

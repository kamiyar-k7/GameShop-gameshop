using Application.Helpers;
using Application.Services.Interfaces.AdminSide;
using Application.ViewModel.AdminSide;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers;

public class HomeController : BaseController
{

    #region Ctor
   
     private readonly IAdminHomeService _homeservices;
    private readonly ILayoutService _layoutService;
    public HomeController(IAdminHomeService dashbord , ILayoutService layoutService)
    {
        _homeservices = dashbord;
         _layoutService = layoutService;
     
    }
    #endregion



    public async Task<IActionResult> Index()   
	{



        var model = await _homeservices.HomeAdminVeiwModel((int)HttpContext.User.GetUserId());
            

        ViewData["Title"] = "Dashboard";

        return View(model);

	}


    public async Task<IActionResult> ContactUs()
    {

        var model = await _homeservices.ContactService((int)HttpContext.User.GetUserId());
        

        ViewData["Title"] = "Contact Page";

        return View(model);
    }

    public async Task< IActionResult> DeleteMessage(int id)
    {
        var res = await _homeservices.DeleteMessage(id);
        if(res)
        {
            return RedirectToAction(nameof(ContactUs));
        }
        ViewData["ErrorMessage"] = "something Wrong Please Try Agin Later";
        return RedirectToAction(nameof(ContactUs));
    }



    [HttpGet]
    public async Task<IActionResult> AboutUs()
    {
        
       
        var model = await _homeservices.FillAdminAboutUs((int)HttpContext.User.GetUserId());

        
        ViewData["Title"] = "ABOUT us";

        return View(model);
    }

    [HttpPost , ValidateAntiForgeryToken]
    public async Task<IActionResult> AboutUs(AdminHomeViewModel model)
    {

        await _homeservices.EditAboutUs(model.aboutUs);
      


        ViewData["Title"] = "ABOUT us";

        return RedirectToAction(nameof(AboutUs));
    }

}

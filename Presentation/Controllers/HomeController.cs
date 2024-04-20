using Application.Services.Interfaces.UserSide;
using Application.ViewModel.UserSide;
using Microsoft.AspNetCore.Mvc;


namespace Presentation.Controllers;


public class HomeController : Controller
{
    #region Ctor

	private readonly IStoreService _storeService;
	private readonly IHomeService _homeService;

    public HomeController(  IStoreService storeService , IHomeService homeService )
    {
           _homeService = homeService;
		_storeService = storeService;
		
    }
	#endregion

	#region Index
	public async Task<IActionResult> Index(int pageId = 1)
	{
        ViewBag.PageId = pageId;

        var games = await _storeService.ShowGames();
        var pagecount = (int)Math.Ceiling(games.Count() / 10.0); 

        ViewBag.pagecount = pagecount;

     
        var paginatedGames = games.OrderByDescending(x => x.ReleaseDate).Skip((pageId - 1) * 10 ).Take(10).ToList();

      
        return View(paginatedGames);

    }

	#endregion


	#region AboutUs  / contact us
	[HttpGet]
    public async Task<IActionResult> AboutUs()
    {
        var about = await _homeService.ShowAbout();
        if (about != null)
        {
            return View(about);

        }
		return View();
       
    }

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> AboutUs(AboutPageViewModel model)
	{
    
        if (ModelState.IsValid)
		{
			var result = await _homeService.AddMessage(model.ContactUsViewModel);
			if(result)
			{
				 ViewBag.SuccessMessage = "Message sent successfully.";
                return	RedirectToAction(nameof(AboutUs));

			}
            TempData["Error"] = "not done ";
           return RedirectToAction(nameof(AboutUs));
        }
        TempData["Error"] = "fill the fields  ";
       return RedirectToAction(nameof(AboutUs));
		
    }
    #endregion
}

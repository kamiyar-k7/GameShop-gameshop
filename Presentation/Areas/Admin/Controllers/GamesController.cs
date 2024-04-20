using Application.Helpers;
using Application.Services.implements.SitteSide;
using Application.Services.Interfaces.AdminSide;
using Application.Services.Interfaces.UserSide;
using Application.ViewModel.AdminSide;
using Application.ViewModel.UserSide;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers;


public class GamesController : BaseController
{
    #region Ctor

    private readonly IProductService _productService;
    private readonly IplatfromsService _platfromservice;
    private readonly IgenreService _genreService;

    public GamesController(IProductService productService , IplatfromsService iplatfromsService , IgenreService genreService)
    {
        _productService = productService;
        _platfromservice = iplatfromsService;
        _genreService = genreService;
    }

    #endregion

    //  models get admin id for layout Service
    public async Task<IActionResult> ListOfGames(int pageId = 1 )
    {
        ViewData["Title"] = "List OF Games";
        ViewBag.PageId = pageId;

        var games = await  _productService.ListOfProducts((int)HttpContext.User.GetUserId());
        var pagecount = (int)Math.Ceiling(games.ListGames.Count() / 10.0);

        ViewBag.pagecount = pagecount;

        
        var paginatedGames = games.ListGames.OrderByDescending(x => x.ReleaseDate).Skip((pageId - 1) * 10).Take(10).ToList();



        var model = await _productService.ListOfProducts((int)HttpContext.User.GetUserId());
        model.ListGames = paginatedGames;
        return View(model);

    }

    public async Task<IActionResult> GameDetails(int id)
    {

        var model = await _productService.GetProductById(id, (int)HttpContext.User.GetUserId());
        ViewData["Title"] = $"{model.Game.Name} Details";
        return View(model);
    }

    #region Addgame 
    [HttpGet]
    public async Task<IActionResult> AddGame()
    {

        var adminid = (int)HttpContext.User.GetUserId();
        var model = await _productService.ShowAddGame(adminid);

        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddGame(ProductViewModel model, List<int> selectedGenres, List<int> selectedPlatforms)
    {

        var res = await _productService.AddNewGame(model.Game, selectedGenres, selectedPlatforms);
        if (res)
        {
            return RedirectToAction(nameof(ListOfGames));

        }

        return RedirectToAction(nameof(AddGame));



    }
    #endregion

    #region Edit game 
    [HttpGet]
    public async Task<IActionResult> EditGame(int id)
    {

        if (id != 0 && id != null)
        {
            var game = await _productService.GetProductById(id, (int)HttpContext.User.GetUserId());
            ViewData["title"] = $"Editing {game.Game.Name}";
            return View(game);

        }
        ViewData["NullId"] = "ID Is Incoorect";
        return RedirectToAction(nameof(ListOfGames));


    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditGame(ProductViewModel model, List<int> selectedGenres, List<int> selectedPlatforms)
    {
        await _productService.EditGame(model.Game, selectedGenres, selectedPlatforms);

        return RedirectToAction("GameDetails", "Games", new { id = model.Game.Id });
    }
    #endregion

    public async Task<IActionResult> DeleteGame(int id)
    {
        await _productService.DeleteGame(id);
        return RedirectToAction(nameof(ListOfGames));
    }

    // not finished 
    public async Task<IActionResult> DeleteScreenShot(int id)
    {
        return View();
    }

    #region Platforms

    public async Task<IActionResult> ListOfPlatforms(int pageId = 1)
    {
        ViewData["Title"] = "List OF Platforms";
        ViewBag.PageId = pageId;

        var plats = await _platfromservice.ListOfPLatforms((int)HttpContext.User.GetUserId());
        var pagecount = (int)Math.Ceiling(plats.ListOfPlats.Count() / 10.0);

        ViewBag.pagecount = pagecount;

        var Pagedplats = plats.ListOfPlats.OrderByDescending(x => x.Name).Skip((pageId - 1) * 10).Take(10).ToList();

        var model = await _platfromservice.ListOfPLatforms((int)HttpContext.User.GetUserId());
        model.ListOfPlats = Pagedplats;

            return View(model);
        
    }

    [HttpGet]
    public async Task<IActionResult> AddNewPlatform()
    {
       var model = await _platfromservice.ShowAddNewPalform((int)HttpContext.User.GetUserId());
        return View(model);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddNewPlatform(AdminPlatformsViewModel model)
    {
        await _platfromservice.AddNewPlatform(model.Platform);

        return RedirectToAction(nameof(ListOfPlatforms));
    }


    [HttpGet]
    public async Task<IActionResult> EditPlatform(int id)
    {
        var model = await _platfromservice.GetPlatformById(id , (int)HttpContext.User.GetUserId());
        return View(model);

    }

    [HttpPost ,ValidateAntiForgeryToken]
    public async Task<IActionResult> EditPlatform(AdminPlatformsViewModel model )
    {
        await _platfromservice.UpdatePlatform(model.Platform);
        return RedirectToAction(nameof(ListOfPlatforms));
    }

    public async Task<IActionResult> RemovePlatform(int id )
    {
        await _platfromservice.RemovePlatfrom(id);

        return RedirectToAction(nameof(ListOfPlatforms));
    }
    #endregion

    #region Genres

    public async Task<IActionResult> ListOfGenres(int pageId = 1)
    {
        ViewBag.PageId = pageId;

        var genres = await _genreService.ListOfGenres((int)HttpContext.User.GetUserId());
        var pagecount = (int)Math.Ceiling(genres.ListOfGenres.Count() / 10.0);

        ViewBag.pagecount = pagecount;


        var Pagedgenres = genres.ListOfGenres.OrderByDescending(x => x.Name).Skip((pageId - 1) * 10).Take(10).ToList();



        var model = await _genreService.ListOfGenres((int)HttpContext.User.GetUserId());
        model.ListOfGenres = Pagedgenres;
        return View(model);
    }

    // addd genre
    [HttpGet]
    public async Task<IActionResult> AddNewGenre()
    {
        var model = await _genreService.ShowAddGenreView((int)HttpContext.User.GetUserId());
        return View(model);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddNewGenre(AdminGenresViewModel model)
    {
        await _genreService.AddNewGenre(model.genre);
        return RedirectToAction(nameof(ListOfGenres));


    }

    // edit genre
    [HttpGet]
    public async Task<IActionResult> EditGenre(int id)
    {
        var model = await _genreService.GetGenreById(id , (int)HttpContext.User.GetUserId());
        return View(model);
    }
    [HttpPost , ValidateAntiForgeryToken]
    public async Task<IActionResult> EditGenre(AdminGenresViewModel model)
    {
        await _genreService.EditGenre(model.genre);
        return RedirectToAction(nameof(ListOfGenres));
    }


    // remove genre 
    public async Task<IActionResult> RemoveGenre(int id)
    {
        await _genreService.RemoveGenre(id);
        return RedirectToAction(nameof(ListOfGenres));
    }
    #endregion

    #region Comments
    [HttpGet]
    public async Task<IActionResult> AllComments()
    {
        var model = await _productService.GetAllCommments((int)HttpContext.User.GetUserId());

        return View(model);
    }

    public async Task<IActionResult> DeleteComment(int Id)
    {
        await _productService.DeleteComment(Id);
        return RedirectToAction(nameof(AllComments));
    }
    #endregion



}

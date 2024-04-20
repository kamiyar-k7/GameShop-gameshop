

using Application.Services.Interfaces.AdminSide;
using Application.ViewModel.AdminSide;
using Domain.IRepository.GamePart;
using Domain.IRepository.HomeRepositoryInterface;
using Domain.IRepository.UserPart;

namespace Application.Services.implements.AdminSide;

public class AdminHomeService : IAdminHomeService
{

    #region Ctor
    private readonly IGameRepository _gameRepository;
    private readonly IAccountRepository _account;
    private readonly IHomeRepository _home;
    private readonly ILayoutService _layoutService;
    private readonly ICartRepository _cartRepository;
    public AdminHomeService(IGameRepository gameRepository ,
                            IAccountRepository accountRepository,
                            IHomeRepository homeRepository , 
                            ILayoutService layoutService,
                            ICartRepository cartRepository)
    {
        
        _gameRepository = gameRepository;
        _account = accountRepository;
        _home = homeRepository;
        _layoutService = layoutService;
        _cartRepository = cartRepository;

    }
    #endregion



    #region Dashboard

    public  CountsViewModel DashboardView()
    {

        var gamecount = _gameRepository.GameCount();
        var users = _account.CountUsers();
        var admins = _account.CountAdmins();
        var orders =  _cartRepository.CountOfOrders();
        CountsViewModel countsViewModel = new CountsViewModel()
        {
            GameCount = gamecount,
            AdminCount = admins,
            UserCount = users,
            OrdersCount = orders
        };


        return countsViewModel;
    }

    public async Task<AdminHomeViewModel> HomeAdminVeiwModel(int id)
    {
        var counts = DashboardView();

        AdminHomeViewModel model = new AdminHomeViewModel()
        {
            Admin = await _layoutService.AdminInfo(id),
            counts = counts
        };
        return model;
    }
    #endregion



    #region Contact 

    public async Task<List<ContactMessagesViewModel>> Messages()
    {
        var messages = await _home.Messages();
        List<ContactMessagesViewModel> model = new List<ContactMessagesViewModel>();
        if (messages != null)
        {
            foreach (var message in messages)
            {
                ContactMessagesViewModel chiledmodel = new ContactMessagesViewModel()
                {
                    Id = message.Id,
                    UserName = message.Name,
                    Email = message.Email,
                    Title = message.Title,
                    Message = message.Message,
                    Time = message.DateTime,
                };
                model.Add(chiledmodel);
            }
            return model;


        }
        return null;
    }

    public async Task<AdminHomeViewModel> ContactService(int id)
    {
        AdminHomeViewModel model = new AdminHomeViewModel()
        {
            Admin  = await _layoutService.AdminInfo(id),
            contactMessages = await Messages()
        };
        return model;
    }

    public async Task<bool> DeleteMessage(int id)
    {
        var result = await _home.DeleteMessage(id);
        if (result) { return true; }
        return false;
    }
    #endregion

    #region About US

    public async Task<AdminHomeViewModel> FillAdminAboutUs(int adminid)
    {
        var admininfo = await _layoutService.AdminInfo(adminid);

        var about = await _home.AboutUs();

        AdminAboutUsViewModel a = new AdminAboutUsViewModel()
        {
            Title = about.Title,
            Author = about.Author,
            AuthorUrl = about.AuthorUrl,
            Description = about.Description,
        };

        AdminHomeViewModel model = new AdminHomeViewModel()
        {
            Admin = admininfo,
            aboutUs = a,
        };

        return model;
    }

    public async Task EditAboutUs(AdminAboutUsViewModel model)
    {
        var aboutinfo = await _home.AboutUs();

        aboutinfo.Title = model.Title;
        aboutinfo.Author = model.Author;
        aboutinfo.AuthorUrl = model.AuthorUrl;
        aboutinfo.Description = model.Description;

        await _home.UpdateAboutUs(aboutinfo);
    }
    #endregion

}

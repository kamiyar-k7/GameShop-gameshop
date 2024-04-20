

using Application.Services.Interfaces.AdminSide;
using Application.ViewModel.AdminSide;
using Domain.IRepository.UserPart;

namespace Application.Services.implements.AdminSide;

public class AdminService  : IAdminService
{

    #region Ctor

    private readonly IAccountRepository _account;
    public readonly ILayoutService _layoutService;
    public AdminService(IAccountRepository accountRepository,
                        ILayoutService layoutService)
    {
            _account = accountRepository;
        _layoutService = layoutService;
    }
    #endregion


    public async Task<List<ListOfAdmins>> AdminsList()
    {
        var admins = await _account.ListOfAdmins();

        List<ListOfAdmins> model = new List<ListOfAdmins>();

        foreach (var admin in admins)
        {
            ListOfAdmins childmodel = new ListOfAdmins()
            {
                Id = admin.Id,
                Email = admin.Email,
                UserName = admin.UserName,
                Created = admin.Created,
                ScreenShot = admin.UserAvatar,
            };
            model.Add(childmodel);
        }
        return model;

    }

    public async Task<AdminsViewModel> AdminsViewModel(int id)
    {
        var admins = await AdminsList();
        var admin = await _layoutService.AdminInfo(id);

        AdminsViewModel model = new AdminsViewModel()
        {
            Admins = admins,
            Admin = admin
        };

        return model;
    }
}

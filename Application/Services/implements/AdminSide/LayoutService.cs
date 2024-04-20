

using Application.Services.Interfaces.AdminSide;
using Application.Services.Interfaces.UserSide;
using Application.ViewModel.AdminSide;
using Domain.IRepository.UserPart;

namespace Application.Services.implements.AdminSide;

public class LayoutService : ILayoutService
{
	#region Ctor
	private readonly IAccountRepository _account;
 
    public LayoutService(IAccountRepository accountService)
    {
            
         _account = accountService;
    }
    #endregion

    public async Task<AdminInformationViewModel> AdminInfoView(int id)
    {
        var admin = await _account.GetUserByIdAsync(id);

        AdminInformationViewModel adminview = new AdminInformationViewModel()
        {
            DateTime = admin.Created,
            Email = admin.Email,
            Id = id,
            PhoneNumber = admin.PhoneNumber,
            UserAvatar = admin.UserAvatar,
            UserName = admin.UserName,

        };

        return adminview;
    }

    public async Task<AdminInformationViewModel> AdminInfo(int id)
    {
        var admin = await AdminInfoView(id);

        AdminInformationViewModel adminmodel = new AdminInformationViewModel()
        {
            Id = admin.Id,
            DateTime = admin.DateTime,
            Email = admin.Email,
            PhoneNumber = admin.PhoneNumber,
            UserAvatar = admin.UserAvatar,
            UserName = admin.UserName,
        };


        return adminmodel; 
    } 
}

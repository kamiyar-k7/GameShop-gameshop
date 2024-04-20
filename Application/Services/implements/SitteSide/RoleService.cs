using Application.Services.Interfaces.UserSide;
using Domain.entities.UserPart.Roles;
using Domain.IRepository.UserPart;

namespace Application.Services.implements.SitteSide;

public class RoleService : IRoleService
{
    #region Ctor
    private readonly IRoleRepository _Roles;
    private readonly IAccountRepository _Account;
    public RoleService(IRoleRepository roleRepository, IAccountRepository accountRepository)
    {
        _Roles = roleRepository;
        _Account = accountRepository;
    }

    #endregion

    public async Task<List<Role>> GetUserRoles(int Id)
    {
        var roles = await _Roles.GetUserRolesById(Id);
        return roles;

    }

    public async Task<bool> IsUserAdmin(int id)
    {
        var user = _Account.SuperAdmin(id);
        if (user == true)
        {
            return true;
        }

        var userRoles = await GetUserRoles(id);


        foreach (var roles in userRoles)
        {
            if (roles.RoleUniqueName == "Admin")
            {
                return true;
            }

        }

        return false;
    }
}

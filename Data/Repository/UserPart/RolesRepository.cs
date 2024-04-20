using Data.ShopDbcontext;
using Domain.entities.UserPart.Roles;
using Domain.IRepository.UserPart;
using Microsoft.EntityFrameworkCore;


namespace Data.Repository.UserPart;


public class RolesRepository : IRoleRepository
{
    #region Ctor
    private readonly GameShopDbContext _DbContext;
    public RolesRepository(GameShopDbContext gameShopDb)
    {
        _DbContext = gameShopDb;
    }
    #endregion

    public List<Role> GetRoles()
    {
        return _DbContext.Roles.ToList();
    }

    public async Task<List<Role>> GetUserRolesById(int Id)
    {
        return await _DbContext.SelectedRole.Where(x => x.UserId == Id).Select(x => x.Role).ToListAsync();

    }
    public List<UserSelectedRole> listOfUserSelectedRoles(int userid)
    {
        return _DbContext.SelectedRole.Where(x => x.UserId == userid).ToList();
    }

    public void DeleteSelectedroles(List<UserSelectedRole> userSelectedRoles)
    {
        _DbContext.SelectedRole.RemoveRange(userSelectedRoles);
    }
    public void AddUserSelectedrole(UserSelectedRole userSelectedRole)
    {
        _DbContext.SelectedRole.AddAsync(userSelectedRole);
    }
    public void SaveChanges()
    {
        _DbContext.SaveChanges();
    }


}


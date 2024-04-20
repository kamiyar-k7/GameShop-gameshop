using Domain.entities.UserPart.Roles;

namespace Domain.IRepository.UserPart;

public interface IRoleRepository
{
    Task<List<Role>> GetUserRolesById(int Id);
    List<Role> GetRoles();
    void AddUserSelectedrole(UserSelectedRole userSelectedRole);
    List<UserSelectedRole> listOfUserSelectedRoles(int userid);

    void DeleteSelectedroles(List<UserSelectedRole> userSelectedRoles);

    void SaveChanges();

}

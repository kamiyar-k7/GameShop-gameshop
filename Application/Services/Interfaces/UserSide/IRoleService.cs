using Domain.entities.UserPart.Roles;

namespace Application.Services.Interfaces.UserSide;

public interface IRoleService
{
	Task<List<Role>> GetUserRoles(int Id);

	Task<bool> IsUserAdmin(int id);
}

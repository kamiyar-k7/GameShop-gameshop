

namespace Domain.entities.UserPart.Roles;

public class Role
{
    public int Id { get; set; }

    public string RoleTitle { get; set; }

    public string RoleUniqueName { get; set; }


    public bool IsDelete { get; set; }

    #region Rels
    public ICollection<UserSelectedRole> UserSelectedRoles { get; set; }
    #endregion

}



using Microsoft.AspNetCore.Http;

namespace Application.ViewModel.AdminSide;

public record UserDetailViewModel : AdminBaseViewModel
{
    public OneUserViewModel User { get; set; }
    public List<UserRolesVeiwModel> SelectedRoles { get; set; }
    public List<AllRolesVeiwModel > AllRoles { get; set; }
    public AdminSideUserOrdersviewModel  Orders { get; set; }
  public  AdminSideUserCommentViewModel Comments { get; set; }

    public List<int> UserRoles { get; set; }
}

public record OneUserViewModel
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string UserAvatar { get; set; }
    public DateTime Created { get; set; }
     public IFormFile pictureFile { get; set; }

}
public record UserRolesVeiwModel
{
    public int Id { get; set; }
    public string RoleName { get;set; }
}

public record AllRolesVeiwModel
{
    public int Id { get; set; }
    public string RoleName { get; set; }
}
public record AdminSideUserOrdersviewModel
{

}
public record AdminSideUserCommentViewModel
{

}

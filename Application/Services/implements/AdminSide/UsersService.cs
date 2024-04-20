
using Application.Services.Interfaces.AdminSide;
using Application.Services.Interfaces.UserSide;
using Application.ViewModel.AdminSide;
using Domain.entities.UserPart.User;
using Domain.entities.UserPart.Roles;
using Application.Helpers;
using Domain.IRepository.UserPart;

namespace Application.Services.implements.AdminSide;

public class UsersService : IUserService
{
    #region Ctor
    private readonly IAccountRepository _account;
    private readonly IRoleRepository _roleRepository;
    private readonly ILayoutService _layoutService;
    private readonly IRoleService _roleservice;
    public UsersService(IAccountRepository account, ILayoutService layoutService, IRoleService roleService, IRoleRepository roleRepository)
    {
        _account = account;
        _layoutService = layoutService;
        _roleservice = roleService;
        _roleRepository = roleRepository;

    }

    #endregion


    public async Task<List<AllUsersViewModel>> ListOFUers()
    {
        var users = await _account.GetUsersAsync();

        List<AllUsersViewModel> model = new List<AllUsersViewModel>();
        foreach (var user in users)
        {
            AllUsersViewModel childmodel = new AllUsersViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Created = user.Created,
                ScreenShot = user.UserAvatar!,
            };
            model.Add(childmodel);
        }
        return model;



    }

    public async Task<UsersViewModel> UsersViewModel(int id)
    {

        var users = await ListOFUers();
        UsersViewModel usersViewModel = new UsersViewModel()
        {
            AllUsers = users,
            Admin = await _layoutService.AdminInfo(id),
        };

        return usersViewModel;

    }

    public async Task<OneUserViewModel> FillUser(int id)
    {
        var user = await _account.GetUserByIdAsync(id);

        OneUserViewModel model = new OneUserViewModel()
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Created = user.Created,
            UserAvatar = user.UserAvatar!,
        };

        return model;
    }

    // move to roles
    public List<AllRolesVeiwModel> allRoles()
    {
        var roles = _roleRepository.GetRoles();

        List<AllRolesVeiwModel> model = new List<AllRolesVeiwModel>();

        foreach (var item in roles)
        {
            AllRolesVeiwModel childmodel = new AllRolesVeiwModel()
            {
                Id = item.Id,
                RoleName = item.RoleTitle,
            };
            model.Add(childmodel);
        }
        return model;

    }



    public async Task<List<UserRolesVeiwModel>> UserRoles(int id)
    {
        var roles = await _roleservice.GetUserRoles(id);
        List<UserRolesVeiwModel> model = new List<UserRolesVeiwModel>();

        foreach (var role1 in roles)
        {
            UserRolesVeiwModel childmodel = new UserRolesVeiwModel()
            {
                Id = role1.Id,
                RoleName = role1.RoleTitle,
            };
            model.Add(childmodel);
        }


        return model;

    }

    public async Task<UserDetailViewModel> UserDetail(int AdminId, int UserId)
    {
        var admin = await _layoutService.AdminInfo(AdminId);
        var user = await FillUser(UserId);
        var roles = await UserRoles(UserId);
        var allroles = allRoles();
        UserDetailViewModel model = new UserDetailViewModel()
        {
            Admin = admin,
            User = user,
            SelectedRoles = roles,
            AllRoles = allroles,
        };

        return model;
    }

    public bool EditUser(OneUserViewModel details, List<int> selectedroles)
    {
        var user = _account.Finduser(details.Id);

        user.UserName = details.UserName;
        user.Email = details.Email;

        #region Set new Image 
        if (details.pictureFile != null)
        {
            if(user.UserAvatar != null)
            {
                // delete exist image 
                string existimage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/UserAvatar", user.UserAvatar);

                if (File.Exists(existimage))
                {
                    File.Delete(existimage);
                }
            }

            //Save New Image
            user.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(details.pictureFile.FileName);


            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/UserAvatar", user.UserAvatar);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                details.pictureFile.CopyTo(stream);
            }

        }
        #endregion

        #region Update User Roles 

        var userroles = _roleRepository.listOfUserSelectedRoles(details.Id);
        if (userroles != null && userroles.Any())
        {
            _roleRepository.DeleteSelectedroles(userroles);
        }

        if (selectedroles != null && selectedroles.Any())
        {
            foreach (var roleid in selectedroles)
            {
                UserSelectedRole userSelectedRole = new UserSelectedRole()
                {
                    RoleId = roleid,
                    UserId = user.Id,
                };
                _roleRepository.AddUserSelectedrole(userSelectedRole);
            }

        }
        #endregion

        _account.UpdateByAdmin(user);
        return true;

    }

  public  void DeleteUserAvatar(int id)
    {
        
        var user = _account.Finduser(id);
        if (user.UserAvatar != null)
        {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/UserAvatar", user.UserAvatar);

            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
           
        }
        _account.DeleteUserAvatar(user);

    }
    public async Task DeleteUser(int userid)
    {
        await _account.DeleteUser(userid);
   
    }


}



using Application.ViewModel.AdminSide;

namespace Application.Services.Interfaces.AdminSide;

public interface IplatfromsService
{

    Task<AdminPlatformsViewModel> ListOfPLatforms(int adminid);

    Task AddNewPlatform(PlatfromsViewModel model);
    Task<AdminPlatformsViewModel> ShowAddNewPalform(int Id);
    Task<AdminPlatformsViewModel> GetPlatformById(int id, int adminid);
    Task UpdatePlatform(PlatfromsViewModel model);
    Task RemovePlatfrom(int id);
}

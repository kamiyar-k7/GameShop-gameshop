

using Application.ViewModel.AdminSide;

namespace Application.Services.Interfaces.AdminSide;

public interface IAdminHomeService
{
    
    Task<bool> DeleteMessage(int id);
    Task<AdminHomeViewModel> HomeAdminVeiwModel(int id);
    Task<AdminHomeViewModel> ContactService(int id);
    Task<AdminHomeViewModel> FillAdminAboutUs(int adminid);
    Task EditAboutUs(AdminAboutUsViewModel model);
}

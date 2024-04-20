
using Application.ViewModel.AdminSide;

namespace Application.Services.Interfaces.AdminSide;

public interface IAdminService
{
    Task<AdminsViewModel> AdminsViewModel(int id);
}

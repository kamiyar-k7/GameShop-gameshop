

using Application.ViewModel.AdminSide;

namespace Application.Services.Interfaces.AdminSide;

public interface ILayoutService
{
    Task<AdminInformationViewModel> AdminInfo(int id);
}

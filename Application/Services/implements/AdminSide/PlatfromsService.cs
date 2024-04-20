

using Application.Services.Interfaces.AdminSide;
using Application.ViewModel.AdminSide;
using Domain.entities.GamePart.Platform;
using Domain.IRepository.GamePart;

namespace Application.Services.implements.AdminSide;

public class PlatfromsService : IplatfromsService
{
    #region Ctor
    private readonly ILayoutService _layoutService;
    private readonly IPlatformRepository _platformRepository;
    public PlatfromsService(IPlatformRepository platformRepository, ILayoutService layoutService)
    {
        _platformRepository = platformRepository;
        _layoutService = layoutService; 

    }
    #endregion


    #region Platforms 
    public async Task<AdminPlatformsViewModel> ListOfPLatforms(int adminid)
    {
        var admin = await _layoutService.AdminInfo(adminid);
        var plats = await _platformRepository.GetPlatforms();


        List<PlatfromsViewModel> PlatModel = new List<PlatfromsViewModel>();

        foreach (var plat in plats)
        {
            PlatfromsViewModel childmodel = new PlatfromsViewModel()
            {
                Id = plat.Id,
                Name = plat.Name,
            };
            PlatModel.Add(childmodel);
        }



        AdminPlatformsViewModel viewModel = new AdminPlatformsViewModel()
        {
            Admin = admin,
            ListOfPlats = PlatModel,
        };
        return viewModel;
    }

    public async Task<AdminPlatformsViewModel> ShowAddNewPalform(int Id)
    {
        var admin = await _layoutService.AdminInfo(Id);
        AdminPlatformsViewModel model = new AdminPlatformsViewModel()
        {
            Admin = admin,
        };
        return model;
    }
    public async Task AddNewPlatform(PlatfromsViewModel model)
    {

        if (model != null)
        {
            Platform platform = new Platform()
            {
                Name = model.Name,
            };

            await _platformRepository.AddNewPlatform(platform);
        }


    }

    public async Task<AdminPlatformsViewModel> GetPlatformById(int id, int adminid)
    {
        var plat = await _platformRepository.GetPlatformById(id);
        var admin = await _layoutService.AdminInfo(adminid);
        PlatfromsViewModel platmodel = new PlatfromsViewModel()
        {
            Id = plat.Id,
            Name = plat.Name,
        };

        AdminPlatformsViewModel model = new AdminPlatformsViewModel()
        {
            Admin = admin,
            Platform = platmodel,
        };
        return model;
    }

    public async Task UpdatePlatform(PlatfromsViewModel model)
    {
        var plat = await _platformRepository.GetPlatformById(model.Id);

        plat.Name = model.Name;

        await _platformRepository.UpdatePlatform(plat);

    }

    public async Task RemovePlatfrom(int id)
    {
        var plat = await _platformRepository.GetPlatformById(id);

        await _platformRepository.RemovePlatform(plat);
    }
    #endregion


}

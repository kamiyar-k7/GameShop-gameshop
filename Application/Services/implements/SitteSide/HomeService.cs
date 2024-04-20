using Application.Services.Interfaces.UserSide;
using Application.ViewModel.UserSide;
using Domain.entities.GamePart.Game;
using Domain.entities.WebSite;
using Domain.IRepository.HomeRepositoryInterface;


namespace Application.Services.implements.SitteSide;



public class HomeService : IHomeService
{
    #region Ctor
    private readonly IHomeRepository _homeRepository;
    public HomeService(IHomeRepository homeRepository)
    {
        _homeRepository = homeRepository;
    }
    #endregion


    public async Task<AboutPageViewModel?> ShowAbout()
    {
        var about = await _homeRepository.AboutUs();
        if (about != null)
        {
            AboutUsViewModel aboutViewModel = new AboutUsViewModel()
            {
                Author = about.Author,
                AuthorUrl = about.AuthorUrl,
                Description = about.Description,
                Title = about.Title,
            };

            AboutPageViewModel model = new AboutPageViewModel()
            {
                AboutUsViewModel = aboutViewModel,
            };
            return model;
        }
        return null;
    }


    public async Task<bool> AddMessage(ContactUsViewModel aboutViewModel)
    {
        if (aboutViewModel != null)
        {
            ContactUs contactUsViewModel = new ContactUs()
            {
                Name = aboutViewModel.Name,
                Email = aboutViewModel.Email,
                Message = aboutViewModel.Message,
                Title = aboutViewModel.Title,
            };
            await _homeRepository.ContactUs(contactUsViewModel);
            return true;
        }
        return false;
    }
}

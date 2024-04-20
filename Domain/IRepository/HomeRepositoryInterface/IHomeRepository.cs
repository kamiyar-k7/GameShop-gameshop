using Domain.entities.WebSite;
namespace Domain.IRepository.HomeRepositoryInterface;


public interface IHomeRepository
{
    Task<AboutUs?> AboutUs();
    Task ContactUs(ContactUs contactUs);

    #region Admin Side
    Task<List<ContactUs>> Messages();
    Task<bool> DeleteMessage(int id);
    Task UpdateAboutUs(AboutUs aboutUs);
    #endregion

}


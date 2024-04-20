using Application.ViewModel.AdminSide;


namespace Application.Services.Interfaces.AdminSide;

public interface IgenreService
{
    Task<AdminGenresViewModel> ListOfGenres(int adminid);
    Task<AdminGenresViewModel> ShowAddGenreView(int adminid);
    Task AddNewGenre(GenresViewModel model);
    Task EditGenre(GenresViewModel model);
    Task<AdminGenresViewModel> GetGenreById(int id, int Adminid);
    Task RemoveGenre(int id);
}

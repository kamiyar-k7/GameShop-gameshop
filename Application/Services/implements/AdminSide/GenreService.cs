
using Application.Services.Interfaces.AdminSide;
using Application.ViewModel.AdminSide;
using Domain.entities.GamePart.Genre;
using Domain.IRepository.GamePart;
using System.Reflection;


namespace Application.Services.implements.AdminSide;

public class GenreService : IgenreService
{
    #region Ctor
    private readonly ILayoutService _layoutService;
    private readonly IGenreRepository _genreRepository;
    public GenreService(IGenreRepository genreRepository , ILayoutService layoutService)
    {
            _genreRepository = genreRepository;
             _layoutService = layoutService; 
    }
    #endregion

    public async Task<AdminGenresViewModel> ListOfGenres(int adminid)
    {
        var admin = await _layoutService.AdminInfo(adminid);
        var genres = await _genreRepository.GetListOfGenres();


        List<GenresViewModel> genremodel = new List<GenresViewModel>();

        foreach (var genre in genres)
        {
            GenresViewModel childmodel = new GenresViewModel()
            {
                Id = genre.Id,
                Name = genre.GenreName,
            };

            genremodel.Add(childmodel);
        }



        AdminGenresViewModel viewModel = new AdminGenresViewModel()
        {
            Admin = admin,
            ListOfGenres = genremodel,
        };
        return viewModel;
    }


    public async Task<AdminPlatformsViewModel> ShowAddNewGenre(int Id)
    {
        var admin = await _layoutService.AdminInfo(Id);
        AdminPlatformsViewModel model = new AdminPlatformsViewModel()
        {
            Admin = admin,
        };
        return model;
    }

    public async Task <AdminGenresViewModel> ShowAddGenreView(int adminid)
    {

        var admin = await _layoutService.AdminInfo(adminid);

        AdminGenresViewModel model = new AdminGenresViewModel()
        {
            Admin= admin,
            
        };
        return model;

    }

    public async Task AddNewGenre(GenresViewModel model)
    {
        var genre = await _genreRepository.GetGenreById(model.Id);

        Genre genre1 = new Genre()
        {
            GenreName = model.Name,
        };

        await _genreRepository.AddNewGenre(genre1);  


    }

    public async Task<AdminGenresViewModel> GetGenreById(int id , int Adminid)
    {
        var Genre = await _genreRepository.GetGenreById(id);
        var admin = await _layoutService.AdminInfo(Adminid);

        GenresViewModel genresViewModel = new GenresViewModel()
        {
            Id = id,
            Name = Genre.GenreName,
        };

        AdminGenresViewModel viewModel = new AdminGenresViewModel()
        {
            genre = genresViewModel,
            Admin= admin,
        };

        return viewModel;   

    }
    public async Task EditGenre(GenresViewModel  model)
    {
        var Genre = await _genreRepository.GetGenreById(model.Id);

        Genre.GenreName = model.Name;

        await _genreRepository.UpdateGenre(Genre);
    }

    public async Task RemoveGenre(int id)
    {
        var genre = await _genreRepository.GetGenreById(id);

        await _genreRepository.RemoveGenre(genre);
    }
}

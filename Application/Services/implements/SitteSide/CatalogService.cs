using Application.Services.Interfaces.UserSide;
using Application.ViewModel.UserSide;
using Domain.entities.GamePart.Game;
using Domain.entities.GamePart.Genre;
using Domain.entities.GamePart.Platform;
using Domain.IRepository.GamePart;
using System;


namespace Application.Services.implements.SitteSide;

public class CatalogService : ICatalogService
{
    #region Ctor

    private readonly IGameRepository _gameRepository;
    private readonly IPlatformRepository _platformRepository;
    private readonly IGenreRepository _genreRepository;
    public CatalogService(IGameRepository gameRepository, IPlatformRepository platform, IGenreRepository genreRepository)
    {

        _gameRepository = gameRepository;
        _platformRepository = platform;
        _genreRepository = genreRepository;
    }
    #endregion



    #region Genreal

    #region 
    public async Task<CatalogViewModel> GetCatalogAsync()
    {
        var listOfGames = await _gameRepository.GamesAsync();
        var listOfPlatforms = await _platformRepository.GetPlatforms();
        var ListOfGenres = await _genreRepository.GetListOfGenres();

        var model = await FillModel(listOfGames, listOfPlatforms, ListOfGenres);
        return model;

    }




    #region Search
    public async Task<CatalogViewModel> SearchCatalog(CatalogViewModel searchViewModel)
    {
      

        var listOfGames = await _gameRepository.GamesAsync();
        var listOfPlatforms = await _platformRepository.GetPlatforms();
        var ListOfGenres = await _genreRepository.GetListOfGenres();

        if (searchViewModel.search != null)
        {
            listOfGames = SearchGames(listOfGames, searchViewModel);
        }
     

        

  
        var model = await FillModel(listOfGames, listOfPlatforms, ListOfGenres);
        return model;


    }

    private List<Game> SearchGames(List<Game> games, CatalogViewModel? searchViewModel)
    {
        var searchstring = searchViewModel.search.SearchString;
        var platformId = searchViewModel.search.PlatfromId;
        var genreId = searchViewModel.search.GenreId;

        if (!string.IsNullOrEmpty(searchstring))
        {
            games = games.Where(x => x.Name.Contains(searchstring)).ToList();
        }

        if (platformId.HasValue)
        {
            games = games.Where(x => x.gameSelectedPlatforms.Any(p => p.PlatformId == platformId)).ToList();
        }

        if (genreId.HasValue)
        {
            games = games.Where(x => x.gemeSelectedGenres.Any(x => x.GenreId == genreId)).ToList();
        }

        return games;
    }





    #endregion

    private async Task<CatalogViewModel> FillModel(List<Game> listOfGames, List<Platform> listOfPlatforms, List<Genre> listOfGenres)
    {


        if (listOfGames != null && listOfPlatforms != null)
        {

            #region Game object Mapping

            List<CatalogGamesViewModel> GameModel = new List<CatalogGamesViewModel>();
            foreach (var games in listOfGames)
            {
                if (games.GamesStatus == Domain.entities.GamePart.Game.GameStatus.Active)
                {
                    CatalogGamesViewModel ChildGameMdoel = new CatalogGamesViewModel()
                    {
                        GameId = games.Id,
                        Name = games.Name,
                        Description = games.Description,
                        Price = games.Price,
                        Company = games.Company,
                        Quantitiy = games.Quantitiy,
                        Rating = games.Rating,
                        ReleaseDate = games.ReleaseDate,
                        SystemRequirements = games.SystemRequirements,
                        Trailer = games.Trailer,
                        Screenshots = new List<string>(),

                    };

                    foreach (var screens in games.Screenshots)
                    {
                        ChildGameMdoel.Screenshots.Add(screens.AvararUrl);
                    }
                    GameModel.Add(ChildGameMdoel);
                }
            
            }
            #endregion

            #region Platform object mapping
            List<CatalogPlatformsViewModel> PlatformModel = new List<CatalogPlatformsViewModel>();
            foreach (var plats in listOfPlatforms)
            {
                CatalogPlatformsViewModel ChildePlatformModel = new CatalogPlatformsViewModel()
                {
                    Id = plats.Id,
                    Name = plats.Name,
                  
                };
                PlatformModel.Add(ChildePlatformModel);
            }
            #endregion

            #region Genreobject Mapping 
            List<CatalogGenreViewModel> GenreModel = new List<CatalogGenreViewModel>();
            foreach (var Genre in listOfGenres)
            {
                CatalogGenreViewModel ChildeGenreModel = new CatalogGenreViewModel()
                {
                    Id = Genre.Id,
                    GenreName = Genre.GenreName,
                 
                };
                GenreModel.Add(ChildeGenreModel);
            }
            #endregion


            CatalogViewModel model = new CatalogViewModel
            {
                Games = GameModel,
                Platforms = PlatformModel,
                Genre = GenreModel,
            };


            return model;
        }
        return null;
    }
    #endregion





    #endregion

}
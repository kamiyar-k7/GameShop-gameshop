using Application.Helpers;
using Application.Services.Interfaces.AdminSide;
using Application.Services.Interfaces.UserSide;
using Application.ViewModel.AdminSide;
using Application.ViewModel.UserSide;
using Domain.entities.Comments;
using Domain.entities.GamePart.Game;
using Domain.entities.GamePart.Genre;
using Domain.entities.GamePart.Platform;
using Domain.IRepository.GamePart;





namespace Application.Services.implements.SitteSide;

public class ProductService : IProductService
{
    #region Ctor
    private readonly IGameRepository _gamerepository;
    private readonly IPlatformRepository _platformRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly ILayoutService _layoutService;



    public ProductService(IGameRepository gameRepository,
        IPlatformRepository platformRepository,
        IGenreRepository genreRepository,
       ICommentRepository commentRepository,
       ILayoutService layoutService)
    {
        _gamerepository = gameRepository;
        _platformRepository = platformRepository;
        _genreRepository = genreRepository;
        _commentRepository = commentRepository;
        _layoutService = layoutService;
    }
    #endregion

    #region General 

    #region Game Details
    // get game by id 
    public async Task<Game> GetGameAsync(int Id)
    {
        var Game = await _gamerepository.GetGameById(Id);
        return Game;
    }

    // fill game model 
    public async Task<GameViewModelProduct> fillgame(Game? Game)
    {

        if (Game != null)
        {
            GameViewModelProduct gameViewModel = new GameViewModelProduct()
            {
                Company = Game.Company,
                Description = Game.Description,
                Name = Game.Name,
                Id = Game.Id,
                Price = Game.Price,
                Rating = Game.Rating,
                ReleaseDate = Game.ReleaseDate,
                SystemRequirements = Game.SystemRequirements,
                Trailer = Game.Trailer,
                ScreenShots = new List<string>(),
                Quantity = Game.Quantitiy,
                Status = Game.GamesStatus
            };
            foreach (var item in Game.Screenshots)
            {
                gameViewModel.ScreenShots.Add(item.AvararUrl);
            }
            return gameViewModel;
        }
        return null;
    }

    // fill comments model 
    public async Task<List<CommentsViewModelProduct>> FillComment(int Id)
    {
        var game = await GetGameAsync(Id);
        var comments = await _commentRepository.GetCommentsOfGame(game.Id);
        List<CommentsViewModelProduct> listofcomments = new List<CommentsViewModelProduct>();
        if (comments != null)
        {

            foreach (var item in comments)
            {

                CommentsViewModelProduct commentmodel = new CommentsViewModelProduct()
                {
                    Comment = item.Comment,
                    CreatedAt = DateTime.UtcNow,
                    Title = item.Title,
                    UserName = item.User.UserName,
                    Ratings = item.Ratings,
                    UserAvatar = item.User.UserAvatar,


                };
                listofcomments.Add(commentmodel);
            }

            return listofcomments;
        }
        return null;
    }

    #region fill Platforms 
    public async Task<List<PlatformViewModelProduct>> FillPlatformModel(List<Platform> platforms)
    {

        List<PlatformViewModelProduct> listofplatforms = new List<PlatformViewModelProduct>();
        if (platforms != null)
        {
            foreach (var plats in platforms)
            {
                PlatformViewModelProduct platformViewModel = new PlatformViewModelProduct()
                {
                    Id = plats.Id,
                    Name = plats.Name,

                };
                listofplatforms.Add(platformViewModel);
            }
            return listofplatforms;
        }
        return null;
    }
    public async Task<List<PlatformViewModelProduct>> FillPlatformById(int Id)
    {
        var platforms = await _platformRepository.GetPlatformsById(Id);
        var plats = await FillPlatformModel(platforms);
        return plats;
    }
    #endregion


    #region FillGenre
    public async Task<List<GenreViewModelProduct>> FillGenre(List<Genre> Genres)
    {

        List<GenreViewModelProduct> listofgenres = new List<GenreViewModelProduct>();
        if (Genres != null)
        {
            foreach (var genre in Genres)
            {
                GenreViewModelProduct genreViewModel = new GenreViewModelProduct()
                {
                    Id = genre.Id,
                    GenreName = genre.GenreName,

                };
                listofgenres.Add(genreViewModel);
            }
            return listofgenres;
        }
        return null;
    }
    public async Task<List<GenreViewModelProduct>> FillGenreById(int Id)
    {
        var Genres = await _genreRepository.GetGenresById(Id);
        var selectedenre = await FillGenre(Genres);
        return selectedenre;
    }
    #endregion




    // fill Related GAmes 
    public async Task<List<RelatedGamesProduct>> FillRelated(int Id)
    {
        var game1 = await GetGameAsync(Id);
        var related = await _gamerepository.GetRelatedGamesBtGenre(game1);
        List<RelatedGamesProduct> relatedGames = new List<RelatedGamesProduct>();
        if (relatedGames != null)
        {
            foreach (var game in related)
            {
                RelatedGamesProduct relatedGamesmodel = new RelatedGamesProduct()
                {
                    Description = game.Description,
                    Id = game.Id,
                    Name = game.Name,
                    Price = game.Price,
                    Rating = game.Rating,
                    ScreenShots = new List<string>(),
                };
                foreach (var screen in game.Screenshots)
                {
                    relatedGamesmodel.ScreenShots.Add(screen.AvararUrl);
                }
                relatedGames.Add(relatedGamesmodel);
            }
            return relatedGames;

        }
        return null;
    }



    public async Task<ProductViewModel> GetProductById(int Id, int Adminid)
    {

        var Game = await GetGameAsync(Id);

        var gameModel = await fillgame(Game);

        var platforms = await FillPlatformById(Id);

        var Genres = await FillGenreById(Id);

        var related = await FillRelated(Id);

        var comments = await FillComment(Id);

        var allPlatformsFromDb = await _platformRepository.GetPlatforms();
        var AllPlats = FillPlatformModel(allPlatformsFromDb);
        var AllGenresFromDb = await _genreRepository.GetListOfGenres();
        var allgenres = FillGenre(AllGenresFromDb);

        AdminInformationViewModel admininfo = new AdminInformationViewModel();

        var admin = admininfo;
        if (Adminid != 0)
        {
            admin = await _layoutService.AdminInfo(Adminid);

        }
        else
        {
            admin = null;
        }




        if (Game != null)
        {


            ProductViewModel model = new ProductViewModel()
            {
                Game = gameModel,
                Platforms = platforms,
                Genres = Genres,
                RelatedGames = related,
                Comments = comments,
                Admin = admin ,
                AllGenres = await allgenres,
                AllPlatforms = await AllPlats,
                
                
                
            };


            return model;

        }
        return null;
    }


    public async Task<List<CommentsViewModelProduct>> GetCommentsbyGameId(int Id)
    {
        var game = await _gamerepository.GetGameById(Id);
        var comments = await _commentRepository.GetCommentsOfGame(game.Id);

        if (comments != null)
        {
            List<CommentsViewModelProduct> list = new List<CommentsViewModelProduct>();

            foreach (var item in comments)
            {

                CommentsViewModelProduct commentmodel = new CommentsViewModelProduct()
                {
                    Comment = item.Comment,
                    CreatedAt = DateTime.UtcNow,
                    Title = item.Title,
                    UserName = item.User.UserName,


                };
                list.Add(commentmodel);
            }

            return list;
        }
        return null;

    }

    public async Task<bool> SubmitComment(CommentsViewModelProduct model)
    {

        if (model != null)
        {
            Comments newcomment = new Comments()
            {
                Comment = model.Comment,
                CreatedAt = DateTime.UtcNow,
                Title = model.Title,
                GameId = model.GameId,
                Ratings = model.Ratings,
                UserId = model.UserId,

            };

            await _commentRepository.AddComment(newcomment);
            return true;
        }
        return false;
    }


    #endregion

    #endregion


    //---------------------------------
    #region Admin Side

    #region Game

    // fill list of games
    public async Task<List<GameViewModelProduct>> ListOfGames()
    {

        var games = await _gamerepository.AdminGames();

        List<GameViewModelProduct> model = new List<GameViewModelProduct>();

        foreach (var Game in games)
        {
            GameViewModelProduct childmodel = new GameViewModelProduct()
            {
                Company = Game.Company,
                Description = Game.Description,
                Name = Game.Name,
                Id = Game.Id,
                Price = Game.Price,
                Rating = Game.Rating,
                ReleaseDate = Game.ReleaseDate,
                SystemRequirements = Game.SystemRequirements,
                Trailer = Game.Trailer,
                ScreenShots = new List<string>(),
                Status = Game.GamesStatus,
                Quantity = Game.Quantitiy

            };
            foreach (var item in Game.Screenshots)
            {
                childmodel.ScreenShots.Add(item.AvararUrl);
            }
            model.Add(childmodel);
        }
        return model;

    }


    public async Task<ProductViewModel> ListOfProducts(int userid)
    {
        var admin = await _layoutService.AdminInfo(userid);
        var games = await ListOfGames();
       
        ProductViewModel adminProductViewModel = new ProductViewModel()
        {
            Admin = admin,
            ListGames = games,

        };

        return adminProductViewModel;
    }



    public async Task<ProductViewModel> ShowAddGame(int id)
    {
        var admin = await _layoutService.AdminInfo(id);
        var DBPlats = await _platformRepository.GetPlatforms();
        var Plats = await FillPlatformModel(DBPlats);
        var DBgenres = await _genreRepository.GetListOfGenres();
        var genres = await FillGenre(DBgenres);

        #region Object mapping 




        #endregion

        ProductViewModel adminGameViewModel = new ProductViewModel()
        {
            Admin = admin,
            Platforms = Plats,
            Genres = genres,


        };
        return adminGameViewModel;
    }

    public async Task<bool> AddNewGame(GameViewModelProduct model, List<int> selectedGenres, List<int> selectedPlatforms)
    {


        List<Screenshot> screenshots = new List<Screenshot>();
        Game NewGame = new Game()
        {
            Name = model.Name,
            Description = model.Description,
            Company = model.Company,
            Price = model.Price,
            Quantitiy = model.Quantity,
            Rating = model.Rating,
            Trailer = model.Trailer,
            ReleaseDate = model.ReleaseDate,
            // GameStatus =  selectedStatus
            gameSelectedPlatforms = new List<GameSelectedPlatform>(),
            gemeSelectedGenres = new List<GemeSelectedGenre>(),
            Screenshots = screenshots,
            SystemRequirements = model.SystemRequirements,
            GamesStatus = (GameStatus)model.selectedStatus,


        };

        var game = await fillgame(NewGame);

        // screenshots

        foreach (var file in model.FormFiles)
        {
            if (file != null && file.Length > 0)
            {
                string uniqueFileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(file.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/GameImages", uniqueFileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                Screenshot screen = new Screenshot()
                {
                    AvararUrl = uniqueFileName,
                    Game = NewGame,
                };
                screenshots.Add(screen);
            }
        }
        //Video 
        if (model.VideoFile != null)
        {
            //Save New Image
            string filename = NameGenerator.GenerateUniqCode() + Path.GetExtension(model.VideoFile.FileName);

            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/Trailers", filename);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                model.VideoFile.CopyTo(stream);
            }
            NewGame.Trailer = filename;
        }
        // plats
        foreach (var PlatId in selectedPlatforms)
        {
            GameSelectedPlatform gameSelectedPlatform = new GameSelectedPlatform()
            {
                PlatformId = PlatId,
                Game = NewGame
            };
            NewGame.gameSelectedPlatforms.Add(gameSelectedPlatform);
        }
        // genre
        foreach (var GenreId in selectedGenres)
        {
            GemeSelectedGenre selectedGenre = new GemeSelectedGenre()
            {
                GenreId = GenreId,
                Game = NewGame
            };
            NewGame.gemeSelectedGenres.Add(selectedGenre);

        }

        // statsu 



        if (NewGame != null)
        {
            await _gamerepository.AddNewGame(NewGame);
            return true;
        }




        return false;


    }

    public async Task EditGame(GameViewModelProduct model, List<int> selectedGenres, List<int> selectedPlatforms)
    {



        List<Screenshot> screenshots = new List<Screenshot>();

        var game = await _gamerepository.GetGameById(model.Id);

        game.Name = model.Name;
        game.Description = model.Description;
        game.Price = model.Price;
        game.SystemRequirements = model.SystemRequirements;
        game.Company = model.Company;
        game.Quantitiy = model.Quantity;
        game.Rating = model.Rating;
        game.ReleaseDate = model.ReleaseDate;
        game.GamesStatus = (GameStatus)model.selectedStatus;
        //game.Screenshots =  screenshots;



        // screenshots
         // it need to be fixed 
        if (model.FormFiles != null && model.FormFiles.Any())
        {
            foreach (var screenshot in game.Screenshots)
            {
                await _gamerepository.DeleteScrrenshot(screenshot.Id);
            }
           await _gamerepository.SaveChanges();
            // Clear existing screenshots
            game.Screenshots.Clear();

          

            foreach (var file in model.FormFiles)
            {
                if (file != null && file.Length > 0)
                {
                    string uniqueFileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(file.FileName);
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/GameImages", uniqueFileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    Screenshot screen = new Screenshot()
                    {
                        AvararUrl = uniqueFileName,
                    };
                    screenshots.Add(screen);
                }
            }

          
            game.Screenshots = screenshots;
        }





        //Video 
        if (model.VideoFile != null)
        {
            if (model.VideoFile != null)
            {
                //Save New Image
                string filename = NameGenerator.GenerateUniqCode() + Path.GetExtension(model.VideoFile.FileName);

                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/Trailers", filename);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    model.VideoFile.CopyTo(stream);
                }
                game.Trailer = filename;
            }
        }

        // genre

        var gamegenres = await _genreRepository.GameSelectedGenre(game.Id);
        _genreRepository.DeleteGameGenres(gamegenres);

        foreach (var GenreId in selectedGenres)
        {
            GemeSelectedGenre selectedGenre = new GemeSelectedGenre()
            {
                GenreId = GenreId,
                GameId = game.Id,
            };
            await _genreRepository.AddSelectedGenres(selectedGenre);

        }

        // plats

        var gameplatforms = await _platformRepository.GameSelectedPlatforms(game.Id);
        _platformRepository.DeleteGamePlatforms(gameplatforms);


        foreach (var PlatId in selectedPlatforms)
        {
            GameSelectedPlatform gameSelectedPlatform = new GameSelectedPlatform()
            {
                PlatformId = PlatId,
                GameId = game.Id,

            };

            await _platformRepository.AddselectedPlats(gameSelectedPlatform);
        }


         _gamerepository.UpdateGame(game);
        await _gamerepository.SaveChanges();

    }


    public async Task DeleteGame(int id)
    {
        await _gamerepository.DeleteGame(id);
    }

    #endregion

    #region Comment
    public async Task<CommentsViewModel> GetAllCommments(int adminid)
    {
        var adminifo = await _layoutService.AdminInfo(adminid);

        var comments = await _commentRepository.AllComents();

        List<CommentModel> commentsModel = new List<CommentModel>();    

        foreach (var item in comments)
        {
            CommentModel commentModel = new CommentModel()
            {
                Title = item.Title,
                Comment = item.Comment,
                CreatedAt = item.CreatedAt,
                Id = item.Id,
                Ratings = item.Ratings,
               UserName = item.User.UserName ,
               GameName = item.Game.Name,
            };
            commentsModel.Add(commentModel);
        }

        CommentsViewModel model = new CommentsViewModel()
        {
            Admin = adminifo,
            Allcoments = commentsModel,
        };

        return model;
        
    }


    public async Task DeleteComment(int id)
    {
        await _commentRepository.DeleteComment(id);

    }
    #endregion

    #endregion

}

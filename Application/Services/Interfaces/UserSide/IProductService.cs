using Application.ViewModel.AdminSide;
using Application.ViewModel.UserSide;


namespace Application.Services.Interfaces.UserSide;

public interface IProductService
{
    #region General

    Task<ProductViewModel> GetProductById(int Id , int Adminid);
    Task<bool> SubmitComment(CommentsViewModelProduct model);

    #endregion


    #region Admin Side
    Task<ProductViewModel> ListOfProducts(int userid);
    Task<ProductViewModel> ShowAddGame(int id);
    Task<bool> AddNewGame(GameViewModelProduct model, List<int> selectedGenres, List<int> selectedPlatforms);
    Task EditGame(GameViewModelProduct model, List<int> selectedGenres, List<int> selectedPlatforms);
    Task DeleteGame(int id);
    Task<CommentsViewModel> GetAllCommments(int adminid);
    Task DeleteComment(int id);


    #endregion
}
